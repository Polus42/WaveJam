using UnityEngine;
using System.Collections;

public class GlobalBehaviour : MonoBehaviour {

    public int color = 1; // 1 == RED; 2 == BLUE; 3 == GREEN;

    private bool triggerTaped = false;
    private GameObject[] gameBlocks;

    // Use this for initialization
    void Start () {
        if (gameBlocks == null)
            gameBlocks = GameObject.FindGameObjectsWithTag("GameBlock");
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
            Debug.Log("color "+color);
        }

        if (Input.GetAxis("Triggers") == -1 && !triggerTaped)
        {
            triggerTaped = true;
            if (color == 1) color = 2;
            else if (color == 2) color = 3;
            else if (color == 3) color = 1;
            Debug.Log("color " + color);
        }

        if (Input.GetAxis("Triggers") == 0 && triggerTaped)
        {
            triggerTaped = false;
        }
    }

    private void UpdateWorld ()
    {
        foreach (GameObject gameBlock in gameBlocks)
        {
            if(color == 1) //RED
            {
                if(gameBlock.GetComponent<floorBase>().isRedActive)
                {
                    gameBlock.SetActive(true);
                }
                else
                {
                    gameBlock.SetActive(false);
                }
            }
            if (color == 2) //BLUE
            {
                if (gameBlock.GetComponent<floorBase>().isBlueActive)
                {
                    gameBlock.SetActive(true);
                }
                else
                {
                    gameBlock.SetActive(false);
                }
            }
            if (color == 3) //GREEN
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
        }
    }
}
