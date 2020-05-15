using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInfo : MonoBehaviour
{
    public int playerCreatedCount;
    private PhotonView myPhotonView;
    // Start is called before the first frame update
    void Start()
    {
        playerCreatedCount = 0;
        myPhotonView = this.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [PunRPC]
    public void RPC_SetPlayerCreatedCount(int count)
    {

        playerCreatedCount = count;

    }


    public void CallAdd() {
        myPhotonView.RPC("RPC_SetPlayerCreatedCount", RpcTarget.AllBuffered, playerCreatedCount + 1);
    }
}
