using UnityEngine;
using System.Collections;

public class playerCollider : MonoBehaviour {

    public string side;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll)
    {
        if(side == "top" && coll.gameObject.tag == "White")
        {
            Debug.Log("Gotcha !");
        } else
        {
            GetComponentInParent<playerController>().setWallBounce(side, true);
        }
    }

    void OnTriggerExit(Collider coll)
    {
            GetComponentInParent<playerController>().setWallBounce(side, false);
    }
}
