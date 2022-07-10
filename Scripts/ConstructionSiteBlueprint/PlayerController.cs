using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;
    
    public float moveSpeed ;
    public float jumpSpeed ;

    private float horizonalMove, verticalMove;
    private Vector3 dir;

    public float gravity;
    private Vector3 velocity;
    
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;
    private bool isGround;

    private void Start () {
        cc = GetComponent<CharacterController> ();
    }

    private void Update () {
        isGround = Physics.CheckSphere (groundCheck.position, checkRadius, groundLayer);

        if (isGround && velocity.y < 0) {
            velocity.y = 0f;
        } 

        horizonalMove = Input.GetAxis ("Horizontal") * moveSpeed;
        verticalMove = Input.GetAxis ("Vertical") * moveSpeed;

        if (Input.GetButtonDown("Jump")) {
            velocity.y = jumpSpeed;
        }

        dir = transform.forward * verticalMove + transform.right * horizonalMove;
        cc.Move(dir * Time.deltaTime);

        velocity.y -= gravity * Time.deltaTime;
        cc.Move(velocity*Time.deltaTime);

    }
}
