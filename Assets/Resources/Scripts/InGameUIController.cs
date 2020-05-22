using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUIController : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private Text team1ScoreText;
    [SerializeField] private Text team2ScoreText;
    [SerializeField] private Text pingText;

    [SerializeField] private GameObject overtimeText;
    [SerializeField] private GameObject resultBackground;
    [SerializeField] private GameObject returnButton;
    [SerializeField] private GameObject team1Win;
    [SerializeField] private GameObject team2Win;

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
        if (PhotonNetwork.OfflineMode == false) pingText.text = PhotonNetwork.GetPing().ToString();
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

    public void showResult(int team)
    {
        overtimeText.SetActive(false);
        resultBackground.SetActive(true);
        returnButton.SetActive(true);

        switch (team)
        {
            case 1:
                team1Win.SetActive(true);
                break;
            case 2:
                team2Win.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void showOvertime()
    {
        overtimeText.SetActive(true);
    }

    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
