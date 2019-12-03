using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    PlayerController pc;

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public int distance = 0;

    float timePassed;

    public float sheild;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpawnWaves());
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
    }

    public void Update()
    {
        /*
        //game timer
        timePassed += Time.deltaTime + 120f;

        float sec = timePassed % 60f;
        */
        //the distance is updated based on power
        distance += Mathf.RoundToInt(pc.power);
        //print(distance);
        
        //print("Time elapsed in seconds: " + sec);
    }

    // Update is called once per frame
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }


    }

}
