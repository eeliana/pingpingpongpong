using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class SpawnTrophyMechanism : MonoBehaviour
{
    public GameObject trop;
    Vector3[] other = { new Vector3(2.29f, 17.45f, 1), new Vector3(-1.31f, 19.4f, 1) , new Vector3(22.11f, 7.04f, 1) } ;
  
    private void Start()
    {
        // trop = GameObject.FindGameObjectWithTag("trophy");
        int ran = UnityEngine.Random.Range(0, other.Length - 1);
        Instantiate(trop, other[ran], Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
