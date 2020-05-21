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

    [FMODUnity.EventRef]
    FMOD.Studio.EventInstance playAmbience;
    FMOD.Studio.EventInstance playMusic;
    FMOD.Studio.EventInstance playWin;
    FMOD.Studio.EventInstance playLose;
    FMOD.Studio.EventInstance playDizzy;
    FMOD.Studio.EventInstance playFirstSheep;
    FMOD.Studio.EventInstance playScore;
    FMOD.Studio.EventInstance playSheep;

    // Start is called before the first frame update
    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        audioSource = GetComponent<AudioSource>();
        playAmbience = FMODUnity.RuntimeManager.CreateInstance("event:/ambience");
        playAmbience.start();
        playMusic = FMODUnity.RuntimeManager.CreateInstance("event:/bgm");
        playMusic.start();
        playWin = FMODUnity.RuntimeManager.CreateInstance("event:/win");
        playLose = FMODUnity.RuntimeManager.CreateInstance("event:/lose");
        playDizzy = FMODUnity.RuntimeManager.CreateInstance("event:/dizzy");
        playFirstSheep = FMODUnity.RuntimeManager.CreateInstance("event:/firstsheep");
        playScore = FMODUnity.RuntimeManager.CreateInstance("event:/score");
        playSheep = FMODUnity.RuntimeManager.CreateInstance("event:/sheep");
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
