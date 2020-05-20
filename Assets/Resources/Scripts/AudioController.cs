using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AudioController : MonoBehaviour
{
     private AudioSource audioSource;
    [SerializeField] 
    private AudioClip[] audioClips;
    private PhotonView myPhotonView;

    // Start is called before the first frame update
    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        TestPlay();
       // FooPlay();
    }

    [PunRPC]
    void RPC_PlayAudio() {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }


    void TestPlay() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            myPhotonView.RPC("RPC_PlayAudio", RpcTarget.AllBuffered);
        }
    }

    //void FooPlay() {
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        audioSource.clip = audioClips[0];
    //        audioSource.Play();
    //    }
    //}
}
