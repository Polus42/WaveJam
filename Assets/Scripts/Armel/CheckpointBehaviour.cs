using UnityEngine;
using System.Collections;

public class CheckpointBehaviour : MonoBehaviour {

    public GameObject checkpoint;
    public Material activeMat;
    public Material inactiveMat;

    private GlobalBehaviour levelManager;

    [HideInInspector]
    public ParticleSystem particle;
    [HideInInspector]
    public MeshRenderer mesh;

    // Use this for initialization
    void Start()
    {
        particle = transform.FindChild("Particle").gameObject.GetComponent<ParticleSystem>();
        mesh = transform.FindChild("Cube").gameObject.GetComponent<MeshRenderer>();
        levelManager = FindObjectOfType<GlobalBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            levelManager.currentCheckpoint.GetComponent<CheckpointBehaviour>().mesh.material = inactiveMat;
            levelManager.currentCheckpoint = checkpoint;
            mesh.material = activeMat;
        }
    }
}
