using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] int scoreValue = 10;

    [Header("Debug")]
    [SerializeField] bool collected = false;

    public int getValue()
    {
        collected = true;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<ObiCollider>().enabled = false;
        return scoreValue;
    }
}
