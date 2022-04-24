using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState currentGameState = GameState.menu;

    public int coinsCollected = 0;

    PlayerController player;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        MenuManager.instance.HideInGameCanvas();
        MenuManager.instance.HideGameOverCanvas();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentGameState != GameState.inGame)
        {
            InGameState();
        }
    }

    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            MenuManager.instance.ShowMenuCanvas();
        }
        if (newGameState == GameState.inGame)
        {
            MenuManager.instance.ShowInGameCanvas();
            MenuManager.instance.HideMenuCanvas();
            MenuManager.instance.HideGameOverCanvas();

            LevelManager.instance.RemoveAllTheLevelBlocks();
            LevelManager.instance.GenerateInitialBlocks();
            
            player.RestartPosition();
        }
        if (newGameState == GameState.gameOver)
        {
            MenuManager.instance.ShowGameOverCanvas();
            MenuManager.instance.HideMenuCanvas();
            MenuManager.instance.HideInGameCanvas();
        }

        currentGameState = newGameState;
    }

    public void MenuState()
    {
        SetGameState(GameState.menu);
    }

    public void InGameState()
    {
        SetGameState(GameState.inGame);
    }

    public void GameOverState()
    {
        SetGameState(GameState.gameOver);
    }

    public void CollectCoin(CollectableItem collectable)
    {
        coinsCollected += collectable.value;
    }
}
