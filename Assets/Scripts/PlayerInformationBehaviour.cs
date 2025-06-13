using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformationBehaviour : MonoBehaviour
{
    public static PlayerInformationBehaviour instance;

    [HideInInspector] public int selectedSpaceShipP1 = -1;
    [HideInInspector] public int selectedSpaceShipP2 = -1;

    [HideInInspector] public bool hasGameStarted = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
}
