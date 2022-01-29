using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayerMechanism : MonoBehaviour
{
    public GameObject[] playerPrefabs;

    public float minX, maxX, minY, maxY;

    public Transform[] spawnPoints;


    // Start is called before the first frame update
    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["role"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, randomPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
