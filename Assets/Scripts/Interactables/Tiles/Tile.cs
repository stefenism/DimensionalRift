using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour{

    enum TileState{
        IDLE,
        MOUSEOVER,
    }

    SpriteRenderer sprite;
    Collider2D collider;
    TileState tileState = TileState.IDLE;

    Color defaultColor = new Color(1, 1, 1, 1);
    Color selectedColor = new Color(0.48f, 1, 0, 1);

    Actor containedActor;

    void Start(){
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    void Update () {
        if(GameManager.gameDaddy.isPlayerTurn()){
            CheckMouseOver();
        }        
    }

    void CheckMouseOver(){
        if(collider.bounds.Contains (MouseUtilities.getMouseWorldPosition())){
            tileState = TileState.MOUSEOVER;
            doMouseOverState();
        }
        else{
            tileState = TileState.IDLE;
            doIdleState();
        }
    }

    public void checkActors(){
        if(transform.childCount <= 0){
            return;
        }
        containedActor = transform.GetChild(0).GetComponent<Actor>();
        if(containedActor != null){
            if(containedActor is Hero){
                HeroManager.addHero((Hero)containedActor);
            }
            else{
                Debug.Log("not a hero");
            }
        }
    }

    void doIdleState(){
        sprite.color = defaultColor;
    }

    void doMouseOverState(){
        sprite.color = selectedColor;
    }

    public bool isMouseOver(){return tileState == TileState.MOUSEOVER;}
}