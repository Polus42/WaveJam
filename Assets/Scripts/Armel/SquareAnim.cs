using UnityEngine;
using System.Collections;

public class SquareAnim : MonoBehaviour {

    private Rigidbody rb;
    private Transform transf;
    private Vector3 velocity;
    private Vector3 basicScale;
    private float newScaleX;
    private float newScaleY;
    private float newScaleZ;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        transf = GetComponent<Transform>();
        basicScale = transf.localScale;
    }

    // Update is called once per frame
    void Update () {
        Animate();
	}

    private void Animate ()
    {
        transf.localScale = basicScale;
        velocity = rb.velocity;
        newScaleX = transf.localScale.x * (1 + Mathf.Abs(velocity.x * 0.06f) - Mathf.Abs(velocity.y * 0.02f));
        newScaleY = transf.localScale.y * (1 + Mathf.Abs(velocity.y * 0.06f) - Mathf.Abs(velocity.x * 0.02f));
        newScaleZ = transf.localScale.z; 
        transf.localScale = new Vector3 (newScaleX, newScaleY, newScaleZ);
    }
}
