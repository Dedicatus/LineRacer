using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGround : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
