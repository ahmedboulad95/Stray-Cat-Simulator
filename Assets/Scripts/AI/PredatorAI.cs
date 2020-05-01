using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorAI : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player") {
            animator.SetBool("isScared", true);
        }
    }

    void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player") {
            animator.SetBool("isScared", false);
        }
    }
}
