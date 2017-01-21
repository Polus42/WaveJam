using UnityEngine;
using System.Collections;

public class greenPlatformBehavior : MonoBehaviour {

    public enum LaunchSide { Up, Down, Left, Right};
    public float power;

    public LaunchSide launchSide;

    private Transform player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Jump") && player)
        {
            if(player.gameObject.GetComponent<playerController>().waveColor == playerController.WaveColor.green)
            {
                Rigidbody rb = player.gameObject.GetComponent<Rigidbody>();
                switch (launchSide)
                {
                    case LaunchSide.Up:
                        rb.AddForce(Vector3.up * power, ForceMode.Impulse);
                        break;
                    case LaunchSide.Down:
                        rb.AddForce(Vector3.down * power, ForceMode.Impulse);
                        break;
                    case LaunchSide.Left:
                        rb.AddForce(Vector3.left * power, ForceMode.Impulse);
                        break;
                    case LaunchSide.Right:
                        rb.AddForce(Vector3.right * power, ForceMode.Impulse);
                        break;
                }
            }
        }        	
	}

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            player = coll.gameObject.transform;
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            player = null;
        }
    }
}
