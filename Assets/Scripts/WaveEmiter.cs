using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveEmiter : MonoBehaviour {

    public Color blueColor = Color.blue;
    public Color redColor = Color.red;
    public Color greenColor = Color.green;
    public Texture WaveShape;
    public float Intensity = 1f;
    public float fadeouttime = 8f;
    public float speed = 40;


    private AudioSource _switchSound;
    private AudioSource _bluesource;
    private AudioSource _redsource;
    private AudioSource _greensource;

    private List<Light> _lightWave = new List<Light>();
    // Use this for initialization
    void Start () {
        // Adding music sources
        _bluesource = gameObject.GetComponents<AudioSource>()[0];
        _redsource = gameObject.GetComponents<AudioSource>()[1];
        _greensource = gameObject.GetComponents<AudioSource>()[2];
        _switchSound = gameObject.GetComponents<AudioSource>()[3];
    }
	// Update is called once per frame
	void Update () {
    transform.Translate(new Vector3(Input.GetAxis("Horizontal"),0,0));
        if (SpectrumAnalyzer.isThisABeat())
        {
            if(_bluesource.mute==false)
            {
                EmitWave(blueColor, speed);
            }
            if(_greensource.mute==false)
            {
                EmitWave(greenColor, speed);
            }
            if(_redsource.mute==false)
            {
                EmitWave(redColor, speed);
            }
        }
    }
    void EmitWave(Color c,float speed)
    {
        GameObject lightGameObject = new GameObject("Wave");
        Light lightComp = lightGameObject.AddComponent<Light>();
        lightComp.color = c;
        lightComp.intensity = Intensity;
        lightComp.type = LightType.Directional;
        lightComp.cookie = WaveShape;
        lightComp.cookieSize = 1;
        lightGameObject.transform.position = transform.position;
        lightGameObject.transform.Translate(new Vector3(0, 0, -9));
        lightGameObject.transform.localEulerAngles = new Vector3(0,0,0);

        StartCoroutine(SpreadLight(lightGameObject,lightComp,speed));

        _lightWave.Add(lightComp);
    }
    IEnumerator SpreadLight(GameObject lightGameObject, Light lightComp, float speed)
    {
        while(lightComp.cookieSize<100)
        {
            lightComp.cookieSize += Time.deltaTime*speed ;
            lightComp.intensity -= Time.deltaTime*fadeouttime;
            yield return null;
        }
        _lightWave.Remove(lightComp);
        Destroy(lightGameObject);
        yield return null;
    }
    void SwitchColor(Color c)
    {
        foreach(Light l in _lightWave)
        {
            l.color = c;
        }
    }
    void SwitchToBlue()
    {
        _switchSound.Play();
        _redsource.mute = true;
        _greensource.mute = true;
        _bluesource.mute = false;
        SwitchColor(blueColor);
    }
    void SwitchToRed()
    {
        _switchSound.Play();
        _redsource.mute = false;
        _greensource.mute = true;
        _bluesource.mute = true;
        SwitchColor(redColor);
    }
    void SwitchToGreen()
    {
        _switchSound.Play();
        _redsource.mute = true;
        _greensource.mute = false;
        _bluesource.mute = true;
        SwitchColor(greenColor);
    }
    // for testing purposes only
    void OnGUI()
    {
        /*stringToEdit = GUI.TextField(new Rect(10, 20, 200, 20), stringToEdit, 25);
        */
        if (GUI.Button(new Rect(10, 10, 100, 20), "Switch Blue"))
            SwitchToBlue();
        if (GUI.Button(new Rect(10, 30, 100, 20), "Switch Red"))
            SwitchToRed();
    }
}
