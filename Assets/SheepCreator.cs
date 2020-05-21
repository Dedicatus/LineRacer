using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SheepCreator : MonoBehaviour
{
    [SerializeField] private Transform topleftPoint;
    [SerializeField] private Transform bottomRightPoint;
    [SerializeField] private float clock = 15;
    [SerializeField] private int sheepNumber,goldenSheep;
    float counter;
    // Start is called before the first frame update
    void Start()
    {
        sheepNumber = 3;
        goldenSheep = 1;
        if(PhotonNetwork.OfflineMode)
        {
            Instantiate(Resources.Load(Path.Combine("Prefabs", "Sheep")), new Vector3(Random.Range(topleftPoint.position.x, bottomRightPoint.position.x), 10f, Random.Range(bottomRightPoint.position.z, topleftPoint.position.z)), Quaternion.identity);
            Instantiate(Resources.Load(Path.Combine("Prefabs", "Sheep")), new Vector3(Random.Range(topleftPoint.position.x, bottomRightPoint.position.x), 10f, Random.Range(bottomRightPoint.position.z, topleftPoint.position.z)), Quaternion.identity);
            Instantiate(Resources.Load(Path.Combine("Prefabs", "GoldenSheep")), new Vector3(Random.Range(topleftPoint.position.x, bottomRightPoint.position.x), 10f, Random.Range(bottomRightPoint.position.z, topleftPoint.position.z)), Quaternion.identity);
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Sheep"), new Vector3(Random.Range(topleftPoint.position.x, bottomRightPoint.position.x), 10f, Random.Range(bottomRightPoint.position.z, topleftPoint.position.z)), Quaternion.identity);
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Sheep"), new Vector3(Random.Range(topleftPoint.position.x, bottomRightPoint.position.x), 10f, Random.Range(bottomRightPoint.position.z, topleftPoint.position.z)), Quaternion.identity);
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "GoldenSheep"), new Vector3(Random.Range(topleftPoint.position.x, bottomRightPoint.position.x), 10f, Random.Range(bottomRightPoint.position.z, topleftPoint.position.z)), Quaternion.identity);
            }
        }
        counter = clock;
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        if (sheepNumber == 0)
            counter = 0;
        if (counter <= 0)
        {
            if (goldenSheep <= 0)
            {
                if (PhotonNetwork.OfflineMode)
                    Instantiate(Resources.Load(Path.Combine("Prefabs", "GoldenSheep")), new Vector3(Random.Range(topleftPoint.position.x, bottomRightPoint.position.x), 10f, Random.Range(bottomRightPoint.position.z, topleftPoint.position.z)), Quaternion.identity);
                else if (PhotonNetwork.IsMasterClient)
                    PhotonNetwork.Instantiate(Path.Combine("Prefabs", "GoldenSheep"), new Vector3(Random.Range(topleftPoint.position.x, bottomRightPoint.position.x), 10f, Random.Range(bottomRightPoint.position.z, topleftPoint.position.z)), Quaternion.identity);
                goldenSheep++;
            }
            else
            {
                if (PhotonNetwork.OfflineMode)
                    Instantiate(Resources.Load(Path.Combine("Prefabs", "Sheep")), new Vector3(Random.Range(topleftPoint.position.x, bottomRightPoint.position.x), 10f, Random.Range(bottomRightPoint.position.z, topleftPoint.position.z)), Quaternion.identity);
                else if (PhotonNetwork.IsMasterClient)
                    PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Sheep"), new Vector3(Random.Range(topleftPoint.position.x, bottomRightPoint.position.x), 10f, Random.Range(bottomRightPoint.position.z, topleftPoint.position.z)), Quaternion.identity);
            }
            sheepNumber++;
            counter = clock;
        }

    }

    public void goal(bool i)
    {
        if (i)
            goldenSheep--;
        sheepNumber--;
    }
}
