using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Trap : MonoBehaviour
{
    public float stunTime;
    [Header("StatusDebug")]
    [SerializeField]
    private float coolDownTimer;
    [SerializeField]
    private float coolDownTime;
    [SerializeField]
    private bool isCoolDown;
    [SerializeField]
    private bool isActive;

    private GameObject curPlayer;
    private AudioController myAudioController;
    private Vector3 originPosition;
    private Animator myAnimator;
    private PhotonView myPhotonView;
    private GameObject[] players;
    private GameObject player1;
    private GameObject player2;
    private GameObject player3;
    private GameObject player4;

    // Start is called before the first frame update
    void Start()
    {

        players = GameObject.FindGameObjectsWithTag("Player");
       
        myPhotonView = GetComponent<PhotonView>();
        
        myAnimator = GetComponent<Animator>();
        originPosition = this.transform.position;
        myAudioController = GameObject.FindWithTag("System").transform.Find("AudioPlayer").GetComponent<AudioController>();

        isCoolDown = false;
        isActive = true;
        coolDownTimer = 0;
        coolDownTime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        AssignPlayer();
        if (isCoolDown) {
            coolDownTimer += Time.deltaTime;
            if (coolDownTime <= coolDownTimer) {
                isCoolDown = false;
                isActive = true;
                coolDownTimer = 0;
                myAnimator.SetBool("isIdle", true);
            }
        }
    }


    private void AssignPlayer() {

        if (player1 == null || player2 == null || player3 == null || player4 == null)
        {
            foreach (GameObject player in players)
            {
                Player myPlayerScript = player.GetComponent<Player>();
                switch (myPlayerScript.getOrder())
                {
                    case Player.PlayerOrder.Player1:
                        player1 = player;
                        break;
                    case Player.PlayerOrder.Player2:
                        player2 = player;
                        break;
                    case Player.PlayerOrder.Player3:
                        player3 = player;
                        break;
                    case Player.PlayerOrder.Player4:
                        player4 = player;
                        break;

                }
            }
        }


    }
    public void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Player" && isActive)
        {
            if (!PhotonNetwork.IsMasterClient) { return; }
            other.gameObject.GetComponent<Player>().isStun = true;
            other.gameObject.GetComponent<Player>().stunTime = stunTime;
            myPhotonView.RPC("RPC_PlayAnimation", RpcTarget.All, 1, Translate(other.transform.GetComponent<Player>().getOrder()));
      }
        
    }

    public void OnTriggerExit(Collider other)
    {
    
        
        
          
        if (other.gameObject == curPlayer && !isCoolDown)
            {
                if (!PhotonNetwork.IsMasterClient) { return; }
                Debug.Log("3333333");
                myPhotonView.RPC("RPC_PlayAnimation", RpcTarget.All, 2, Translate(other.transform.GetComponent<Player>().getOrder())); ;

            }
       
    }

    private int Translate(Player.PlayerOrder order) {
        int num = 0;
        switch (order) {
            case Player.PlayerOrder.Player1:
                num = 1;
                break;
            case Player.PlayerOrder.Player2:
                num = 2;
                break;
            case Player.PlayerOrder.Player3:
                num = 3;
                break;
            case Player.PlayerOrder.Player4:
                num = 4;
                break;

        }

        return num;
    }
   

    [PunRPC]
    public void RPC_PlayAnimation(int num, int order) {
        if (num == 1)
        {
            Vector3 p = new Vector3();
            Debug.Log("1111111111");
            switch (order) {
                case 1:
                     p = player1.transform.position;
                    break;
                case 2:
                     p = player2.transform.position;
                    break;
                case 3:
                     p = player3.transform.position;
                    break;
                case 4:
                     p = player4.transform.position;
                    break;

            }
            
            this.transform.position = new Vector3(p.x, originPosition.y, p.z);
            myAnimator.SetBool("isIdle", false);
            myAnimator.SetBool("isOpen", false);
            myAnimator.SetBool("isClose", true);
            myAudioController.playDizzy.start();
            isActive = false;
            curPlayer = transform.gameObject;

        }
        else {
            Debug.Log("2222");
            myAnimator.SetBool("isClose", false);
            myAnimator.SetBool("isOpen", true);
            myAnimator.SetBool("isIdle", true);
            this.transform.position = originPosition;
            isCoolDown = true;
        }
    
    }


}
