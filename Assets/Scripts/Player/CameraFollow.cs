using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Transform tFollowTarget;
    private CinemachineVirtualCamera vcam;


    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Seeker");
            if (player == null )
            { 
                player = GameObject.FindGameObjectWithTag("Hider");
            }

        }

        if (player != null)
        {
            tFollowTarget = player.transform;
            vcam.LookAt = tFollowTarget;
            vcam.Follow = tFollowTarget;
        }
    }
}
