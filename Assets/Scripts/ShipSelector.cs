using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelector : MonoBehaviour
{
    public static ShipSelector instance;
    
    public float projectileSpeed = 8f;
    float playerSpeed = 14f;
    float playerBounds = 14.6f;

    bool canShoot = true;
    bool shootCountdown;
    float canShootTimer = 1f;
    float shootTime = 0f;

    [SerializeField] GameObject projectilePrefab;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            Movement();
        }
        else if (Input.GetAxis("Horizontal") == 0 && gameObject.transform.rotation.z != 0)
        {
            gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, 0, gameObject.transform.rotation.w);
        }

        if (Input.GetButton("Jump") && canShoot)
        {
            canShoot = false;
            shootCountdown = true;
            ShootProjectile();
        }

        if (shootCountdown)
        {
            shootTime += Time.deltaTime;
            if (shootTime >= canShootTimer)
            {
                shootCountdown = false;
                shootTime = 0f;
                canShoot = true;
            }
        }
    }

    void Movement()
    {
        Vector3 gameObjectPosition = gameObject.transform.position;
        gameObjectPosition.x = Mathf.Clamp(gameObjectPosition.x + Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime, -playerBounds, playerBounds);
        transform.position = gameObjectPosition;
    }

    void ShootProjectile()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}
