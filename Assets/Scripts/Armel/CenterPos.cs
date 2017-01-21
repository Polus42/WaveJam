using UnityEngine;
using System.Collections;

public class CenterPos : MonoBehaviour {

    private Transform[] sons;
    private Vector3 pos;
    private Vector3 newPos;
	// Use this for initialization
	void Start () {
        sons = GetComponentsInChildren<Transform>();
        pos = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        newPos = pos;
	    foreach(Transform trans in sons)
        {
            newPos += trans.position;
        }
        newPos = newPos / sons.Length;
        transform.position = newPos;
	}
}
