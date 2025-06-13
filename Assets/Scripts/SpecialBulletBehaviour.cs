using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpecialBulletBehaviour : MonoBehaviour
{
    [SerializeField] float boundDown;
    [SerializeField] LayerMask layer;
    [SerializeField] GameObject explosionTrail;

    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (PlayerBehaviour.instance.projectileSpeed * Time.deltaTime), gameObject.transform.position.z);
        if (gameObject.transform.position.y > boundDown)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Contains("UFO"))
        {
            Instantiate(explosionTrail, gameObject.transform.position, Quaternion.identity);
            AreaDamage();
            Destroy(gameObject);
        }
    }

    void AreaDamage()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale * 15, Quaternion.identity, layer);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag.Contains("UFO"))
            {
                List <GameObject> tempUFOAmount = new List<GameObject>();
                if (!hitColliders[i].transform.parent.gameObject.CompareTag("Parent"))
                {
                    hitColliders[i].transform.parent.gameObject.SetActive(false);
                }
                else if (hitColliders[i].transform.parent.gameObject.CompareTag("Parent"))
                {
                    hitColliders[i].gameObject.SetActive(false);
                }
                tempUFOAmount.Add(hitColliders[i].gameObject);
                EnemiesBehaviour.instance.enemiesSpeed += EnemiesBehaviour.instance.addedSpeedPerDestroy * tempUFOAmount.Count;
                LivesPointsBehaviour.instance.AddScore(tempUFOAmount.Count);
                EnemiesBehaviour.instance.CheckEnemiesAmount();
            }
            i++;
        }
    }
}
