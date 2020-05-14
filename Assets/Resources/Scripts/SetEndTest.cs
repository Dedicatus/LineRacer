using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class SetEndTest : MonoBehaviour
{

    public GameObject rope;
    public Transform pin1;
    public Transform pin2;

    private ObiParticleAttachment[] particleAttachments; 
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Instantiate(rope);
        rope.transform.position =  (pin1.position + pin2.position)/2;
        particleAttachments = rope.GetComponentsInChildren<ObiParticleAttachment>();
        particleAttachments[0].target = pin1;
        particleAttachments[1].target = pin2;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
