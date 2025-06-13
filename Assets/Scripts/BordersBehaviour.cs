using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordersBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Contains("UFO"))
        {
            EnemiesBehaviour.instance.ChangeDirection();
        }
    }
}
