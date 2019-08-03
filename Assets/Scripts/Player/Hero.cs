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
            setReady();
        }
    }

    void CheckMouseClick(){
        if(actorState == ActorState.MOUSEOVER){
            if(Input.GetMouseButtonDown(0)){                
                Debug.Log("You Clicked a hero!: " + gameObject.name);
                HeroManager.setCurrentHero(this);                
            }
        }
    }

    void doMouseOverState(){        
        setMouseOver();
    }
}