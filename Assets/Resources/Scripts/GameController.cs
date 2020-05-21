using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState { Preparation, Playing, Result }
    public enum Team { Team1, Team2 }

    private GameState myState;

    [SerializeField] int team1Score;
    [SerializeField] int team2Score;

    [SerializeField] float preparationTime = 3.0f;
    [SerializeField] float preparationTimer;

    [SerializeField] float gameTime = 30.0f;
    [SerializeField] float gameTimer;

    private InGameUIController myInGameUIController;
    private AudioController myAudioController;

    // Start is called before the first frame update
    void Start()
    {
        myInGameUIController = GameObject.FindWithTag("System").transform.Find("InGameUIController").GetComponent<InGameUIController>();
        myAudioController = GameObject.FindWithTag("System").transform.Find("AudioPlayer").GetComponent<AudioController>();

        myState = GameState.Preparation;
        team1Score = 0;
        team2Score = 0;
        preparationTimer = preparationTime;
        gameTimer = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch (myState)
        {
            case GameState.Preparation:
                preparationTimer -= Time.deltaTime;
                if (preparationTimer < 0) { startGame(); }
                break;
            case GameState.Playing:
                gameTimer -= Time.deltaTime;
                if (gameTimer < 0) { endGame(); }
                break;
        }
    }

    public void startGame()
    {
        if (myState == GameState.Preparation)
        {
            myState = GameState.Playing;
            preparationTimer = preparationTime;
        }
    }

    public void endGame()
    {
        if (myState == GameState.Playing)
        {
            myAudioController.playMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            myAudioController.playMusic.release();

            if (team1Score == team2Score)
            {
                myInGameUIController.showOvertime();
            }
            else
            {
                myState = GameState.Result;
                if (team1Score > team2Score) { 
                    
                    myInGameUIController.showResult(1);
                    myAudioController.PlayEndSound(1);

                }
                else { 
                    myInGameUIController.showResult(2);
                    myAudioController.PlayEndSound(2);
                }
            }
        }
    }

    public GameState getCurState()
    {
        return myState;
    }

    public float getGameTimer()
    {
        return gameTimer;
    }

    public void addScore(Team team, int score)
    {
        if (team1Score == 0 && team2Score == 0)
        {
            myAudioController.playFirstSheep.start();
            myAudioController.playFirstSheep.release();
        }

        myAudioController.playScore.start();
        myAudioController.playScore.release();
        
        switch (team)
        {
            case Team.Team1:
                team1Score += score;
                myInGameUIController.updateScore(team, team1Score);              
                break;
            case Team.Team2:
                team2Score += score;
                myInGameUIController.updateScore(team, team2Score);
                break;
            default:
                break;
        }
    }
}
