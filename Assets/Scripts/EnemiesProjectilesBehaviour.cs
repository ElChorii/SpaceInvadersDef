using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesProjectilesBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float boundDown;

    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - (speed * Time.deltaTime), gameObject.transform.position.z);
        if (gameObject.transform.position.y < boundDown)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("SpaceShip"))
        {
            Destroy(gameObject);
            LivesPointsBehaviour.instance.LoseLive();
        }
    }
}
