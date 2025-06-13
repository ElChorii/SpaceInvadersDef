using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBehaviour : MonoBehaviour
{
    public static TimerBehaviour instance;

    [HideInInspector] public bool shootCooldown;
    [SerializeField] float shootCooldownTime;

    [HideInInspector] public bool specialShootCooldown;
    [SerializeField] float specialShootCooldownTime;

    [HideInInspector] public bool moveInZCooldown;
    [SerializeField] float moveInZCooldownTime;

    [HideInInspector] public bool betweenLoadingAnimation;
    [SerializeField] float timeBetweenLoadingAnimation;

    [HideInInspector] public bool betweenLevelAnimation;
    [SerializeField] float betweenLevelDuration;

    float originalShootCooldownTime;
    float originalSpecialShootCooldownTime;
    float originalMoveInZCooldownTime;
    float counterTimeBetweenLoadingAnimation = 0f;
    float counterTimeBetweenLevel = 0f;

    [HideInInspector] public int loadingAnimationID = 0;

    //Player2
    [HideInInspector] public bool shootCooldown2;
    [HideInInspector] public bool specialShootCooldown2;
    float originalShootCooldownTime2;
    float originalSpecialShootCooldownTime2;
    float originalMoveInZCooldownTime2;

    [SerializeField] float shootCooldownTime2;
    [SerializeField] float specialShootCooldownTime2;
    [SerializeField] float moveInZCooldownTime2;
    [HideInInspector] public bool moveInZCooldown2;

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

    private void Start()
    {
        //Player1
        shootCooldown = false;
        originalShootCooldownTime = shootCooldownTime;
        originalSpecialShootCooldownTime = specialShootCooldownTime;
        originalMoveInZCooldownTime = moveInZCooldownTime;

        //Player2
        shootCooldown2 = false;
        originalShootCooldownTime2 = shootCooldownTime;
        originalSpecialShootCooldownTime2 = specialShootCooldownTime;
        originalMoveInZCooldownTime2 = moveInZCooldownTime;
        shootCooldownTime2 = shootCooldownTime;
        specialShootCooldownTime2 = specialShootCooldownTime;
        moveInZCooldownTime2 = moveInZCooldownTime;
    }

    void Update()
    {
        //Player 1 and Selector
        if (shootCooldown)
        {
            shootCooldownTime -= Time.deltaTime;
            if (shootCooldownTime <= 0)
            {
                shootCooldown = false;
                PlayerBehaviour.instance.canShoot = true;
                shootCooldownTime = originalShootCooldownTime;
            }
        }

        if (specialShootCooldown)
        {
            specialShootCooldownTime -= Time.deltaTime;
            if (specialShootCooldownTime <= 0)
            {
                specialShootCooldown = false;
                PlayerBehaviour.instance.canShootSpecial = true;
                specialShootCooldownTime = originalSpecialShootCooldownTime;
            }
        }

        if (moveInZCooldown)
        {
            moveInZCooldownTime -= Time.deltaTime;
            if (moveInZCooldownTime <= 0)
            {
                moveInZCooldown = false;
                PlayerBehaviour.instance.canChangeLayer = true;
                moveInZCooldownTime = originalMoveInZCooldownTime;
            }
        }

        //Player2
        

        if (betweenLoadingAnimation)
        {
            counterTimeBetweenLoadingAnimation += Time.deltaTime;
            if (counterTimeBetweenLoadingAnimation >= timeBetweenLoadingAnimation)
            {
                betweenLoadingAnimation = false;
                counterTimeBetweenLoadingAnimation = 0f;
                if (loadingAnimationID == 1)
                {
                    MainMenuBehaviour.instance.StartGameOUT();
                }
                else if (loadingAnimationID == 2)
                {
                    MainMenuBehaviour.instance.LoadLevelOUT();
                }
            }
        }

        if (betweenLevelAnimation)
        {
            counterTimeBetweenLevel += Time.deltaTime;
            if (counterTimeBetweenLevel >= betweenLevelDuration)
            {
                betweenLevelAnimation = false;
                counterTimeBetweenLevel = 0f;
                LevelUIBehaviour.instance.NextLevelOUT();
            }
        }
    }
}
