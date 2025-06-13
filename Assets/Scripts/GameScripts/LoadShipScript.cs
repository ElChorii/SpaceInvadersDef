using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadShipScript : MonoBehaviour
{
    [SerializeField] private Vector3 playerSpawnPosition;

    void Start()
    {
        //Instantiate(MenuScript.instance.currentSpaceship, playerSpawnPosition, Quaternion.identity);

        Debug.Log(MenuScript.instance.currentSpaceshipDEBUG);
    }
}
