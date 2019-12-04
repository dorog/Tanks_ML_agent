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
        resultText.text = result;
        playerScoreText.text = "" + playerScore;
        aiScoreText.text = "" + aiScore;
        scoreUI.SetActive(true);

        Invoke("NewRound", 3);
    }

    private void NewRound()
    {
        scoreUI.SetActive(false);
        battleManager.StartRound();
    }
}
