using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] Text coinText, scoreText, maxScoreText;

    PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            int coin = GameManager.instance.coinsCollected;
            float score = player.GetTravelledDistance();
            float maxScore = PlayerPrefs.GetFloat("maxscore", 0);

            coinText.text = coin.ToString();
            scoreText.text = "Score: " + score.ToString();
            maxScoreText.text = "Max Score: " + maxScore.ToString();
        }
    }
}
