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
    public float crouch_lower_speed;
    public float running_threshold;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    Vector3 velocity;
    private Vector3 initial_pos;
    private bool _enabled = true;
    private bool crouching = false;
    private bool running = false;

    private movingStates player_state;
    private bool is_crouching = false;

    public enum movingStates
    {
        RUNNING,
        WALKING,
        CROUCHING
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        initial_pos = cam.transform.localPosition;
    }

    public bool isRunning()
    {
        return running;
    }

    public void enablePlayerMovement()
    {
        _enabled = true;
    }

    public void disablePlayerMovement()
    {
        _enabled = false;
    }

    public movingStates getMoveState()
    {
        return player_state;
    }

    void Update()
    {
        if (_enabled)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            running = Input.GetKey(KeyCode.LeftShift) && controller.velocity.magnitude >= running_threshold;
            is_crouching = Input.GetKey(KeyCode.C);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (is_crouching)
            {
                cam.transform.localPosition = Vector3.MoveTowards(cam.transform.localPosition, crouch_height.localPosition, crouch_lower_speed * Time.deltaTime);
            }
            else
            {
                cam.transform.localPosition = Vector3.MoveTowards(cam.transform.localPosition, initial_pos, crouch_lower_speed * Time.deltaTime);
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.LeftShift) && !crouching)
            {
                player_state = movingStates.RUNNING;
                speed = running_speed;
            }
            else if (is_crouching)
            {
                player_state = movingStates.CROUCHING;
                speed = crouching_speed;
            }
            else
            {
                player_state = movingStates.WALKING;
                speed = walking_speed;
            }

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            if (!isGrounded)
            {
                velocity.y -= gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                
            }
        }
    }
}
