﻿using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
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

    public void OnTriggerEnter(Collider other)
    {
        //if (!PhotonNetwork.IsMasterClient) { return; }
            if (other.gameObject.tag == "Player" && isActive)
            {
            myPhotonView.RPC("RPC_PlayAnimation", RpcTarget.AllBuffered, 1, other);
                isActive = false;
                other.gameObject.GetComponent<Player>().isStun = true;
                other.gameObject.GetComponent<Player>().stunTime = stunTime;
                curPlayer = other.gameObject;
                
                // myAudioController.playDizzy.release();

            }
        
    }

    public void OnTriggerExit(Collider other)
    {

       // if (PhotonNetwork.IsMasterClient)
       //{
            if (other.gameObject == curPlayer && !isCoolDown)
            {

                myPhotonView.RPC("RPC_PlayAnimation", RpcTarget.AllBuffered, 2, other);


                isCoolDown = true;
            }
       // }
    }


    [PunRPC]
    public void RPC_PlayAnimation(int num, Transform transform) {
        if (num == 1)
        {
            myAnimator.SetBool("isIdle", false);
            myAnimator.SetBool("isOpen", false);
            myAnimator.SetBool("isClose", true);
            Vector3 p = transform.position;
            this.transform.position = new Vector3(p.x, originPosition.y, p.z);
            myAudioController.playDizzy.start();
        }
        else {
            myAnimator.SetBool("isClose", false);
            myAnimator.SetBool("isOpen", true);
            this.transform.position = originPosition;
        }
    
    }


}
