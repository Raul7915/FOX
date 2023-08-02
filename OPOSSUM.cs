using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPOSSUM : MonoBehaviour
{
    Animator animator;
    [SerializeField] bool DEAD = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FOX"))
        {
            DEAD = true;
            if (DEAD)
           { 
            animator.SetBool("DEAD", true);
            }
        }

    }

}
