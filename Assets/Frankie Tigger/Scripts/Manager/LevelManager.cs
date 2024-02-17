using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject[] levels;
    public int levelIndex;


    [Header(" Debug ")]
    [SerializeField] private bool preventSpawning;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainCamera;
    // Start is called before the first frame update
   
    private void Awake()
    {
        LoadData();

        if (!preventSpawning)
            SpawnLevel();

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.LevelComplete:
                levelIndex++;
                SaveData();
                break;
        }
    }
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnLevel()
    {
        if (levelIndex >= levels.Length)
            levelIndex = 0;

        GameObject levelInstance = Instantiate(levels[levelIndex], transform);

        StartCoroutine(EnableLevelCoroutine());

        IEnumerator EnableLevelCoroutine()
        {
            yield return new WaitForSeconds(Time.deltaTime);
            levelInstance.SetActive(true);

            if (levelIndex == 4 || levelIndex==5 || levelIndex==6) // Dördüncü seviye
            {
                // Oyuncu ve oyuncu kamerasýný devre dýþý býrak
                DisablePlayerAndCamera();
            }
        }
    }

    private void DisablePlayerAndCamera()
    {
        // Oyuncu ve oyuncu kamerasýný devre dýþý býrak
        if (player != null)
        {
            player.SetActive(false);
        }

        if (mainCamera != null)
        {
            mainCamera.SetActive(false);
        }
    }

    private void LoadData()
    {
        levelIndex = PlayerPrefs.GetInt("Level");
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("Level", levelIndex);
    }

    public void sonrakilevel()
    {
        levelIndex++; // Sonraki seviyeye geç
        SaveData(); // Seviye indeksini kaydet
        SceneManager.LoadScene(levelIndex % levels.Length); // B



    }
}
