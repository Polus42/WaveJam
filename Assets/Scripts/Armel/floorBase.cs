using UnityEngine;
using System.Collections;

public class floorBase : MonoBehaviour {

    public bool isRedActive = false;
    public bool isGreenActive = false;
    public bool isBlueActive = false;

    // Use this for initialization
    void Start () {
        GetComponent<Renderer>().material.mainTextureScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

}
