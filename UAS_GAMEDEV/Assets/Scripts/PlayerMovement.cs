using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public CharacterController controller;

    float speed;
    public float walking_speed = 12f;
    public float running_speed;
    public float gravity = -9.81f;

    private Vector3 velocity;
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
        }
    }
}
