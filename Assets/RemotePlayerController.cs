using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemotePlayerController : MonoBehaviour
{
    [SerializeField] GameObject[] playerList;
    // Start is called before the first frame update
    void Start()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient) { return; }

        playerList = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in playerList)
        {
            Player myPlayerScript = player.GetComponent<Player>();
            Rigidbody myPlayerRigidbody = player.GetComponent<Rigidbody>();
            if (!myPlayerScript.getIsMaster())
            {
                if (myPlayerScript.getForward() && myPlayerScript.getRight())
                {
                    player.transform.rotation = Quaternion.Euler(0, 45, 0);
                    myPlayerRigidbody.MovePosition(transform.position + transform.forward * myPlayerScript.getMoveSpeed() * Time.fixedDeltaTime);
                    Debug.Log("1");
                }
                else if (myPlayerScript.getForward() && myPlayerScript.getLeft())
                {
                    transform.rotation = Quaternion.Euler(0, 315, 0);
                    myPlayerRigidbody.MovePosition(transform.position + transform.forward * myPlayerScript.getMoveSpeed() * Time.fixedDeltaTime);
                    Debug.Log("2");
                }
                else if (myPlayerScript.getBackward() && myPlayerScript.getRight())
                {
                    transform.rotation = Quaternion.Euler(0, 135, 0);
                    myPlayerRigidbody.MovePosition(transform.position + transform.forward * myPlayerScript.getMoveSpeed() * Time.fixedDeltaTime);
                    Debug.Log("3");
                }
                else if (myPlayerScript.getBackward() && myPlayerScript.getLeft())
                {
                    transform.rotation = Quaternion.Euler(0, 225, 0);
                    myPlayerRigidbody.MovePosition(transform.position + transform.forward * myPlayerScript.getMoveSpeed() * Time.fixedDeltaTime);
                    Debug.Log("4");
                }
                else if (myPlayerScript.getForward())
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    myPlayerRigidbody.MovePosition(transform.position + transform.forward * myPlayerScript.getMoveSpeed() * Time.fixedDeltaTime);
                    Debug.Log("5");
                }
                else if (myPlayerScript.getBackward())
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    myPlayerRigidbody.MovePosition(transform.position + transform.forward * myPlayerScript.getMoveSpeed() * Time.fixedDeltaTime);
                    Debug.Log("6");
                }
                else if (myPlayerScript.getLeft())
                {
                    transform.rotation = Quaternion.Euler(0, 270, 0);
                    myPlayerRigidbody.MovePosition(transform.position + transform.forward * myPlayerScript.getMoveSpeed() * Time.fixedDeltaTime);
                    Debug.Log("7");
                }

                else if (myPlayerScript.getRight())
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    myPlayerRigidbody.MovePosition(transform.position + transform.forward * myPlayerScript.getMoveSpeed() * Time.fixedDeltaTime);
                    Debug.Log("8");
                }
            }
        }
    }
}
