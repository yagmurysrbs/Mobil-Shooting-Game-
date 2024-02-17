using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public enum GameState { Menu, Game, LevelComplete, Gameover }
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header(" Settings ")]
    private GameState gameState;

    [Header(" Actions ")]
    public static Action<GameState> onGameStateChanged;

    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        
        SetGameState(GameState.Menu);
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }

    public void NextLevel()
    {
        // Tell the level manager to increase the level index
        SceneManager.LoadScene(0);
    }
}
