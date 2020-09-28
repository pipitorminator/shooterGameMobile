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
    public FixedJoystick BombJoystick;

    private Vector3 movement;
    private Rigidbody rb;
    private Animator animator;
    private PlayerShooting playerShooting;

    public Rigidbody bombArea;
    public float range;
    private Vector3 bombAreaMovement;

    private void Awake()
    {
        Time.timeScale = 1;
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

        var bombHorizontal = BombJoystick.Horizontal;
        var bombVertical = BombJoystick.Vertical;

        Move(horizontal, vertical);

        Turning(rotHorizontal, rotVertical);

        MoveBombArea(bombHorizontal, bombVertical);

        Animating(horizontal, vertical);
    }

    void Move(float horizontal, float vertical)
    {
        movement.Set(horizontal, 0, vertical);

        movement = movement.normalized * (speed * Time.deltaTime);

        rb.MovePosition((transform.position + movement));
    }

    void MoveBombArea(float horizontal, float vertical)
    {
        if (horizontal == 0 && vertical == 0)
        {
            bombArea.gameObject.SetActive(false);
            return;
        }

        bombArea.gameObject.SetActive(true);

        bombAreaMovement.Set(horizontal, 0, vertical);

        bombAreaMovement = bombAreaMovement * range;

        bombArea.MovePosition(transform.position + bombAreaMovement);
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