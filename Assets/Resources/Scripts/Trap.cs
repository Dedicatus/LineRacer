using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
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
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player" && isActive) {
            isActive = false;
            other.gameObject.GetComponent<Player>().isStun = true;
            other.gameObject.GetComponent<Player>().stunTime = stunTime;
            curPlayer = other.gameObject;
            myAudioController.playDizzy.start();
           // myAudioController.playDizzy.release();

        }
    }

    public void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject == curPlayer && !isCoolDown) {
            isCoolDown = true;
        }
    }
}
