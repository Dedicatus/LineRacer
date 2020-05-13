using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// This script will be added to any multiplayer scene
public class GameSetupController : MonoBehaviour
{
    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer"), new Vector3(0f, 1.5f, 0f), Quaternion.identity);
    }
}
