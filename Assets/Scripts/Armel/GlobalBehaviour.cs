using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GlobalBehaviour : MonoBehaviour {

    public int color = 1; // 1 == BLUE; 2 == GREEN; 3 == RED;
    public GameObject currentCheckpoint;

    private bool triggerTaped = false;
    private List<GameObject> gameBlocks = new List<GameObject>();
    private GameObject[] UIParticles1;
    private GameObject[] UIParticles2;
    private GameObject[] UIParticles3;

    private GameObject _player;

    // Use this for initialization
    void Start () {
        _player = GameObject.Find("Player");
        if (gameBlocks.Capacity == 0)
        {
            gameBlocks.AddRange(GameObject.FindGameObjectsWithTag("GameBlock"));
            gameBlocks.AddRange(GameObject.FindGameObjectsWithTag("Green"));
            gameBlocks.AddRange(GameObject.FindGameObjectsWithTag("White"));
        }

        UIParticles1 = GameObject.FindGameObjectsWithTag("UIParticles");
        UIParticles2 = GameObject.FindGameObjectsWithTag("UIParticles2");
        UIParticles3 = GameObject.FindGameObjectsWithTag("UIParticles3");
        foreach (GameObject particles in UIParticles2)
        {
            particles.SetActive(false);
        }
        foreach (GameObject particles in UIParticles3)
        {
            particles.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        changeWorld();
        UpdateWorld();
	}

    private void changeWorld()
    {
        // 1 == RED; 2 == BLUE; 3 == GREEN;
        if (Input.GetAxis("Triggers") == 1 && !triggerTaped)
        {
            triggerTaped = true;
            if (color == 1) color = 3;
            else if (color == 2) color = 1;
            else if (color == 3) color = 2;
            //Debug.Log("color "+color);
            _player.GetComponent<playerController>().switchWaveLeft();
            _player.GetComponent<WaveEmiter>().SwitchWaveLeft();
        }

        if (Input.GetAxis("Triggers") == -1 && !triggerTaped)
        {
            triggerTaped = true;
            if (color == 1) color = 2;
            else if (color == 2) color = 3;
            else if (color == 3) color = 1;
            _player.GetComponent<playerController>().switchWaveRight();
            _player.GetComponent<WaveEmiter>().SwitchWaveRight();
            //Debug.Log("color " + color);
        }

        if (Input.GetAxis("Triggers") == 0 && triggerTaped)
        {
            triggerTaped = false;
        }
        changeUIColor();
    }
    void changeUIColor()
    {
        if (color == 1)
        {
            foreach(GameObject particles in UIParticles1)
            {
                particles.SetActive(true);
            }
            foreach (GameObject particles in UIParticles2)
            {
                particles.SetActive(false);
            }
            foreach (GameObject particles in UIParticles3)
            {
                particles.SetActive(false);
            }
        }
        else if (color == 2)
        {
            foreach (GameObject particles in UIParticles1)
            {
                particles.SetActive(false);
            }
            foreach (GameObject particles in UIParticles2)
            {
                particles.SetActive(true);
            }
            foreach (GameObject particles in UIParticles3)
            {
                particles.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject particles in UIParticles1)
            {
                particles.SetActive(false);
            }
            foreach (GameObject particles in UIParticles2)
            {
                particles.SetActive(false);
            }
            foreach (GameObject particles in UIParticles3)
            {
                particles.SetActive(true);
            }
        }
    }
    private void UpdateWorld ()
    {
        foreach (GameObject gameBlock in gameBlocks)
        {
            if(color == 1) //BLUE
            {
                if(gameBlock.GetComponent<floorBase>().isBlueActive)
                {
                    gameBlock.SetActive(true);
                }
                else
                {
                    gameBlock.SetActive(false);
                }
            }
            if (color == 2) //GREEN
            {
                if (gameBlock.GetComponent<floorBase>().isGreenActive)
                {
                    gameBlock.SetActive(true);
                }
                else
                {
                    gameBlock.SetActive(false);
                }
            }
            if (color == 3) //RED
            {
                if (gameBlock.GetComponent<floorBase>().isRedActive)
                {
                    gameBlock.SetActive(true);
                }
                else
                {
                    gameBlock.SetActive(false);
                }
            }
        }
    }

    public void RespawnPlayer()
    {
        _player.transform.position = currentCheckpoint.transform.position;
        currentCheckpoint.GetComponent<CheckpointBehaviour>().particle.Play();
    }
}
