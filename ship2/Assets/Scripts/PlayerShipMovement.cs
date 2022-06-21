using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipMovement : MonoBehaviour
{
    public static PlayerShipMovement instance;
    public float moveSpeed;
    public Rigidbody rb;
    public FloatingJoystick joystick;
    private float moveH, moveV;

    private void Awake()
    {
        instance = this;
    }
    private void FixedUpdate()
    {
        rbMove();
    }
    void rbMove()
    {
        moveH = joystick.Horizontal;
        moveV = joystick.Vertical;
        Vector3 dir = new Vector3(moveV, 0, -moveH);
        rb.velocity = new Vector3(moveV * moveSpeed, rb.velocity.y, -moveH * moveSpeed);

        if (dir != Vector3.forward)
        {
            transform.LookAt(transform.position + dir);
        }
    }
}
