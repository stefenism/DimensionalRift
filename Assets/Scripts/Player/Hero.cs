using System.Collections;
using UnityEngine;

public class Hero : Actor {
    
    public int movementDistance = 2;

    void Update(){
        if(GameManager.gameDaddy.isPlayerTurn()){
            checkAnim();
            if(isFinished()){
                return;
            }
            CheckMouseOver();
            CheckMouseClick();            
        }
    }    

    void CheckMouseOver(){
        if(collider.bounds.Contains (MouseUtilities.getMouseWorldPosition())){            
            doMouseOverState();            
        }
        else{
            if(actorState != ActorState.SELECTED){
                setReady();
            }            
        }
    }

    void CheckMouseClick(){
        if(actorState == ActorState.MOUSEOVER){
            if(Input.GetMouseButtonDown(0)){                
                Debug.Log("You Clicked a hero!: " + gameObject.name);
                HeroManager.setCurrentHero(this);                
            }
        }        
        if(Input.GetMouseButtonDown(1)){
            Debug.Log("cancel hero move");
            if(HeroManager.heroDaddy.selectedHero != null){
                Debug.Log("cancel hero move initiated");
                HeroManager.heroDaddy.selectedHero = null;
                HeroManager.clearMove();
            }
        }        
    }

    void checkAnim(){
        switch(actorState){
            case ActorState.READY:
                anim.SetBool("Ready", true);
                anim.SetBool("March", false);
                anim.SetBool("Finished", false);
                anim.SetBool("Selected", false);
                anim.SetBool("Dying", false);
                break;
            case ActorState.MOVING:
                anim.SetBool("Ready", false);
                anim.SetBool("March", true);
                anim.SetBool("Finished", false);
                anim.SetBool("Selected", false);
                anim.SetBool("Dying", false);
                break;
            case ActorState.FINISHED:
                anim.SetBool("Ready", false);
                anim.SetBool("March", false);
                anim.SetBool("Finished", true);
                anim.SetBool("Selected", false);
                anim.SetBool("Dying", false);
                break;
            case ActorState.MOUSEOVER:
                anim.SetBool("Ready", false);
                anim.SetBool("March", false);
                anim.SetBool("Finished", false);
                anim.SetBool("Selected", true);
                anim.SetBool("Dying", false);
                break;
            case ActorState.SELECTED:
                anim.SetBool("Ready", false);
                anim.SetBool("March", false);
                anim.SetBool("Finished", false);
                anim.SetBool("Selected", true);
                anim.SetBool("Dying", false);
                break;
            case ActorState.DYING:
                anim.SetBool("Ready", false);
                anim.SetBool("March", false);
                anim.SetBool("Finished", false);
                anim.SetBool("Selected", false);
                anim.SetBool("Dying", true);
                break;
            default:
                break;
        }
    }

    public override void kill(){
        Debug.Log("inside kill hero for: " + this.name);
        HeroManager.heroDaddy.heroList.Remove(this);
        TileManager.getTileAt(transform.position).containedActor = null;
        this.setDying();
        StartCoroutine(destroyWait(1));        
    }

    IEnumerator destroyWait(int seconds){
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }

    void doMouseOverState(){        
        setMouseOver();
    }
}