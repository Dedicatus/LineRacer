using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamBase : MonoBehaviour
{
    [SerializeField] private GameController.Team myTeam;

    private GameController myGameController;

    private void Start()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectible")
        {
            myGameController.addScore(myTeam, other.GetComponent<Collectible>().getValue());
        }
    }
}
