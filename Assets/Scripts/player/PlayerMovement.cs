using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6;
    public FixedJoystick joystickMove;
    public FixedJoystick joystickRotate;

    private Vector3 movement;
    private Rigidbody rb;
    private Animator animator;
    private PlayerShooting playerShooting;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
    }

    private void FixedUpdate()
    {
        var horizontal = 0f;
        var vertical = 0f;


        if (Application.isMobilePlatform)
        {
            horizontal = joystickMove.Horizontal;
            vertical = joystickMove.Vertical;
        }
        else
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        var rotHorizontal = joystickRotate.Horizontal;
        var rotVertical = joystickRotate.Vertical;


        Move(horizontal, vertical);

        Turning(rotHorizontal, rotVertical);

        Animating(horizontal, vertical);
    }

    void Move(float horizontal, float vertical)
    {
        movement.Set(horizontal, 0, vertical);

        movement = movement.normalized * (speed * Time.deltaTime);

        rb.MovePosition((transform.position + movement));
    }

    void Turning(float horizontal, float vertical)
    {
        Vector3 rot = new Vector3(horizontal, 0, vertical);
        if (rot != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rot);
            playerShooting.Shoot();
        }
    }

    void Animating(float horizontal, float vertical)
    {
        bool walking = horizontal != 0 || vertical != 0;

        animator.SetBool("Walking", walking);
    }
}