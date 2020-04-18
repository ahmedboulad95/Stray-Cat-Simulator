using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CharacterController controller;

    public bool IsMoving { get; set; }

    [SerializeField] private float walkSpeed = 12.0f;
    [SerializeField] private float runSpeed = 24.0f;
    [SerializeField] private float gravity = 5.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
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

            //Vector3 direction = new Vector3(x, 0, z);
            //direction = (direction.sqrMagnitude > 1.0f) ? direction.normalized : direction;

            Vector3 velocity = transform.right * x + -transform.up * gravity + transform.forward * z;
            controller.Move(velocity * moveSpeed * Time.deltaTime);

            /*Vector3 facingRotation = Vector3.Normalize(new Vector3(x, 0.0f, z));
            if (facingRotation != Vector3.zero)
                transform.forward = facingRotation;*/
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            IsMoving = false;
        }
    }
}
