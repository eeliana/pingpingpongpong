using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TrophyCollection : MonoBehaviour
{
    public float countdown = 3;
    bool isCollecting = false;

    // Collector
    GameObject mouse;

    private void Update()
    {
        if (isCollecting)
        {
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
            }
            else
            {
                // Get the master client
                /*
                PhotonView photonView = PhotonView.Get(mouse);
                photonView.RPC("OnGameOverCat", RpcTarget.MasterClient);
                */
                GameObject[] hiders = GameObject.FindGameObjectsWithTag("Hider");
                GameObject[] seekers = GameObject.FindGameObjectsWithTag("Seeker");
                GameObject[] players = new GameObject[hiders.Length + seekers.Length];

                hiders.CopyTo(players, 0);
                seekers.CopyTo(players, hiders.Length);

                foreach (GameObject p in players) 
                {
                    PhotonView pv = p.GetComponent<PhotonView>();
                    if (pv.Owner.IsMasterClient) {
                        PhotonNetwork.LoadLevel("GameOverCat");
                    }
                }

                isCollecting = false;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hider"))
        {
            mouse = collision.gameObject;
            isCollecting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hider"))
        {
            countdown = 3;
            isCollecting = false;
        }
    }

}
