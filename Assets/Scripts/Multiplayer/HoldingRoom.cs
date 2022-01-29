using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class HoldingRoom : MonoBehaviour
{

    public GameObject seekerBtn;
    public GameObject hiderBtn;
    public GameObject playBtn;

    // constants for roles
    const int hider = 0;
    const int seeker = 1;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playBtn.SetActive(true);
        }
        else {
            playBtn.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2) 
        {
            PhotonNetwork.LoadLevel("Level1 test");
        } else {
            Debug.Log("Not enough players!");
        }
        
    }

    public void OnClickSeekerBtn()
    {
        playerProperties["role"] = seeker;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickHiderBtn()
    {
        playerProperties["role"] = hider;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

}
