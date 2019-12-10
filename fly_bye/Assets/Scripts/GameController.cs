﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    PlayerController pc;

    public GameObject[] hazards;
    public GameObject[] obstacles;
    public GameObject playerObject;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float sheild;
    public int distance = 0;
    public Text distanceText;
    public Text powerText;
    public Text sheildText;
    public Slider powerBar;
    public Slider sheildBar;
    public GameObject mainMenuPanel;

    float timePassed;
    bool restart;
    bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        /*
        StartCoroutine(SpawnWaves());
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        */
        sheildText.text = "Shield Levels: 100%";
        powerText.text = "Power Levels: 100%";
        gameOver = true;
        powerBar.value = 100;
        sheildBar.value = 10;

    }

    public void Update()
    {

        //the distance is updated based on power
        if (!gameOver)
        {
            distance += Mathf.RoundToInt(pc.power);
            UpdateDistance(distance);
        }
    }

    // Update is called once per frame
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject item;
                int objectType = Random.Range(0, 2);
                if (objectType > 0)
                {
                    item = hazards[Random.Range(0, hazards.Length)];
                }
                else
                {
                    item = obstacles[Random.Range(0, obstacles.Length)];
                }
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (item, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                break;
            }
        }


    }

    public void PlayGame()
    {
        StartCoroutine(SpawnWaves());
        //instantiate new player object
        Instantiate(playerObject, new Vector3(0, 0, 0), Quaternion.identity);

        print("Spawn New Player");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();

        gameOver = false;

    }

    public void UpdateShield(int level)
    {
        //print("Sheild Text Updated");
        int newLevel = level * 10;
        sheildText.text = "Shield Levels: " + newLevel + "%";
        sheildBar.value = level;
    }

    public void UpdateLevel(float level)
    {
        int intLevel;
        if (level > 0)
        {
            intLevel = Mathf.FloorToInt(level * 100.0f);
        }
        else
        {
            intLevel = 0;
        }
        powerText.text = "Power Level: " + intLevel + "%";
        powerBar.value = intLevel;
    }

    public void UpdateDistance(int distance)
    {
        distanceText.text = distance.ToString() + " KM";
    }

    public void EndGame()
    {
        print("Game Over");
        gameOver = true;
        //StopCoroutine(SpawnWaves());
        GameObject.Find("MenuPanel").SetActive(true);
    }

}
