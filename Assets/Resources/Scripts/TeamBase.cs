using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamBase : MonoBehaviour
{
    [SerializeField] private GameController.Team myTeam;

    private GameController myGameController;
    private SheepCreator mySheepCreator;
    private void Start()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        mySheepCreator = GameObject.FindWithTag("SheepCreator").GetComponent<SheepCreator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectible"||other.tag == "GoldenSheep")
        {
            myGameController.addScore(myTeam, other.GetComponent<Collectible>().getValue());
            if (other.tag == "GoldenSheep")
                mySheepCreator.goal(true);
            else mySheepCreator.goal(false);
        }
    }
}
