using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorAI : MonoBehaviour
{
    [SerializeField] private GameObject headIk_;
    private Animator animator_;
    private float rotationSpeed_ = 10.0f;

    private string state_ = "Normal";
    private GameObject inProximityEnemy_;

    void Start() {
        animator_ = GetComponent<Animator>();
    }

    void Update() {
        
    }

    void LateUpdate() {
        if(state_ == "Scared") {
            Vector3 direction = (inProximityEnemy_.transform.position - headIk_.transform.position).normalized;
            headIk_.transform.rotation = Quaternion.LookRotation(headIk_.transform.forward, direction);
        } 
    }

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player") {
            state_ = "Scared";
            animator_.SetBool("isScared", true);
            inProximityEnemy_ = col.gameObject;
        }
    }

    void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player") {
            state_ = "Normal";
            animator_.SetBool("isScared", false);
            inProximityEnemy_ = null;
        }
    }
}
