using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public GameObject[] backgroundPrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private Vector3 backgroudSpawnPos = new Vector3(95, 1.3f, 19.3f);

    private PlayerController playerControllerScript;

    //store values for Instantiate section

        //for objects
    private float startDelay = 2;
    private float repeateRate = 2;
    //for background
    public float startDelayBackground = 2;
    public float repeateRateBackground = 2;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        InvokeRepeating("SpawnObstacle", startDelay, repeateRate);

        InvokeRepeating("SpawnBackground", startDelayBackground, repeateRateBackground);


    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == true)
        {
            CancelInvoke();
            
        }
    }

    void SpawnObstacle()
    {
        int obstacleIndex = Random.Range(0, obstaclePrefab.Length);

        Instantiate(obstaclePrefab[obstacleIndex], spawnPos, obstaclePrefab[obstacleIndex].transform.rotation);
    }

    void SpawnBackground()
    {
        int backgroundIndex = Random.Range(0, backgroundPrefab.Length);

        Instantiate(backgroundPrefab[backgroundIndex], backgroudSpawnPos, backgroundPrefab[backgroundIndex].transform.rotation);
    }
}
