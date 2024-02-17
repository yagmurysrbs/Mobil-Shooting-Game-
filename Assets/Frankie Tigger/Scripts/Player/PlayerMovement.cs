using System.Collections;
using System.Collections.Generic;
using UnityEngine.Splines; 
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{

    enum State { Idle, Run, Warzone,Dead,Salute}

    [Header(" Elements ")]
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private CharacterIK playerIK;
    [SerializeField] private CharacterRagdoll characterRagdoll;


    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float slowMoScale;
    [SerializeField] private Transform enemyTarget;
    private State state;
    private Warzone currentWarzone;

    [Header(" Spline Settings ")]
     private float warzoneTimer;

    [Header(" Actions ")]
    public static Action onEnteredWarzone;
    public static Action onExitedWarzone;
    public static Action onDied;

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
                
                break;

            case GameState.Game:
                StartRunning();
                break;

            //case GameState.LevelComplete:
            //    gamePanel.SetActive(false);
            //    levelCompletePanel.SetActive(true);
            //    break;

            //case GameState.Gameover:
            //    gamePanel.SetActive(false);
            //    gameoverPanel.SetActive(true);
            //    break;
        }
    }

    void Start()
    {
        Application.targetFrameRate = 60;

        state = State.Idle;

        // Place the player at the last checkpoint position (if any)
        transform.position = CheckpointManager.instance.GetCheckpointPosition();

    }
    private void ManageState()
    {
        switch (state)
        {
            case State.Idle:
                break;

            case State.Run:
                Move();
                break;
            case State.Warzone:
                ManageWarzoneState();
                break;


        }
    }

    private void StartRunning()
    {
        state = State.Run;
        playerAnimator.PlayRunAnimation();
    }


    void Update()
    {
        if (GameManager.instance.IsGameState())
            ManageState();
    }

    private void Move()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }



    public void EnteredWarzoneCallback(Warzone warzone)
    {
        if (currentWarzone != null)
            return;

        state = State.Warzone;
        currentWarzone = warzone;


        currentWarzone.StartAnimatingIKTarget();

        warzoneTimer = 0;

        playerAnimator.Play(currentWarzone.GetAnimationToPlay(),currentWarzone.GetAnimatorSpeed());

        Time.timeScale = slowMoScale;
        Time.fixedDeltaTime = slowMoScale / 50;

        playerIK.ConfigureIK(currentWarzone.GetIKTarget());

        onEnteredWarzone?.Invoke();

        Debug.Log("Entered Warzone ! ");
    }


    private void ManageWarzoneState()
    {
        warzoneTimer += Time.deltaTime;

        float splinePercent = Mathf.Clamp01(warzoneTimer / currentWarzone.GetDuration());
        transform.position = currentWarzone.GetPlayerSpline().EvaluatePosition(splinePercent);

        if (splinePercent >= 1)
            TryExitWarzone();
    }

    private void TryExitWarzone()
    {
        Warzone nextWarzone = currentWarzone.GetNextWarzone();

        if (nextWarzone == null)
            ExitWarzone();
        else
        {
            currentWarzone = null;
            EnteredWarzoneCallback(nextWarzone);
        }
    }

    private void ExitWarzone()
    {
        currentWarzone = null;

        state = State.Run;
        playerAnimator.Play("Run", 1);

        Time.timeScale = 1;
        Time.fixedDeltaTime = 1f / 50;

        playerIK.DisableIK();

        onExitedWarzone?.Invoke();
    }
    public Transform GetEnemyTarget()
    {
        return enemyTarget;
    }


    public void TakeDamage()
    {
        state = State.Dead;
        
        characterRagdoll.Ragdollify();

        Time.timeScale = 1;
        Time.fixedDeltaTime = 1f / 50;

        onDied?.Invoke();

        GameManager.instance.SetGameState(GameState.Gameover);
    }


    public void HitFinishLine()
    {
        Debug.Log("Hit finish Line");

        //state = State.Salute;
        // playerAnimator.PlayIdleAnimation();
        playerAnimator.PlaySaluteAnimation();

        GameManager.instance.SetGameState(GameState.LevelComplete);
    }

    public Warzone GetCurrentWarzone()
    {
        return currentWarzone;
    }

}
