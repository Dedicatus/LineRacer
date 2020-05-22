using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamBase : MonoBehaviour
{
    [SerializeField] private GameController.Team myTeam;

    private GameController myGameController;
    private SheepSpawnController mySheepSpawnController;
    private void Start()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        mySheepSpawnController = GameObject.FindWithTag("SheepSpawnController").GetComponent<SheepSpawnController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectible"||other.tag == "GoldenSheep")
        {
            other.GetComponent<Collectible>().setTarget(transform);
            myGameController.addScore(myTeam, other.GetComponent<Collectible>().getValue());
            if (other.tag == "GoldenSheep")
                mySheepSpawnController.captureSheep(true);
            else mySheepSpawnController.captureSheep(false);
        }
    }
}
