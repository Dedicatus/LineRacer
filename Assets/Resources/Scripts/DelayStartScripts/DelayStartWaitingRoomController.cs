using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DelayStartWaitingRoomController : MonoBehaviourPunCallbacks
{
    // photon view for sending rpc that updates the timer
    private PhotonView myPhotonView;

    // Scene navigation indexes
    [SerializeField]
    private int multiplayerSceneIndex;  //Number for the build index to the multiplay scene.
    [SerializeField]
    private int menuSceneIndex;

    //number of players in the room out of the total room size
    private int playerCount;
    private int roomSize;
  

    // UI text varibles
    [SerializeField]
    private Text roomCountDisplay;

    // bool values
    private bool readyToStart;
    private bool startingGame;

    // Start is called before the first frame update
    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        PlayerCountUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        WaitingForMorePlayers();
    }

    void PlayerCountUpdate()
    {
        //update player count when players join the room
        // displays player count
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        roomCountDisplay.text = "Waiting for " + (roomSize - playerCount) + " players...";
        if (playerCount == roomSize)
        {
            readyToStart = true;
        }
        else {
            readyToStart = false;
        }


    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        //called whenver a new player joins the room
        PlayerCountUpdate();
        
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        PlayerCountUpdate();
    }

    void WaitingForMorePlayers() 
    {
        //when thers is enough players, start the game.
        if (readyToStart) {
            if (startingGame) { return; }
           
            StartGame();
            
        }
    }

    void StartGame() {
        // Multiplayer scene is loaded to start the game 
        startingGame = true;
        if (!PhotonNetwork.IsMasterClient) {
            return;
        }

        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(multiplayerSceneIndex);

    }

    public void DelayCancel() {
        // cancel button
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }
}
