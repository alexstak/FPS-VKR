using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float groundDistance = 1.0f;
    private Vector3 velocity;
    private bool isGrounded;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");



        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if(Input.GetKey("c"))
        {
            controller.height = 1f;
        }
        else
        {
            controller.height = 2f;
        }

        if(Input.GetKey("left shift"))
        {
            speed = 20f;
        }
        else
        {
            speed = 10f;
        }
    }

}
