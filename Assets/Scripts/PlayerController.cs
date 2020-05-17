using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CharacterController controller;
    private Camera mainCamera;
    private Vector3 velocity;
    private Dictionary<string, Stat> statMap_;

    public bool IsMoving { get; set; }

    [SerializeField] private float walkSpeed = 6.0f;
    [SerializeField] private float runSpeed = 12.0f;
    [SerializeField] private float gravity = 5.0f;

    [SerializeField] private GameObject bigBallOfViolencePrefab;
    private bool isFighting = false;

    void Start() {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        IsMoving = false;

        BuildStatMap();
    }

    void Update()
    {
        Debug.Log("HP :: " + statMap_["HP"].GetStatValue());
        Debug.Log("Attack :: " + statMap_["Attack"].GetStatValue());

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

    public Vector3 GetVelocity() {
        return velocity;
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        /*if(hit.gameObject.tag == "Enemy" && !isFighting)
        {
            Instantiate(bigBallOfViolencePrefab, transform.position, Quaternion.identity);
            isFighting = true;
            transform.GetComponent<MeshRenderer>().enabled = false;
            hit.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }*/
    }

    private void BuildStatMap() {
        statMap_ = new Dictionary<string, Stat>();
        
        Stat hpStat = new Stat("HP", 300);
        hpStat.AddModifier(new StatModifier(hpStat.Name, StatModifier.ModifierType.ADD, 50.0f));
        hpStat.AddModifier(new StatModifier(hpStat.Name, StatModifier.ModifierType.MULTIPLY, -0.5f));
        statMap_.Add(hpStat.Name, hpStat);

        Stat attackStat = new Stat("Attack", 5);
        attackStat.AddModifier(new StatModifier(attackStat.Name, StatModifier.ModifierType.MULTIPLY, 0.5f));
        statMap_.Add(attackStat.Name, attackStat);
    }

    public int? GetStatValueByName(string statName) {
        if(string.IsNullOrEmpty(statName) || !statMap_.ContainsKey(statName)) return null;
        return statMap_[statName].GetStatValue();
    }
}
