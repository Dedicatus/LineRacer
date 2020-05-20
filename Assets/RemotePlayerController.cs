using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemotePlayerController : MonoBehaviour
{
    [SerializeField] GameObject[] playerList;
    [SerializeField] GameObject[] remotePlayerList;

    [SerializeField] GameObject remotePlayer1;
    [SerializeField] GameObject remotePlayer2;
    [SerializeField] GameObject remotePlayer3;
    [SerializeField] GameObject remotePlayer4;

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient || PhotonNetwork.OfflineMode) { return; }

        playerList = GameObject.FindGameObjectsWithTag("Player");

        assignRemotePlayer();
    }

    private void assignRemotePlayer()
    {
        if (remotePlayer1 == null || remotePlayer2 == null || remotePlayer3 == null || remotePlayer4 == null)
        {
            remotePlayerList = GameObject.FindGameObjectsWithTag("RemotePlayer");

            foreach (GameObject remotePlayer in remotePlayerList)
            {
                RemotePlayer myRemotePlayerScript = remotePlayer.GetComponent<RemotePlayer>();

                switch (myRemotePlayerScript.getOrder())
                {
                    case RemotePlayer.PlayerOrder.Player1:
                        remotePlayer1 = remotePlayer;
                        break;
                    case RemotePlayer.PlayerOrder.Player2:
                        remotePlayer2 = remotePlayer;
                        break;
                    case RemotePlayer.PlayerOrder.Player3:
                        remotePlayer3 = remotePlayer;
                        break;
                    case RemotePlayer.PlayerOrder.Player4:
                        remotePlayer4 = remotePlayer;
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient || PhotonNetwork.OfflineMode) { return; }

        assignRemotePlayer();

        foreach (GameObject player in playerList)
        {
            Player myPlayerScript = player.GetComponent<Player>();
            Rigidbody myPlayerRigidbody = player.GetComponent<Rigidbody>();

            RemotePlayer myRemotePlayerScript;

            switch (myPlayerScript.getOrder())
            {
                case Player.PlayerOrder.Player1:
                    myRemotePlayerScript = remotePlayer1.GetComponent<RemotePlayer>();
                    break;
                case Player.PlayerOrder.Player2:
                    myRemotePlayerScript = remotePlayer2.GetComponent<RemotePlayer>();
                    break;
                case Player.PlayerOrder.Player3:
                    myRemotePlayerScript = remotePlayer3.GetComponent<RemotePlayer>();
                    break;
                case Player.PlayerOrder.Player4:
                    myRemotePlayerScript = remotePlayer4.GetComponent<RemotePlayer>();
                    break;
                default:
                    return;
            }

            
            if (myRemotePlayerScript.getForward() && myRemotePlayerScript.getRight())
            {
                player.transform.rotation = Quaternion.Euler(0, 45, 0);
                myPlayerRigidbody.MovePosition(transform.position + transform.forward * myPlayerScript.getMoveSpeed() * Time.fixedDeltaTime);
                Debug.Log("1");
            }
            else if (myRemotePlayerScript.getForward() && myRemotePlayerScript.getLeft())
            {
                transform.rotation = Quaternion.Euler(0, 315, 0);
                myPlayerRigidbody.MovePosition(transform.position + transform.forward * myPlayerScript.getMoveSpeed() * Time.fixedDeltaTime);
                Debug.Log("2");
            }
            else if (myRemotePlayerScript.getBackward() && myRemotePlayerScript.getRight())
            {
                transform.rotation = Quaternion.Euler(0, 135, 0);
                myPlayerRigidbody.MovePosition(transform.position + transform.forward * myPlayerScript.getMoveSpeed() * Time.fixedDeltaTime);
                Debug.Log("3");
            }
            else if (myRemotePlayerScript.getBackward() && myRemotePlayerScript.getLeft())
            {
                transform.rotation = Quaternion.Euler(0, 225, 0);
                myPlayerRigidbody.MovePosition(transform.position + transform.forward * myPlayerScript.getMoveSpeed() * Time.fixedDeltaTime);
                Debug.Log("4");
            }
            else if (myRemotePlayerScript.getForward())
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                myPlayerRigidbody.MovePosition(transform.position + transform.forward * myPlayerScript.getMoveSpeed() * Time.fixedDeltaTime);
                Debug.Log("5");
            }
            else if (myRemotePlayerScript.getBackward())
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                myPlayerRigidbody.MovePosition(transform.position + transform.forward * myPlayerScript.getMoveSpeed() * Time.fixedDeltaTime);
                Debug.Log("6");
            }
            else if (myRemotePlayerScript.getLeft())
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
                myPlayerRigidbody.MovePosition(transform.position + transform.forward * myPlayerScript.getMoveSpeed() * Time.fixedDeltaTime);
                Debug.Log("7");
            }

            else if (myRemotePlayerScript.getRight())
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                myPlayerRigidbody.MovePosition(transform.position + transform.forward * myPlayerScript.getMoveSpeed() * Time.fixedDeltaTime);
                Debug.Log("8");
            }
        }
    }
}
