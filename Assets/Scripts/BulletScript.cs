using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector] public float currentBulletSpeed;

    private float limitY = 10f;

    private Rigidbody bulletRigidbody;

    private void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.position.y >= limitY)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        bulletRigidbody.MovePosition(transform.position + Vector3.up * currentBulletSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        MenuScript.instance.LoadSceneDEBUG(collision.gameObject.name);
    }
}
