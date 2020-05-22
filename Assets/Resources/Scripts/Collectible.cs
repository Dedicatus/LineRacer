using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] int scoreValue = 10;

    [Header("Debug")]
    [SerializeField] bool collected = false;
    [SerializeField] bool isAutopilot = false;
    [SerializeField] float autopilotSpeed = 3f;
    [SerializeField] float maxSpeed = 6f;
    [SerializeField] float soundCD = 5f;
    [SerializeField] Transform target;

    float soundTimer;
    Rigidbody rb;
    private AudioController myAudioController;
    private void Start()
    {
        soundTimer = soundCD;
        rb = gameObject.GetComponent<Rigidbody>();
        myAudioController = GameObject.FindWithTag("System").transform.Find("AudioPlayer").GetComponent<AudioController>();
    }

    private void FixedUpdate()
    {
        detectSpeed();
        if (isAutopilot)
        {
            float step = autopilotSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                isAutopilot = false;
                Quaternion rotation = Quaternion.Euler(0, transform.rotation.y, 0);

                transform.Find("SheepCollider").gameObject.GetComponent<Rigidbody>().mass *= 0.1f;
                transform.Find("SheepCollider").gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
            else
            {
                Vector3 relativePos = target.position - transform.position;

                // the second argument, upwards, defaults to Vector3.up
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = rotation;
            }
        }
    }

    void detectSpeed()
    {
        if (rb == null)
            return;
        soundTimer -= Time.deltaTime;
        if (soundTimer > 0)
            return;
        else
        {
            float curS = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z);
            if (curS >= maxSpeed)
            {
                makesound();
                soundTimer = soundCD;
            }
        }
    }

    void makesound()
    {
        myAudioController.playSheep.start();
        myAudioController.playSheep.release();
        Debug.Log("mie~");
    }

    public int getValue()
    {
        collected = true;
        Destroy(gameObject.GetComponent<ObiRigidbody>());
        Destroy(gameObject.GetComponent<Rigidbody>());
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<ObiCollider>().enabled = false;
        transform.Find("SheepCollider").gameObject.AddComponent<Rigidbody>();
        transform.Find("SheepCollider").gameObject.GetComponent<BoxCollider>().size = new Vector3(transform.Find("SheepCollider").gameObject.GetComponent<BoxCollider>().size.x * 2, transform.Find("SheepCollider").gameObject.GetComponent<BoxCollider>().size.y, transform.Find("SheepCollider").gameObject.GetComponent<BoxCollider>().size.z * 2);
        isAutopilot = true;
        return scoreValue;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Collectible" || collision.gameObject.tag == "GoldenSheep")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    public void setTarget(Transform tar)
    {
        target = tar;
    }

    public bool getCollected()
    {
        return collected;
    }

}
