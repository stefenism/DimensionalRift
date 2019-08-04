using UnityEngine;

public class Slime : Actor {
    
    void Update(){
        if(GameManager.gameDaddy.isEnemyTurn()){
            checkAnim();
        }
        checkAnim();
    }

    public override void kill(){
        Debug.Log("killing the slime");
        EnemyManager.enemyDaddy.slimeList.Remove(this);
        TileManager.getTileAt(transform.position).containedActor = null;
        Destroy(this.gameObject);
    }

    void checkAnim(){
        switch(actorState){
            case ActorState.READY:
                anim.SetBool("Ready", true);
                anim.SetBool("March", false);
                anim.SetBool("AttackReady", false);
                anim.SetBool("Attacking", false);
                break;
            case ActorState.MOVING:
                anim.SetBool("Ready", false);
                anim.SetBool("March", true);
                anim.SetBool("AttackReady", false);
                anim.SetBool("Attacking", false);
                break;
            case ActorState.ATTACKREADY:
                anim.SetBool("Ready", false);
                anim.SetBool("March", false);
                anim.SetBool("AttackReady", true);
                anim.SetBool("Attacking", false);
                break;
            case ActorState.ATTACKING:                
                anim.SetBool("Ready", false);
                anim.SetBool("March", false);
                anim.SetBool("AttackReady", false);
                anim.SetBool("Attacking", true);
                break;
        }
    }
}