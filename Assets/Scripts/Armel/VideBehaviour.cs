using UnityEngine;
using System.Collections;

public class VideBehaviour : MonoBehaviour {

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
        if (other.CompareTag("Player"))
        {
            levelManager.RespawnPlayer();
        }
    }
}
