using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    public float MovementSpeed = 8f;
    public float JumpVelocity = 15f;
    public float FeetRadius = 0.3f;
    private bool isGrounded = false;

    public GameObject EBtn;
    private Animator anim;
    PhotonView view;

    public Transform Feet;
    public LayerMask groundLayer;
    public Rigidbody2D rb;

    private float moveHorizontal;

    bool isNear = false;
    bool isSeeker = false;
    bool isHider = true;

    private void Start()
    {
        //Get reference to rigidbody component for left right movement and jumping
        //rb = GetComponent<Rigidbody2D>();

        EBtn.SetActive(false);
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (view.IsMine)
        {

            moveHorizontal = Input.GetAxis("Horizontal");
            
            isHider = this.gameObject.tag == "Hider";
            isSeeker = this.gameObject.tag == "Seeker";

            if (isSeeker && isNear && Input.GetKeyDown(KeyCode.E))
            {
                
                Debug.Log("Caught");
                // Get the master client
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
        
            }

            //Move the player
            if (moveHorizontal != 0)
            {
                rb.velocity = new Vector2(moveHorizontal * MovementSpeed, rb.velocity.y);
            } else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            //Character to face correct direction
            if (moveHorizontal > 0) //moving right
            {
                // transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                if (isHider) {
                    anim.SetBool("RunningRight", true);
                } else {
                    anim.SetBool("CatRunningRight", true);
                }
                
            } else if (moveHorizontal < 0) //moving left
            {
                // transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                if (isHider) {
                    anim.SetBool("RunningLeft", true);
                } else {
                    anim.SetBool("CatRunningLeft", true);
                }
            
            } else {
                if (isHider) {
                    anim.SetBool("RunningLeft", false);
                    anim.SetBool("RunningRight", false);
                } else {
                    anim.SetBool("CatRunningLeft", false);
                    anim.SetBool("CatRunningRight", false);
                }
            }

            //Check if player is grounded
            isGrounded = Physics2D.OverlapCircle(Feet.position, FeetRadius, groundLayer);

            //Handle player jumping, player jumps when jump key is pressed and its not midair
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpVelocity);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if ((isSeeker && collision.gameObject.CompareTag("Hider"))
            || (isHider && collision.gameObject.CompareTag("Seeker")))
        {
            isNear = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if ((isSeeker && collision.gameObject.CompareTag("Hider"))
            || (isHider && collision.gameObject.CompareTag("Seeker")))
        {
            isNear = false;
        }
    }

    /*
    Sample code
    
    [PunRPC]
    private void OnGameOverMouse() {
        PhotonNetwork.LoadLevel("GameOverMouse");
    }

    [PunRPC]
    private void OnGameOverCat() {
        PhotonNetwork.LoadLevel("GameOverCat");
    }
    */

    // TODO: Add functionality to restart game (call from master client)

}
