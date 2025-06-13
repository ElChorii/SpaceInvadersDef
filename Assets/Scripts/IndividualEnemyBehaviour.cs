using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IndividualEnemyBehaviour : MonoBehaviour
{
    [SerializeField] float[] shootInterval;
    [SerializeField] float shootTimer;

    [SerializeField] GameObject bulletGameObject;

    bool checkBounds = true;

    float gameOverBound = 3f;

    private void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= Random.Range(shootInterval[0], shootInterval[1]))
        {
            shootTimer = 0f;

            if (IsBottomMost())
            {
                if (Random.Range(1, 7) == 1)
                {
                    Shoot();
                }
            }
        }

        if (gameObject.transform.position.y <= gameOverBound && checkBounds)
        {
            checkBounds = false;
            LevelUIBehaviour.instance.GameOverIN();
        }
    }

    private bool IsBottomMost()
    {
        Vector3 position = transform.position;
        List<List<GameObject>> columnList = null;

        columnList = EnemiesBehaviour.instance.enemiesList1;


        if (columnList == null) return false;

        foreach (var column in columnList)
        {
            if (column.Contains(gameObject))
            {
                foreach (var enemy in column)
                {
                    if (enemy.activeSelf && enemy.transform.position.y < position.y)
                    {
                        return false;
                    }
                }
                break;
            }
        }
        return true;
    }

    private void Shoot()
    {
        if (EnemiesBehaviour.instance.mustMove)
        {
            Instantiate(bulletGameObject, gameObject.transform.position, Quaternion.identity);
        }
    }
}
