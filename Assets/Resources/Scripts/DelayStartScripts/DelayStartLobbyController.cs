﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class DelayStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject delayStartButton;    //button used for creating and joining a game
    [SerializeField]
    private GameObject delayCancelButton;   //button used to stop searching for a game to join
    [SerializeField]
    private int RoomSize;   //Manul set the number of player in the room at one time
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()  //Callback function for when the first connection is established
    {
        PhotonNetwork.AutomaticallySyncScene = true;    //Makes it so whatever scene the master client has
        delayStartButton.SetActive(true);
    }

    public void DelayStart() // Paired to the Delay button
    {
        delayStartButton.SetActive(false);
        delayCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();     //First try to join an existing room.
        Debug.Log("Quick start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)   //Callback function for faild random room joining.
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    private void CreateRoom()   //trying to create our own room.
    {
        Debug.Log("Creating room now");
        int randomRoomNumber = Random.Range(0, 10000);  //creating a random name for the room.
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);   //attempting to create a new room.
        //Debug.Log(randomRoomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)   //callback function for failed room creation.
    {
        Debug.Log("Failed to create room... try again");
        CreateRoom();
    }

    public void DelayCancel()   //Paired to the cancel button. Used to stop looking for a room to join.
    {
        delayCancelButton.SetActive(false);
        delayStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
