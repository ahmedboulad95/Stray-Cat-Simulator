using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CharacterController controller;

    [SerializeField] private float walkSpeed = 12.0f;
    [SerializeField] private float runSpeed = 24.0f;
    [SerializeField] private float gravity = 5.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float speed = walkSpeed;

        if (Input.GetKey("left shift"))
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
            speed = runSpeed;
        }
        else if (x != 0f || z != 0f)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }

        Vector3 move = transform.right * x + -transform.up * gravity + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }
}
