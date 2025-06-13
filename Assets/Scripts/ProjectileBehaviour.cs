using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] string[] spaceShips;
    [SerializeField] float boundDown;
    [SerializeField] GameObject explosionTrail;
    float projectileSpeed;

    private void Start()
    {
        if (PlayerBehaviour.instance == null)
        {
            projectileSpeed = ShipSelector.instance.projectileSpeed;
        }
        else
        {
            projectileSpeed = PlayerBehaviour.instance.projectileSpeed;
        }
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (projectileSpeed * Time.deltaTime), gameObject.transform.position.z);
        if (gameObject.transform.position.y > 100f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Contains("UFO"))
        {
            if (!collision.transform.parent.gameObject.CompareTag("Parent"))
            {
                collision.transform.parent.gameObject.SetActive(false);
            }
            else if (collision.transform.parent.gameObject.CompareTag("Parent"))
            {
                collision.gameObject.SetActive(false);
            }
            Destroy(this.gameObject);
            Instantiate(explosionTrail, gameObject.transform.position, Quaternion.identity);
            EnemiesBehaviour.instance.enemiesSpeed += EnemiesBehaviour.instance.addedSpeedPerDestroy;
            LivesPointsBehaviour.instance.AddScore(1);
            EnemiesBehaviour.instance.CheckEnemiesAmount();

            PowerupSpawner.instance.SpawnPowerup(transform.position);
        }
        if (collision.gameObject.CompareTag("SpaceShip"))
        {
            for (int i = 0; i < spaceShips.Length; i++)
            {
                if (collision.gameObject.name == spaceShips[i])
                {
                    if (PlayerInformationBehaviour.instance.selectedSpaceShipP1 == -1)
                    {
                        PlayerInformationBehaviour.instance.selectedSpaceShipP1 = i;
                    }
                    else if (PlayerInformationBehaviour.instance.selectedSpaceShipP1 != -1)
                    {
                        PlayerInformationBehaviour.instance.selectedSpaceShipP2 = i;
                        MainMenuBehaviour.instance.LoadLevelIN();
                    }
                }
            }
        }
    }
}
