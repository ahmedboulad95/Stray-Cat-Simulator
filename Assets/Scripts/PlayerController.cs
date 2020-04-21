using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CharacterController controller;
    private Camera camera;

    public bool IsMoving { get; set; }

    [SerializeField] private float walkSpeed = 12.0f;
    [SerializeField] private float runSpeed = 24.0f;
    [SerializeField] private float gravity = 5.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
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

            //Vector3 movementDirection = new Vector3(x, 0, z);
            //movementDirection = camera.transform.TransformDirection(movementDirection);
            //Debug.Log(movementDirection);

            //movementDirection.y -= -transform.up.y * gravity;

            Vector3 velocity = camera.transform.right * x + -transform.up * gravity + camera.transform.forward * z;
            //velocity = camera.transform.TransformDirection(velocity);
            controller.Move(velocity * moveSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            IsMoving = false;
        }
    }
}
