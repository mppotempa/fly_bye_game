using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    PlayerController pc;

    public GameObject[] hazards;
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
    public bool isPlaying;

    float timePassed;
    bool restart;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaves());
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        sheildText.text = "Shield Levels: 100%";
        powerText.text = "Power Levels: 100%";
        isPlaying = true;

    }

    public void Update()
    {

        //the distance is updated based on power
        if (isPlaying)
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
                GameObject hazard = hazards[Random.Range (0, hazards.Length)];
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }


    }

    public void UpdateShield(int level)
    {
        //print("Sheild Text Updated");
        int newLevel = level * 10;
        sheildText.text = "Shield Levels: " + newLevel + "%";
    }

    public void UpdateLevelText(float level)
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
    }

    public void UpdateDistance(int distance)
    {
        distanceText.text = distance.ToString() + " KM";
    }

    public void EndGame()
    {
        print("Game Over");
        isPlaying = false;
        StopCoroutine(SpawnWaves());
    }

}
