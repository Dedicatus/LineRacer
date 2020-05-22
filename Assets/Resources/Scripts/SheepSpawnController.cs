using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SheepSpawnController : MonoBehaviour
{
    [SerializeField] private Transform topleftPoint;
    [SerializeField] private Transform bottomRightPoint;
    [SerializeField] private float spawnCD = 15;
    [SerializeField] private int sheepNumber, goldenSheep;
    [SerializeField] private int maxSheep = 4;
    float spawnTimer;
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
        spawnTimer = spawnCD;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (sheepNumber == 0)
            spawnTimer = 0;
        if (spawnTimer <= 0 && sheepNumber < maxSheep)
        {
            if (goldenSheep <= 0)
            {
                if (PhotonNetwork.OfflineMode)
                    Instantiate(Resources.Load(Path.Combine("Prefabs", "GoldenSheep")), new Vector3(Random.Range(topleftPoint.position.x, bottomRightPoint.position.x), 10f, Random.Range(bottomRightPoint.position.z, topleftPoint.position.z)), Quaternion.identity);
                else
                {
                    if (PhotonNetwork.IsMasterClient)
                        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "GoldenSheep"), new Vector3(Random.Range(topleftPoint.position.x, bottomRightPoint.position.x), 10f, Random.Range(bottomRightPoint.position.z, topleftPoint.position.z)), Quaternion.identity);
                }
                goldenSheep++;
            }
            else
            {
                if (PhotonNetwork.OfflineMode)
                    Instantiate(Resources.Load(Path.Combine("Prefabs", "Sheep")), new Vector3(Random.Range(topleftPoint.position.x, bottomRightPoint.position.x), 10f, Random.Range(bottomRightPoint.position.z, topleftPoint.position.z)), Quaternion.identity);
                else
                {
                    if (PhotonNetwork.IsMasterClient)
                        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Sheep"), new Vector3(Random.Range(topleftPoint.position.x, bottomRightPoint.position.x), 10f, Random.Range(bottomRightPoint.position.z, topleftPoint.position.z)), Quaternion.identity);
                }
            }
            sheepNumber++;
            spawnTimer = spawnCD;
        }

    }

    public void captureSheep(bool isGolden)
    {
        if (isGolden)
            goldenSheep--;
        sheepNumber--;
    }
}
