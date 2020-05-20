using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemotePlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    public enum PlayerOrder { Player1, Player2, Player3, Player4 };

    private PlayerOrder order;

    [Header("RemoteDebug")]
    [SerializeField] private bool forward;
    [SerializeField] private bool backward;
    [SerializeField] private bool left;
    [SerializeField] private bool right;

    private Player myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = gameObject.GetComponent<Player>();
        forward = false;
        backward = false;
        left = false;
        right = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("GameController").GetComponent<GameController>().getCurState() == GameController.GameState.Playing)
        {
            remoteInputHandler();
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
