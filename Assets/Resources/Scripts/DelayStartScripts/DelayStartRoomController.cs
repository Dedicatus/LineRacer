using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DelayStartRoomController : MonoBehaviourPunCallbacks
{
    //scene navigation index
    [SerializeField]
    private int waitingRoomSceneIndex;  

    public override void OnEnable()
    {
        //register to callback functions
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom() //Callback function for when we successfully create or join a room.
    {
        // called when player joins the room
        //load into the waiting room scene;
        SceneManager.LoadScene(waitingRoomSceneIndex);
    }

   
}
