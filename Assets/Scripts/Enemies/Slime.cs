using UnityEngine;

public class Slime : Actor {
    
    void Update(){
        if(GameManager.gameDaddy.isEnemyTurn()){
            checkAnim();
        }
    }

    void checkAnim(){
        switch(actorState){
            case ActorState.READY:
                anim.SetBool("Ready", true);
                anim.SetBool("March", false);
                anim.SetBool("Finished", false);
                break;
            case ActorState.MOVING:
                anim.SetBool("Ready", false);
                anim.SetBool("March", true);
                anim.SetBool("Finished", false);
                break;
            case ActorState.FINISHED:
                anim.SetBool("Ready", false);
                anim.SetBool("March", false);
                anim.SetBool("Finished", true);
                break;
        }
    }
}