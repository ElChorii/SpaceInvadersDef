using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemiesBehaviour : MonoBehaviour
{
    public static EnemiesBehaviour instance;

    [SerializeField] float initialPosX;
    [SerializeField] float initialPosY;
    public float firstLayerZ;

    [SerializeField] float goDownAmount;

    [SerializeField] float spaceBetweenX;
    [SerializeField] float spaceBetweenY;

    [SerializeField] float columns;
    [SerializeField] float rows;

    public float enemiesSpeed;
    public float addedSpeedPerDestroy;
    [SerializeField] float boundaries;

    [SerializeField] GameObject[] enemiesPrefabs;
    [HideInInspector] public List<List<GameObject>> enemiesList1 = new List<List<GameObject>>();

    [SerializeField] GameObject[] allSpaceShips;
    Vector3 initialPlayer1Pos = new Vector3 (0f, 6f, 0f);
    [SerializeField] GameObject enemiesParent;

    float direction = 1;
    bool collisionDone = false;
    [HideInInspector] public bool mustMove = true;
    float originalEnemiesSpeed;

    GameObject player1;

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
    void Start()
    {
        CreateEnemies(enemiesList1, firstLayerZ, "Enemy");
        player1 = Instantiate(allSpaceShips[PlayerInformationBehaviour.instance.selectedSpaceShipP1], initialPlayer1Pos, Quaternion.identity);
    }

    void Update()
    {
        EnemiesMovement();

        collisionDone = false;
    }

    void CreateEnemies(List<List<GameObject>> currentEnemyList, float layerZ, string namePrefix)
    {
        for (int i = 0; i < columns; i++)
        {
            currentEnemyList.Add(new List<GameObject>());
            for (int j = 0; j < rows; j++)
            {
                Vector3 position = new Vector3(initialPosX + i * spaceBetweenX, initialPosY - j * spaceBetweenY, layerZ);
                GameObject enemy = Instantiate(enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)], position, Quaternion.identity);
                enemy.name = $"{namePrefix}({i},{j})";
                enemy.gameObject.transform.SetParent(enemiesParent.transform);
                currentEnemyList[i].Add(enemy);
            }
        }
    }

    void EnemiesMovement()
    {
        if (mustMove)
        {
            foreach (var enemyList in new List<List<List<GameObject>>> { enemiesList1})
            {
                foreach (var column in enemyList)
                {
                    foreach (var enemy in column)
                    {
                        if (enemy.activeSelf)
                        {
                            enemy.transform.position += Vector3.right * direction * enemiesSpeed * Time.deltaTime;
                        }
                    }
                }
            }
        }
    }

    void MoveEnemiesDown()
    {
        foreach (var enemyList in new List<List<List<GameObject>>> { enemiesList1})
        {
            foreach (var column in enemyList)
            {
                foreach (var enemy in column)
                {
                    if (enemy.activeSelf)
                    {
                        enemy.transform.position += Vector3.down * goDownAmount;
                    }
                }
            }
        }
    }

    public void ChangeDirection()
    {
        if (!collisionDone)
        {
            collisionDone = true;
            direction *= -1;
            MoveEnemiesDown();
        }
    }

    public void CheckEnemiesAmount()
    {
        int count = 0;
        foreach (Transform child in enemiesParent.transform)
        {
            if (child.gameObject.activeSelf)
            {
                count++;
            }
        }
        if (count == 0)
        {
            LevelUIBehaviour.instance.NextLevelIN();
        }
    }

    public void Restart()
    {
        foreach (var enemyList in new List<List<List<GameObject>>> { enemiesList1})
        {
            foreach (var column in enemyList)
            {
                foreach (var enemy in column)
                {
                    Destroy(enemy);
                }
            }
        }
        foreach (var enemyList in new List<List<List<GameObject>>> { enemiesList1})
        {
            enemyList.Clear();
        }
        enemiesSpeed = 1f;
        player1.transform.position = initialPlayer1Pos;
        CreateEnemies(enemiesList1, firstLayerZ, "Enemy");
    }
}
