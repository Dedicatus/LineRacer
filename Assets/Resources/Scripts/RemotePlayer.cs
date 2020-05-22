using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RemotePlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    public enum PlayerOrder { Player1, Player2, Player3, Player4 };

    [SerializeField] private PlayerOrder order;

    [Header("RemoteDebug")]
    [SerializeField] private bool forward;
    [SerializeField] private bool backward;
    [SerializeField] private bool left;
    [SerializeField] private bool right;
    [SerializeField] GameObject[] playerList;
    private Player myPlayer;
    private PhotonView myPhotonView;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = gameObject.GetComponent<Player>();
        playerList = GameObject.FindGameObjectsWithTag("Player");
        myPhotonView = gameObject.GetComponent<PhotonView>();
        createHalo();
        forward = false;
        backward = false;
        left = false;
        right = false;
    }

    void createHalo()
    {
        foreach (GameObject player in playerList)
        {
            Player myPlayerScript = player.GetComponent<Player>();
            if (order == PlayerOrder.Player1)
            { 
                if(myPlayerScript.getOrder() == Player.PlayerOrder.Player1)
                    Instantiate(Resources.Load(Path.Combine("Prefabs", "Halo1")), myPlayerScript.transform.position - new Vector3(0f,0.4f,0f), Quaternion.identity);
            }
            if (order == PlayerOrder.Player2)
            {
                if (myPlayerScript.getOrder() == Player.PlayerOrder.Player2)
                    Instantiate(Resources.Load(Path.Combine("Prefabs", "Halo1")), myPlayerScript.transform.position - new Vector3(0f, 0.4f, 0f), Quaternion.identity);
            }
            if (order == PlayerOrder.Player3)
            {
                if (myPlayerScript.getOrder() == Player.PlayerOrder.Player3)
                    Instantiate(Resources.Load(Path.Combine("Prefabs", "Halo2")), myPlayerScript.transform.position - new Vector3(0f, 0.4f, 0f), Quaternion.identity);
            }
            if (order == PlayerOrder.Player4)
            {
                if (myPlayerScript.getOrder() == Player.PlayerOrder.Player4)
                    Instantiate(Resources.Load(Path.Combine("Prefabs", "Halo2")), myPlayerScript.transform.position - new Vector3(0f, 0.4f, 0f), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameObject.FindWithTag("GameController").GetComponent<GameController>().getCurState() == GameController.GameState.Playing)
        {
            if (myPhotonView.IsMine)
            {
                remoteInputHandler();
            }
        }
    }

    private void remoteInputHandler()
    {

        //Keyboard
        if (Input.GetKey(KeyCode.W))
        {
            forward = true;
        }
        else
        {
            forward = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            backward = true;
        }
        else
        {
            backward = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            left = true;
        }
        else
        {
            left = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            right = true;
        }
        else
        {
            right = false;
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(forward);
            stream.SendNext(backward);
            stream.SendNext(left);
            stream.SendNext(right);
        }
        else
        {
            forward = (bool)stream.ReceiveNext();
            backward = (bool)stream.ReceiveNext();
            left = (bool)stream.ReceiveNext();
            right = (bool)stream.ReceiveNext();
        }
    }

    public bool getForward()
    {
        return forward;
    }

    public bool getBackward()
    {
        return backward;
    }

    public bool getLeft()
    {
        return left;
    }

    public bool getRight()
    {
        return right;
    }

    public PlayerOrder getOrder()
    {
        return order;
    }
}
