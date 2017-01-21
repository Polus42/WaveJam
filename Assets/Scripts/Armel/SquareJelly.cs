using UnityEngine;
using System.Collections;

public class SquareJelly : MonoBehaviour {

    public Transform jellyTopLeft;
    public Transform jellyTopRight;
    public Transform jellyBotLeft;
    public Transform jellyBotRight;

    private float newScaleX;
    private float newScaleY;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ChangeScale();
	}

    void ChangeScale ()
    {
        newScaleX = (Mathf.Abs(jellyTopRight.position.x - jellyTopLeft.position.x) + Mathf.Abs(jellyBotRight.position.x - jellyBotLeft.position.x)) / 2;
        newScaleX = (newScaleX / 0.74f) * 0.75f;
        newScaleY = (Mathf.Abs(jellyTopRight.position.y - jellyBotRight.position.y) + Mathf.Abs(jellyTopLeft.position.y - jellyBotLeft.position.y)) / 2;
        newScaleY = (newScaleY / 0.74f) * 0.75f;
        transform.localScale = new Vector3(newScaleX, newScaleY, transform.localScale.z);
    }
}
