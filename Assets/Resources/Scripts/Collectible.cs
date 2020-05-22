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
    [SerializeField] Transform target;

    private void FixedUpdate()
    {
        if (isAutopilot)
        {
            float step = autopilotSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                isAutopilot = false;
                Quaternion rotation = Quaternion.Euler(0, transform.rotation.y, 0);
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

    public int getValue()
    {
        collected = true;
        Destroy(gameObject.GetComponent<ObiRigidbody>());
        Destroy(gameObject.GetComponent<Rigidbody>());
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<ObiCollider>().enabled = false;
        transform.Find("SheepCollider").gameObject.AddComponent<Rigidbody>();
        isAutopilot = true;
        return scoreValue;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
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
