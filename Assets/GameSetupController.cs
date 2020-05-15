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
    private GameObject ropePrefab;
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
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer"), spawnPoints[0].position, Quaternion.identity);
                break;
            case 2:
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer"), spawnPoints[1].position, Quaternion.identity);
                break;
            case 3:
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer"), spawnPoints[2].position, Quaternion.identity);
                break;
            case 4:
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer"), spawnPoints[3].position, Quaternion.identity);
                break;

        }

        
    }


    private void CreateRope() { 
        Debug.Log("Creating Rope");
        rope = GameObject.Instantiate(ropePrefab, new Vector3(0f, 1.5f, 0f), Quaternion.identity);
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
                rope.transform.position = (players[0].gameObject.transform.position + players[1].gameObject.transform.position) / 2;
                attachments[0].target = players[0].transform;
                attachments[1].target = players[1].transform;
                initialized = true;
            }
        }
    }

}
