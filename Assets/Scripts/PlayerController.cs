using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;
using TMPro;

public class PlayerController : NetworkBehaviour
{
    [Header("Movement")]
    public NetworkVariable<float> moveSpeed = new NetworkVariable<float>(8, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public NetworkVariable<float> strength = new NetworkVariable<float>(5, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public float groundDrag;    

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool  readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;


    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    // [SerializeField] private CinemachineVirtualCamera vc;
    [SerializeField] private CinemachineFreeLook fl;
    [SerializeField] private AudioListener listener;

    // Adjustan ng bounce force sa character
    public float bounceForce;

    [SerializeField] private Animator animator; // FOR 3D PLAYER


    public override void OnNetworkSpawn()
    {
        if(IsOwner)
        {
            //enable the listener
            listener.enabled = true;
            //set camera priority
            fl.Priority = 3;

        }

        else
        {
            //set camera priority low
            fl.Priority = 0;
        }


    }



    // Start is called before the first frame update
    void Start()
    {
        if(!IsOwner) return;        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

    }

    void FixedUpdate()
    {
        if(!IsOwner) return;
        
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Client# " + OwnerClientId + "; movement speed: " + moveSpeed.Value + "; strength: " + strength.Value); // i-comment mo 'to kung ayaw mo yung sunod-sunod sa Logs


        if(!IsOwner) return;


        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if(grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        MyInput();
        SpeedControl();



        // if (Input.GetKeyDown(KeyCode.T))
        // {
        //     moveSpeed.Value = 20;
        //     // Debug.Log("Client# " + OwnerClientId + "; movement speed: " + moveSpeed.Value);
        // }

        // if (Input.GetKeyDown(KeyCode.Y))
        // {
        //     strength.Value = 10;
        //     // Debug.Log("Client# " + OwnerClientId + "; strength: " + strength.Value);
        // }


    }


    

    void PlayerRun()
    {
        if(!IsOwner) return;

        if(moveSpeed.Value >= 13)
        {
            
        }

    }

    void MyInput()
    {
        if(!IsOwner) return;

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(verticalInput == 1 || verticalInput == 0)
        {
            animator.SetFloat("speed", Mathf.Abs(verticalInput)); // FOR MOVEMENT ANIMATION
            animator.SetFloat("speed2", moveSpeed.Value);
        }

        if(Input.GetKey(KeyCode.S))
        {
            animator.SetFloat("speed", Mathf.Abs(verticalInput)); // FOR MOVEMENT ANIMATION
            animator.SetFloat("speed2", moveSpeed.Value);
        }

        if(horizontalInput == 1 || verticalInput == 0)
        {
            animator.SetFloat("speed", Mathf.Abs(horizontalInput)); // FOR MOVEMENT ANIMATION
            animator.SetFloat("speed2", moveSpeed.Value);
        }
     

        //when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            animator.SetBool("jump", true);
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("isAttacking");
        }
    }


    void MovePlayer()
    {
        if(!IsOwner) return;
        //calc movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on ground
        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed.Value * 10f, ForceMode.Force);
            animator.SetBool("jump", false);
        }

        //in air
        // else if(!grounded)
        // {
        //     rb.AddForce(moveDirection.normalized * moveSpeed.Value * 10f * airMultiplier, ForceMode.Force);
        // }
        

    }

    void SpeedControl()
    {
        if(!IsOwner) return;

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit moveSpeed
        if(flatVel.magnitude > moveSpeed.Value)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed.Value;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void Jump()
    {
        if(!IsOwner) return;

        
        //reset Y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        rb.AddForce(Vector3.down * 200f, ForceMode.Force);

        

    }

    void ResetJump()
    {
        if(!IsOwner) return;

        readyToJump = true;
    }

//collisions for trap and gameplay logic
    void OnCollisionEnter(Collision collision)
    {
        if (!IsOwner) return;

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Team1") || collision.gameObject.CompareTag("Team2"))
        {
            BounceBack();
        }
        else if (collision.gameObject.CompareTag("SlowTrap"))
        {
            SlowPlayer();
        }
        else if (collision.gameObject.CompareTag("StickTrap"))
        {
            StickToTrap(); 
        }
    }


    void BounceBack()
    {
        if (!IsOwner) return;
        //bounce logic
        Vector3 bounceDirection = -rb.velocity.normalized;

        rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);

        Debug.Log("Player Bounced Back!");

    }

    void SlowPlayer()
{
    if (!IsOwner) return;

    // reduce player speeddd
    moveSpeed.Value = 4f;
    Debug.Log("Player Slowed Down!");

   
    StartCoroutine(RestorePlayerSpeed());
}

IEnumerator RestorePlayerSpeed()
{
    yield return new WaitForSeconds(3f);

    // restore player speedd
    moveSpeed.Value = 8f; //timer
    Debug.Log("Player Speed Restored!");
}



    void StickToTrap()
{
    if (!IsOwner) return;
    Debug.Log("Player Stuck to Trap!");

    // freeze player after collision
    rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

    StartCoroutine(UnstickFromTrap());
}

IEnumerator UnstickFromTrap()
{
    yield return new WaitForSeconds(2f);

    // unfreeze si player
    rb.constraints = RigidbodyConstraints.None;

    // ikikeep yung rotation constraint ni player
    rb.freezeRotation = true;

    Debug.Log("Player Unstuck from Trap!");
}


}
