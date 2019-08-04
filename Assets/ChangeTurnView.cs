using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTurnView : MonoBehaviour
{
    

    Animator animator;
    int x;
    void Start () {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        x++;

        if(x>100){
            animator.SetBool("YourTurn",false);
            animator.SetBool("EnemyTurn",true);
        }
    }
}
