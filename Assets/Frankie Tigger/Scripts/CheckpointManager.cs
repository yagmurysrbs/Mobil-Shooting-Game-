using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    [Header(" Settings ")]
    private Vector3 lastCheckpointPosition;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        Checkpoint.onInteracted += CheckpointInteractedCallback;

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        Checkpoint.onInteracted -= CheckpointInteractedCallback;

        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.LevelComplete:
                lastCheckpointPosition = Vector3.zero;
                break;
        }
    }

    private void CheckpointInteractedCallback(Checkpoint checkpoint)
    {
        lastCheckpointPosition = checkpoint.GetPosition();
    }

    public Vector3 GetCheckpointPosition()
    {
        return lastCheckpointPosition;
    }

}
