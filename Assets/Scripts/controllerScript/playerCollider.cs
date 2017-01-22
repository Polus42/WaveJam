using UnityEngine;
using System.Collections;

public class playerCollider : MonoBehaviour {

    public string side;

    private bool isTriggered = false;
    Collider other;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(isTriggered && !other.gameObject.activeSelf)
        {
            GetComponentInParent<playerController>().setWallBounce(side, false);
            isTriggered = false;
            other = null;
        }
	
	}

    void OnTriggerEnter(Collider coll)
    {
        if(side == "top" && coll.gameObject.tag == "White")
        {
            Debug.Log("Gotcha !");
        } else
        {
            if (coll.gameObject.tag != "CheckPoint")
            {
                isTriggered = true;
                other = coll;
                GetComponentInParent<playerController>().setWallBounce(side, true);
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        GetComponentInParent<playerController>().setWallBounce(side, false);
        isTriggered = false;
        other = null;
    }
}
