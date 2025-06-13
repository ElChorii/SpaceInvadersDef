using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour instance;

    public float projectileSpeed;
    float addedYToBullet = 1f;
    float addedZToBullet = 0.8f;

    [SerializeField] float playerSpeed;
    [SerializeField] float playerBounds;

    [SerializeField] float playerRotationTime;
    [SerializeField] float playerForwardTime;
    [SerializeField] LeanTweenType playerRotationAnim;
    [SerializeField] LeanTweenType playerForwardAnim;

    [SerializeField] float initialZPosition;
    [SerializeField] float spaceBetweenLayers;
    [SerializeField] float layersAmount;

    [SerializeField] float goUpRotationAmount;
    [SerializeField] float goDownRotationAmount;
    [SerializeField] float normalPlayerRotation;
    float wantedRotation;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject specialProjectilePrefab;

    [HideInInspector] public bool canChangeLayer;
    [HideInInspector] public bool canChangeLayer2;
    bool limitReached;
    bool wantLayerUp;
    float wantedLayer;

    [HideInInspector] public bool canShoot;
    [HideInInspector] public bool canShootSpecial;
    [HideInInspector] public bool canShoot2;
    [HideInInspector] public bool canShootSpecial2;

    bool shootCooldown2;
    float shootCooldownTime2 = 0.8f;
    float originalShootCooldownTime2;

    bool specialShootCooldown2;
    float specialShootCooldownTime2 = 5f;
    float originalSpecialShootCooldownTime2;

    bool moveInZCooldown2;
    float moveInZCooldownTime2 = 0.3f;
    float originalMoveInZCooldownTime2;

    bool invertedMovement = false;
    [SerializeField]
    float invertedMovementDuration;

    bool speedPowerup = false;

    [SerializeField]
    float speedPowerupDuration;
    [SerializeField]
    float speedIncrease;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        canChangeLayer = true;
        canShoot = true;
        canShootSpecial = true;

        canChangeLayer2 = true;
        canShoot2 = true;
        canShootSpecial2 = true;

        originalShootCooldownTime2 = shootCooldownTime2;
        originalSpecialShootCooldownTime2 = specialShootCooldownTime2;
        originalMoveInZCooldownTime2 = moveInZCooldownTime2;
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
            TimerBehaviour.instance.shootCooldown = true;
            ShootProjectile();
        }

        if (Input.GetButton("Fire1") && canShootSpecial && !gameObject.transform.CompareTag("Selector"))
        {
            canShootSpecial = false;
            TimerBehaviour.instance.specialShootCooldown = true;
            ShootSpecialProjectile();
        }
    }

    void Movement()
    {
        float wantedSpeed = speedPowerup ? speedIncrease : playerSpeed;
        int direction = invertedMovement ? -1 : 1;

        Vector3 gameObjectPosition = gameObject.transform.position;
        gameObjectPosition.x = Mathf.Clamp(gameObjectPosition.x + (Input.GetAxis("Horizontal")) * direction * wantedSpeed * Time.deltaTime, -playerBounds, playerBounds);
        transform.position = gameObjectPosition;
        Quaternion playerRotation = gameObject.transform.rotation;
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            playerRotation.z = -15f;
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            playerRotation.z = 15f;
        }
    }

    void Movement2()
    {
        Vector3 gameObjectPosition = gameObject.transform.position;
        gameObjectPosition.x = Mathf.Clamp(gameObjectPosition.x + (Input.GetAxis("Horizontal2")) * playerSpeed * Time.deltaTime, -playerBounds, playerBounds);
        transform.position = gameObjectPosition;
        Quaternion playerRotation = gameObject.transform.rotation;
        if (Input.GetAxisRaw("Horizontal2") == 1)
        {
            playerRotation.z = -15f;
        }
        if (Input.GetAxisRaw("Horizontal2") == -1)
        {
            playerRotation.z = 15f;
        }
    }

    void LayerChange()
    {
        if (wantLayerUp)
        {
            if (gameObject.transform.position.z + spaceBetweenLayers <= initialZPosition + (spaceBetweenLayers * layersAmount))
            {
                wantedLayer = spaceBetweenLayers;
                wantedRotation = goUpRotationAmount;
                limitReached = false;
            }
            else
            {
                limitReached = true;
            }
        }
        else if (!wantLayerUp)
        {
            if (gameObject.transform.position.z - spaceBetweenLayers >= initialZPosition)
            {
                wantedLayer = -spaceBetweenLayers;
                wantedRotation = goDownRotationAmount;
                limitReached = false;
            }
            else
            {
                limitReached = true;
            }
        }

        if (!limitReached)
        {
            LeanTween.rotateX(gameObject, wantedRotation, playerForwardTime).setEase(playerForwardAnim).setOnComplete(() =>
            {
                LeanTween.moveZ(gameObject, gameObject.transform.position.z + (wantedLayer), playerRotationTime).setEase(playerRotationAnim).setOnComplete(() =>
                {
                    LeanTween.rotateX(gameObject, normalPlayerRotation, playerForwardTime).setEase(playerForwardAnim).setOnComplete(() =>
                    {
                        TimerBehaviour.instance.moveInZCooldown = true;
                    });
                });
            });
        }
        else
        {
            canChangeLayer = true;
        }
    }

    void LayerChange2()
    {
        if (wantLayerUp)
        {
            if (gameObject.transform.position.z + spaceBetweenLayers <= initialZPosition + (spaceBetweenLayers * layersAmount))
            {
                wantedLayer = spaceBetweenLayers;
                wantedRotation = goUpRotationAmount;
                limitReached = false;
            }
            else
            {
                limitReached = true;
            }
        }
        else if (!wantLayerUp)
        {
            if (gameObject.transform.position.z - spaceBetweenLayers >= initialZPosition)
            {
                wantedLayer = -spaceBetweenLayers;
                wantedRotation = goDownRotationAmount;
                limitReached = false;
            }
            else
            {
                limitReached = true;
            }
        }

        if (!limitReached)
        {
            LeanTween.rotateX(gameObject, wantedRotation, playerForwardTime).setEase(playerForwardAnim).setOnComplete(() =>
            {
                LeanTween.moveZ(gameObject, gameObject.transform.position.z + (wantedLayer), playerRotationTime).setEase(playerRotationAnim).setOnComplete(() =>
                {
                    LeanTween.rotateX(gameObject, normalPlayerRotation, playerForwardTime).setEase(playerForwardAnim).setOnComplete(() =>
                    {
                        moveInZCooldown2 = true;
                    });
                });
            });
        }
        else
        {
            canChangeLayer2 = true;
        }
    }

    void ShootProjectile()
    {
        Vector3 playerPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + addedYToBullet, gameObject.transform.position.z + addedZToBullet);
        Instantiate(projectilePrefab, playerPosition, Quaternion.identity);
    }

    void ShootSpecialProjectile()
    {
        Vector3 playerPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + addedYToBullet, gameObject.transform.position.z + addedZToBullet);
        Instantiate(specialProjectilePrefab, playerPosition, Quaternion.identity);
    }

    public void StartInvertedDebuff()
    {
        StartCoroutine(InvertedDebuff());
    }

    IEnumerator InvertedDebuff()
    {
        invertedMovement = true;
        yield return new WaitForSeconds(invertedMovementDuration);
        invertedMovement = false;
        yield return null;
    }

    public void StartSpeed()
    {
        StartCoroutine(PowerupSpeed());
    }

    IEnumerator PowerupSpeed()
    {
        speedPowerup = true;
        yield return new WaitForSeconds(speedPowerupDuration);
        speedPowerup = false;
        yield return null;
    }
}
