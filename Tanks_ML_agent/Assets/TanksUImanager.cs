using UnityEngine;
using UnityEngine.UI;

public class TanksUImanager : MonoBehaviour
{
    private int number;
    public Text msg;
    public AIvsPlayerBattleManager battleManager;

    [Header("Score Settings")]
    public string win = "You win!";
    public string lose = "You lose!";

    public GameObject scoreUI;
    public Text resultText;
    public Text playerScoreText;
    public Text aiScoreText;

    private int playerScore = 0;
    private int aiScore = 0;

    public int maxScore = 5;
    public GameObject gameOver;

    public GameObject game;
    public GameObject mainMenu;

    public void Count(int last)
    {
        number = last;
        Invoke("SetTxt", 2);
    }

    private void SetTxt()
    {
        if(number == 0)
        {
            msg.text = "Goooooo!!!";
            Invoke("Go", 2);
        }
        else
        {
            msg.text = "" + number;
            number--;
            Invoke("SetTxt", 2);
        }
    }

    private void Go()
    {
        msg.text = "";
        battleManager.StartBattle();
    }

    public void PlayerWin()
    {
        playerScore++;
        SetScore(playerScore, aiScore, win);
    }

    public void PlayerLose()
    {
        aiScore++;
        SetScore(playerScore, aiScore, lose);
    }

    private void SetScore(int playerScore, int aiScore, string result)
    {
        if (maxScore == playerScore || maxScore == aiScore)
        {
            gameOver.SetActive(true);
        }
        else
        {
            Invoke("NewRound", 3);
        }

        resultText.text = result;
        playerScoreText.text = "" + playerScore;
        aiScoreText.text = "" + aiScore;
        scoreUI.SetActive(true);
    }

    private void NewRound()
    {
        scoreUI.SetActive(false);
        battleManager.StartRound();
    }

    public void StartNewGame()
    {
        gameOver.SetActive(false);
        scoreUI.SetActive(false);
        playerScore = 0;
        aiScore = 0;

        battleManager.StartRound();
    }

    public void GameOver()
    {
        playerScore = 0;
        aiScore = 0;

        gameOver.SetActive(false);
        scoreUI.SetActive(false);
        mainMenu.SetActive(true);
        game.SetActive(false);
    }
}
