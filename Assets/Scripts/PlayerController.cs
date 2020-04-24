using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CharacterController controller;
    private Camera mainCamera;
    private Vector3 velocity;

    public bool IsMoving { get; set; }

    [SerializeField] private float walkSpeed = 12.0f;
    [SerializeField] private float runSpeed = 24.0f;
    [SerializeField] private float gravity = 5.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        IsMoving = false;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (!Mathf.Approximately(x, 0.0f) || !Mathf.Approximately(z, 0.0f))
        {
            IsMoving = true;
            bool isRunning = Input.GetKey("left shift");
            float moveSpeed = (isRunning) ? runSpeed : walkSpeed;

            if(isRunning)
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
            }
            else
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
            }

            velocity = mainCamera.transform.right * x + -transform.up * gravity + mainCamera.transform.forward * z;
            controller.Move(velocity * moveSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            IsMoving = false;
        }
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }
}
