using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehavior : MonoBehaviour {
    private int activeButton = 0; // 0 play , 1 quit
    private GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
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
    void swapCamera()
    {
        GameObject.Find("CameraMenu").SetActive(false);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
    }
}
