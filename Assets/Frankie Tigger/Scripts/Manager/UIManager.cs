using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

    [Header(" Panels ")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject bitirmePanel;
    // Start is called before the first frame update
    private LevelManager levelManager;
    private int index;
    
    private void Awake()
    {
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
            case GameState.Menu:
                    menuPanel.SetActive(true);
                    gamePanel.SetActive(false);
                    levelCompletePanel.SetActive(false);
                    gameoverPanel.SetActive(false);
                break;

            case GameState.Game:
                menuPanel.SetActive(false);
                gamePanel.SetActive(true);
                break;

            case GameState.LevelComplete:
                levelCompletePanel.SetActive(true);
                gamePanel.SetActive(false);
              
               
                break;

            case GameState.Gameover:
                gamePanel.SetActive(false);
                gameoverPanel.SetActive(true);
                break;
        }
    }
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>(); // LevelManager bileþenini bul
       
            int index = levelManager.levelIndex; // levelIndex deðerine eriþ
                                                 // Burada levelIndex deðerini kullanabilirsiniz
        if (index == 6) // Dördüncü seviye
        {

            menuPanel.SetActive(false);
            bitirmePanel.SetActive(true);

        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonCallback()
    {
        GameManager.instance.SetGameState(GameState.Game);
    }

    public void RetryButtonCallback()
    {
        GameManager.instance.Retry();
    }

    public void NextButtonCallback()
    {
        GameManager.instance.NextLevel();
    }

    public void  gameoverbutton()
    {

        Application.Quit();
    }

  

   

    
}
