using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveEmiter : MonoBehaviour {
    public Texture WaveShape;
    public float Intensity = 1f;
    public float fadeouttime = 8f;
    private List<Light> _lightWave = new List<Light>();
    private string stringToEdit = "";
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
    transform.Translate(new Vector3(Input.GetAxis("Horizontal"),0,0));
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
    // for testing purposes only
    void OnGUI()
    {
        stringToEdit = GUI.TextField(new Rect(10, 20, 200, 20), stringToEdit, 25);

        if (GUI.Button(new Rect(10, 10, 50, 10), "Pulse"))
            EmitWave(Color.blue,float.Parse(stringToEdit));
    }
}
