using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsBehaviourAll : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpaceShip"))
        {
            Debug.Log(other.name);
            if (gameObject.CompareTag("PowerupInverted"))
            {
                PlayerBehaviour.instance.StartInvertedDebuff();
                Debug.Log("Invertido");
            }
            else if (gameObject.CompareTag("PowerupSpeed"))
            {
                PlayerBehaviour.instance.StartSpeed();
                Debug.Log("Rápido");
            }
            Destroy(gameObject);
        }        
    }
}
