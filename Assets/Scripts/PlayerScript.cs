using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;

    [SerializeField] private float shipSpeed;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float bulletSpeed;

    [HideInInspector] public bool canPlayerMove;

    [HideInInspector] public bool canShoot = true;
    [HideInInspector] public bool canSuperShoot = true;

    [SerializeField] private float leftBarrier;
    [SerializeField] private float rightBarrier;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject superBulletPrefab;
    private Rigidbody shipRigidbody;

    private Vector3 currentPosition;

    [SerializeField] private bool hasTwoGuns;
    [SerializeField] private bool useRightGun = false;
    [SerializeField] private Vector3 bulletSpawn;
    [SerializeField] private Vector3 secondaryBulletSpawn;
    private Vector3 selectedBulletSpawn;

    //power ups
    [SerializeField] private GameObject invertirPowerUp;
    bool invertido = false;
    [SerializeField] private GameObject velocidadDisparoPowerUp;
    bool masRapido = false;
    [SerializeField] private GameObject velocidadMovimientoPowerUp;
    bool movimientoRapido = false;

    [SerializeField] private float powerUpDuration = 8f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        shipRigidbody = GetComponent<Rigidbody>();

        canPlayerMove = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (canPlayerMove) 
        {
            Movement();
        }
    }

    private void Movement()
    {
        currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x + (Input.GetAxis("Horizontal")) * shipSpeed * Time.deltaTime, leftBarrier, rightBarrier);

        if (invertido == true)
        {
            shipSpeed = shipSpeed * -1;
        }

        shipRigidbody.MovePosition(currentPosition);
    }

    private void Shoot()
    {
        if (hasTwoGuns == true)
        {
            if (useRightGun)
            {
                selectedBulletSpawn = bulletSpawn;
            }
            else
            {
                selectedBulletSpawn = secondaryBulletSpawn;
            }

            useRightGun = !useRightGun;
        }
        else
        {
            selectedBulletSpawn = bulletSpawn;
        }

        GameObject createdBullet = Instantiate(bulletPrefab, transform.position + selectedBulletSpawn, Quaternion.identity);
        createdBullet.GetComponent<BulletScript>().currentBulletSpeed = bulletSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + bulletSpawn, new Vector3(0.1f, 0.1f, 0.1f));

        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(transform.position + secondaryBulletSpawn, new Vector3(0.1f, 0.1f, 0.1f));
    }
    public void CrearPowerUp(Vector3 posicion)
    {
        if (Random.Range(0f, 1f) <= Random.Range(0.05f, 0.1f))
        {
            Instantiate(invertirPowerUp, posicion, Quaternion.identity);
        }
        else if (Random.Range(0f, 1f) <= Random.Range(0.11f, 0.16f))
        {
            Instantiate(velocidadDisparoPowerUp, posicion, Quaternion.identity);
        }
        else if (Random.Range(0f, 1f) <= Random.Range(0.17f, 0.22f))
        {
            Instantiate(velocidadMovimientoPowerUp, posicion, Quaternion.identity);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("invertirPowerUp"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(InvertirMovimiento());
        }
        if (collision.gameObject.CompareTag("movimientoPowerUp"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(MasVelocidadDisparo());
        }
        if (collision.gameObject.CompareTag("disparoPowerUp"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(MasVelocidadMovimiento());
        }
    }
    IEnumerator InvertirMovimiento()
    {
        invertido = true;
        yield return new WaitForSeconds(powerUpDuration);
        invertido = false;
        yield return null;
    }
    IEnumerator MasVelocidadDisparo()
    {
        masRapido = true;
        yield return new WaitForSeconds(powerUpDuration);
        masRapido = false;
        yield return null;
    }
    IEnumerator MasVelocidadMovimiento()
    {
        movimientoRapido = true;
        yield return new WaitForSeconds(powerUpDuration);
        movimientoRapido = false;
        yield return null;
    }
}
