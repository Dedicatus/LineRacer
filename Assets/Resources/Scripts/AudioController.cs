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
    public FMOD.Studio.EventInstance playDizzy;
    public FMOD.Studio.EventInstance playFirstSheep;
    public FMOD.Studio.EventInstance playScore;
    public FMOD.Studio.EventInstance playSheep;

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

    public void PlayEndSound(int teamNum) {
        switch (PhotonNetwork.LocalPlayer.ActorNumber) {
            case 1:
                if (teamNum == 1) {
                    playWin.start();
                    playWin.release();
                }
                else{
                    playLose.start();
                    playLose.release();
                }
                break;
            case 2:
                if (teamNum == 1)
                {
                    playWin.start();
                    playWin.release();
                }
                else
                {
                    playLose.start();
                    playLose.release();
                }
                break;
            case 3:
                if (teamNum == 2)
                {
                    playWin.start();
                    playWin.release();
                }
                else
                {
                    playLose.start();
                    playLose.release();
                }
                break;
            case 4:
                if (teamNum == 2)
                {
                    playWin.start();
                    playWin.release();
                }
                else
                {
                    playLose.start();
                    playLose.release();
                }
                break;

        }
    }
}
