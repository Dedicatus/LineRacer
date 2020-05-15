using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Obi;

// This script will be added to any multiplayer scene
public class GameSetupController : MonoBehaviour
{
    private ObiParticleAttachment[] attachments;
    [SerializeField]
    private GameObject ropePrefabTeam1;
    [SerializeField]
    private GameObject ropePrefabTeam2;
    private GameObject rope;
    private GameObject[] players;
    [SerializeField]
    private Transform[] spawnPoints;

    private int roomSize;
    private bool allCreated;
    private bool initialized;


    private void Awake()
    {
        roomSize = 2;

        initialized = false;
        allCreated = false;
        CreatePlayer();
        CreateRope();

    }

    void Start()
    {
       

        attachments = rope.GetComponentsInChildren<ObiParticleAttachment>();
        
    }

    private void CreatePlayer()
    {

        Debug.Log("Creating Player");
        switch (PhotonNetwork.LocalPlayer.ActorNumber) {
            case 1:
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player1"), spawnPoints[0].position, Quaternion.identity);
                break;
            case 2:
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player2"), spawnPoints[1].position, Quaternion.identity);
                break;
            case 3:
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player3"), spawnPoints[2].position, Quaternion.identity);
                break;
            case 4:
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player4"), spawnPoints[3].position, Quaternion.identity);
                break;

        }

        
    }


    private void CreateRope() {
        Debug.Log("Creating Rope");
        switch (PhotonNetwork.LocalPlayer.ActorNumber)
        {
            case 1:
                rope = GameObject.Instantiate(ropePrefabTeam1, new Vector3(0f, 1.5f, 0f), Quaternion.identity);
                break;
            case 2:
                rope = GameObject.Instantiate(ropePrefabTeam1, new Vector3(0f, 1.5f, 0f), Quaternion.identity);
                break;
            case 3:
                rope = GameObject.Instantiate(ropePrefabTeam2, new Vector3(0f, 1.5f, 0f), Quaternion.identity);
                break;
            case 4:
                rope = GameObject.Instantiate(ropePrefabTeam2, new Vector3(0f, 1.5f, 0f), Quaternion.identity);
                break;

        }
    }

    private void Update()
    {
        if (!initialized)
        {
            if (!allCreated)
            {
                // CHECK IF ALL PLAYERS CREATED
                if (GameObject.FindGameObjectsWithTag("Player").Length == roomSize)
                {
                    allCreated = true;
                   
                }
            }
            else
            {
                //ASSIGN SCENE
                players = GameObject.FindGameObjectsWithTag("Player");
                Debug.Log(players.Length);

                foreach (GameObject player in players) {

                    if (player.GetComponent<PhotonView>().Owner.ActorNumber == 1) {
                        attachments[1].target = player.transform;
                    }
                    if (player.GetComponent<PhotonView>().Owner.ActorNumber == 2)
                    {
                        attachments[0].target = player.transform;
                    }

                }
              //  rope.transform.position = (players[0].gameObject.transform.position + players[1].gameObject.transform.position) / 2;
                //if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                //{
                //    attachments[0].target = players[0].transform;
                //    attachments[1].target = players[1].transform;
                //}
                //else {
                //    attachments[0].target = players[1].transform;
                //    attachments[1].target = players[0].transform;
                //}
                initialized = true;
            }
        }
    }

}
