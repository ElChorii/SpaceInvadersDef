using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public static MenuScript instance;
    
    [HideInInspector] public GameObject currentSpaceship;
    [HideInInspector] public string currentSpaceshipDEBUG;

    [SerializeField] private GameObject[] allSpaceShips;

    [SerializeField] private Vector3 playerSelectorSpawnPos;

    [SerializeField] private GameObject startMenuCanvas;


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

        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        int randomShipID = Random.Range(0, allSpaceShips.Length);

        Instantiate(allSpaceShips[randomShipID], playerSelectorSpawnPos, allSpaceShips[randomShipID].transform.rotation);

        startMenuCanvas.SetActive(false);
    }

    public void LoadScene(GameObject selectedSpaceship)
    {
        currentSpaceship = selectedSpaceship;
        SceneManager.LoadScene("Game");
    }

    public void LoadSceneDEBUG(string selectedSpaceship)
    {
        currentSpaceshipDEBUG = selectedSpaceship;
        SceneManager.LoadScene("Game");
    }
}
