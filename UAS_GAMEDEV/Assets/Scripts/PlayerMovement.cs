using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public CharacterController controller;
    public Camera cam;
    public Transform crouch_height;

    float speed;
    public float walking_speed;
    public float running_speed;
    public float crouching_speed;
    public float gravity;
    public float jumpHeight;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    Vector3 velocity;
    private Vector3 initial_pos;
    private bool _enabled = true;
    private bool crouching = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
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

            if (Input.GetKey(KeyCode.LeftShift) && !crouching)
            {
                speed = running_speed;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                speed = crouching_speed;
            }
            else
            {
                speed = walking_speed;
            }
            
            if (Input.GetKeyDown(KeyCode.C))
            {
                crouching = true;
                initial_pos = cam.transform.position;
                cam.transform.position = crouch_height.position;
            }
            else if (Input.GetKeyUp(KeyCode.C))
            {
                crouching = false;
                Vector3 new_pos = new Vector3(cam.transform.position.x, initial_pos.y, cam.transform.position.z);
                cam.transform.position = new_pos;
            }

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            if (!isGrounded)
            {
                velocity.y -= gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }
        }
    }
}
