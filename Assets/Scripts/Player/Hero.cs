using UnityEngine;

public class Hero : Actor {
    
    public int movementDistance = 2;

    void Update(){
        if(GameManager.gameDaddy.isPlayerTurn()){
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

    void doMouseOverState(){        
        setMouseOver();
    }
}