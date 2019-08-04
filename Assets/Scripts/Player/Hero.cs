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
                break;
            case ActorState.FINISHED:
                anim.SetBool("Ready", false);
                anim.SetBool("March", false);
                anim.SetBool("Finished", true);
                break;
        }
    }

    void doMouseOverState(){        
        setMouseOver();
    }
}