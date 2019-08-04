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

    public void setPlayerTurn(){
        animator.SetBool("YourTurn", true);
        animator.SetBool("EnemyTurn", false);
        animator.SetBool("PlacementTurn", false);
    }

    public void setEnemyTurn(){
        animator.SetBool("YourTurn", false);
        animator.SetBool("EnemyTurn", true);
        animator.SetBool("PlacementTurn", false);
    }

    public void setPlacementTurn(){
        animator.SetBool("YourTurn", false);
        animator.SetBool("EnemyTurn", false);
        animator.SetBool("PlacementTurn", true);
    }
}
