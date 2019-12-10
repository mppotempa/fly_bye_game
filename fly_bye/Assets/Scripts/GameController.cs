using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    PlayerController pc;

    public GameObject[] hazards;
    public GameObject[] obstacles;
    public GameObject playerObject;
    public GameObject mainPanel;
    public GameObject gameStatsPanel;
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
    public Text highscoreText;
    public Slider powerBar;
    public Slider sheildBar;

    float timePassed;
    string key = "highScore";
    bool restart;
    bool gameOver;
    bool isPlaying;
    Coroutine coSpawnWaves;

    // Start is called before the first frame update
    void Start()
    {
        /*
        StartCoroutine(SpawnWaves());
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        */
        mainPanel.SetActive(true);
        gameStatsPanel.SetActive(false);
        gameOver = false;
        isPlaying = false;
        ResetValues();

        highscoreText.text = "High Score: " + PlayerPrefs.GetInt(key, 0) + "km";

    }

    public void Update()
    {

        //the distance is updated based on power
        if (isPlaying && !gameOver)
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

    //create a new player object, start the game
    public void PlayGame()
    {
        //prevent duplicate games from being run
        if (!isPlaying)
        {
            ResetValues();
            mainPanel.SetActive(false);
            gameStatsPanel.SetActive(true);
            coSpawnWaves = StartCoroutine(SpawnWaves());
            //instantiate new player object
            Instantiate(playerObject, new Vector3(0, 0, 0), Quaternion.identity);

            print("Spawn New Player");
            //set up to update player info
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            pc = player.GetComponent<PlayerController>();

            isPlaying = true;
            gameOver = false;
        }
    }

    public void UpdateShield(int level)
    {
        //print("Sheild Text Updated");
        int newLevel = level * 10;
        sheildText.text = "Shield Levels: " + newLevel + "%";
        sheildBar.value = level;
    }

    //set the power level
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

    //sets values to end the game
    public void EndGame()
    {
        print("Game Over");
        gameOver = true;
        isPlaying = false;
        StopCoroutine(coSpawnWaves);
        if (HighScore())
        {
            highscoreText.text = "New High Score: " + PlayerPrefs.GetInt(key, 0) + "km";
        }
        else
        {
            highscoreText.text = "High Score: " + PlayerPrefs.GetInt(key, 0) + "km";
        }
        mainPanel.SetActive(true);
        gameStatsPanel.SetActive(false);
    }

    //check to see if there is a new highscore and assign it
    public bool HighScore()
    {
        int current = PlayerPrefs.GetInt(key, 0);
        if (distance > current)
        {
            PlayerPrefs.SetInt(key, distance);
            PlayerPrefs.Save();
            return true;
        }
        else
        {
            return false;
        }
    }

    //reset variables to base values
    public void ResetValues()
    {
        powerBar.value = 100;
        sheildBar.value = 10;
        sheildText.text = "Shield Levels: 100%";
        powerText.text = "Power Levels: 100%";
        distance = 0;
    }
}
