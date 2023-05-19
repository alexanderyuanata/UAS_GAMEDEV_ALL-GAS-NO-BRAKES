using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public CharacterController controller;

    float speed;
    public float walking_speed;
    public float running_speed;
    public float gravity;
    public float jumpHeight;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    Vector3 velocity;
    private bool _enabled = true;


    private void Awake()
    {
        instance = this;
    }

    public void enablePlayerMovement()
    {
        _enabled = true;
    }

    public void disablePlayerMovement()
    {
        _enabled = false;
    }

    void Update()
    {
        if (_enabled)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = running_speed;
            }
            else
            {
                speed = walking_speed;
            }

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed *Time.deltaTime);

            if(Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }
}
