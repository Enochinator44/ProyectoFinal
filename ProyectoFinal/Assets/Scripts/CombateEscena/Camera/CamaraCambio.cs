using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraCambio : MonoBehaviour
{

    private Animator animator;

    public GameObject Enemy;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        switch (Enemy.GetComponent<Enemigo>().estado)
        {
            case 0:
                animator.Play("Main Camera");
                break;
            case 1:
                animator.Play("Right Camera");
                break;
            case 2:
                animator.Play("Left Camera");
                break;
            case 3:
                animator.Play("Right Camera");
                break;
            case 4:
                animator.Play("Left Camera");
                break;

        }
    }
}
