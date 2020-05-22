using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
     private AudioSource audioSource;
    [SerializeField] 
    private AudioClip[] audioClips;
    private PhotonView myPhotonView;

    [FMODUnity.EventRef]
    FMOD.Studio.EventInstance playAmbience;
    public FMOD.Studio.EventInstance playMusic;
    FMOD.Studio.EventInstance playWin;
    FMOD.Studio.EventInstance playLose;
    public FMOD.Studio.EventInstance playDizzy;
    public FMOD.Studio.EventInstance playFirstSheep;
    public FMOD.Studio.EventInstance playScore;
    public FMOD.Studio.EventInstance playSheep;
    public FMOD.Studio.EventInstance playHerdingSpree;
    public FMOD.Studio.EventInstance playMegaSheep;
    public FMOD.Studio.EventInstance playMonsterShepherd;
    public FMOD.Studio.EventInstance playGodPanLike;
    public FMOD.Studio.EventInstance playHolyShepherd;

    // volume control
    FMOD.Studio.Bus SFXBus;
    [SerializeField]
    [Range(-25f, 10f)]
    private float sfxBusVolume;
    private float sfxVol;

    FMOD.Studio.Bus MusicBus;
    [SerializeField]
    [Range(-25f, 10f)]
    private float musicBusVolume;
    private float musicVol;

    [SerializeField] private GameObject setting;

    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Slider sliderMusic;

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
        playHerdingSpree = FMODUnity.RuntimeManager.CreateInstance("event:/herdingspree");
        playMegaSheep = FMODUnity.RuntimeManager.CreateInstance("event:/megasheep");
        playMonsterShepherd = FMODUnity.RuntimeManager.CreateInstance("event:/monstershepherd");
        playGodPanLike = FMODUnity.RuntimeManager.CreateInstance("event:/godpanlike");
        playHolyShepherd = FMODUnity.RuntimeManager.CreateInstance("event:/holyshepherd");

        MusicBus = FMODUnity.RuntimeManager.GetBus("bus:/music");
        SFXBus = FMODUnity.RuntimeManager.GetBus("bus:/sfx");
        setting.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TestPlay();
        // FooPlay();

        changeSFXVol();
        changeMusicVol();
    }

    public void changeSFXVol()
    {
        sfxBusVolume = sliderSFX.value;
        sfxVol = Mathf.Pow(10.0f, sfxBusVolume / 20f);
        SFXBus.setVolume(sfxVol);
        //Debug.Log(sliderSFX.value);
    }

    public void changeMusicVol()
    {
        musicBusVolume = sliderMusic.value;
        musicVol = Mathf.Pow(10.0f, musicBusVolume / 20f);
        MusicBus.setVolume(musicVol);
        // Debug.Log(sliderMusic.value);
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
