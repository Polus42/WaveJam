using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour {
    private GameObject player;
    private bool started = false;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        foreach (AudioSource a in player.GetComponents<AudioSource>())
        {
            a.Pause();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameObject.GetComponent<AudioSource>().isPlaying&&!started)
        {
            foreach (AudioSource a in player.GetComponents<AudioSource>())
            {
                a.Play();
            }
            InvokeRepeating("switchColor", 5.0f, 5.0f);
            started = true;
        }
        if (Input.GetButtonDown("Submit"))
        {
            // on lance le jeu !
            GameObject.Find("Player");
            player.transform.Translate(0,0,-5);
            StartCoroutine(lerpToPlayerCamera(GameObject.Find("CameraMenu").transform.position, GameObject.Find("Main Camera").transform.position,1f));
        }
    }
    IEnumerator lerpToPlayerCamera( Vector3 start, Vector3 end, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0)
        {
            i += Time.deltaTime * rate;
            GameObject.Find("CameraMenu").transform.position = Vector3.Lerp(start, end, i);
            yield return null;
        }
        swapCamera();
        yield return null;
    }
    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Level");
    }
    void swapCamera()
    {
        GameObject.Find("CameraMenu").SetActive(false);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
        StartCoroutine(changeScene());
    }
    void switchColor()
    {
        player.GetComponent<WaveEmiter>().SwitchWaveLeft();
    }
}
