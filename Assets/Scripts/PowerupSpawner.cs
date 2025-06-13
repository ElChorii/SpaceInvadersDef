using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public static PowerupSpawner instance;

    [SerializeField]
    GameObject invertedPrefab;

    [SerializeField]
    GameObject speedPrefab;

    void Awake()
    {
        instance = this;
    }

    public void SpawnPowerup(Vector3 spawnPos)
    {
        int randomValue = Random.Range(0, 2);
        Debug.Log(randomValue);
        
        if (randomValue == 0)
        {
            Instantiate(invertedPrefab, spawnPos, Quaternion.identity);
        }
        else if (randomValue == 1)
        {
            Instantiate(speedPrefab, spawnPos, Quaternion.identity);
        }
    }
}
