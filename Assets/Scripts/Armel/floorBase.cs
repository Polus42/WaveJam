using UnityEngine;
using System.Collections;

public class floorBase : MonoBehaviour {

    public bool isFast = false;
    public int color = 1;

    private GameObject player;

	// Use this for initialization
	void Start () {
	    //Get player walk speed
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    private void changeWorld()
    {
        if(Input.GetButtonDown("LeftTrigger"))
        {
            if (color == 1) color = 3;
            if (color == 2) color = 1;
            if (color == 3) color = 2;
        }

        if (Input.GetButtonDown("RightTrigger"))
        {
            if (color == 1) color = 2;
            if (color == 2) color = 3;
            if (color == 3) color = 1;
        }
    }
}
