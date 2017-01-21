using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GlobalBehaviour : MonoBehaviour {

    public int color = 1; // 1 == BLUE; 2 == GREEN; 3 == RED;

    private bool triggerTaped = false;
    private List<GameObject> gameBlocks = new List<GameObject>();

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
            GameObject.Find("LBUI").GetComponent<RawImage>().color = Color.green;
            GameObject.Find("RBUI").GetComponent<RawImage>().color = Color.red;
        }
        else if (color == 2)
        {
            GameObject.Find("LBUI").GetComponent<RawImage>().color = Color.red;
            GameObject.Find("RBUI").GetComponent<RawImage>().color = Color.blue;
        }
        else
        {
            GameObject.Find("LBUI").GetComponent<RawImage>().color = Color.blue;
            GameObject.Find("RBUI").GetComponent<RawImage>().color = Color.green;
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
}
