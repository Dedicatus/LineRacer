using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private Text team1ScoreText;
    [SerializeField] private Text team2ScoreText;

    private GameController myGameController;

    // Start is called before the first frame update
    void Start()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = ((int)myGameController.getGameTimer()).ToString();
    }

    public void updateScore(GameController.Team team, int score)
    {
        switch (team)
        {
            case GameController.Team.Team1:
                team1ScoreText.text = score.ToString();
                break;
            case GameController.Team.Team2:
                team2ScoreText.text = score.ToString();
                break;
            default:
                break;
        }
    }
}
