using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (transform.parent.GetComponent<Collectible>().getCollected())
        {
            if (collision.gameObject.tag == "Player")
            {
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            }
        }
    }
}
