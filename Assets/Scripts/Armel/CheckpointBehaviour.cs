using UnityEngine;
using System.Collections;

public class CheckpointBehaviour : MonoBehaviour {

    public GameObject checkpoint;

    private GlobalBehaviour levelManager;

    // Use this for initialization
    void Start()
    {
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
                levelManager.currentCheckpoint = checkpoint;
        }
    }
}
