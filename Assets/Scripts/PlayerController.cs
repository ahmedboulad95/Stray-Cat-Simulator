using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CharacterController controller;
    private Camera mainCamera;
    private Vector3 velocity;

    public bool IsMoving { get; set; }

    [SerializeField] private float walkSpeed = 8.0f;
    [SerializeField] private float runSpeed = 16.0f;
    [SerializeField] private float gravity = 5.0f;

    [SerializeField] private GameObject bigBallOfViolencePrefab;
    private bool isFighting = false;

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

        bool isRunning = Input.GetKey("left shift");
        float moveSpeed = (isRunning) ? runSpeed : walkSpeed;

        if (!Mathf.Approximately(x, 0.0f) || !Mathf.Approximately(z, 0.0f))
        {
            IsMoving = true;

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
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            IsMoving = false;
        }

        velocity = mainCamera.transform.right * x + -transform.up * gravity + mainCamera.transform.forward * z;
        controller.Move(velocity * moveSpeed * Time.deltaTime);
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        /*if(hit.gameObject.tag == "Enemy" && !isFighting)
        {
            Instantiate(bigBallOfViolencePrefab, transform.position, Quaternion.identity);
            isFighting = true;
            transform.GetComponent<MeshRenderer>().enabled = false;
            hit.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }*/
    }
}
