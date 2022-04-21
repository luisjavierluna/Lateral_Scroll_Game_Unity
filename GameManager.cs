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

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
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

        }
        if (newGameState == GameState.inGame)
        {

        }
        if (newGameState == GameState.gameOver)
        {

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
}
