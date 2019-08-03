using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour{

    enum TileState{
        IDLE,
        MOUSEOVER,
        MOVEABLE,
    }

    SpriteRenderer sprite;
    Collider2D collider;
    TileState tileState = TileState.IDLE;

    Color defaultColor = new Color(1, 1, 1, 1);
    Color selectedColor = new Color(0.48f, 1, 0, 1);
    Color moveableColor = new Color(1, 0.45f, 0, 1);

    public Actor containedActor;

    void Start(){
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    void Update () {
        if(GameManager.gameDaddy.isPlayerTurn()){
            if(HeroManager.heroDaddy.selectedHero == null){
                CheckMouseOver();
                //THIS WILL HAVE TO CHANGE
            }            
            if(isMoveable()){
                doTileMoveable();
            }
        }        
    }

    void CheckMouseOver(){
        if(collider.bounds.Contains (MouseUtilities.getMouseWorldPosition())){
            tileState = TileState.MOUSEOVER;
            doMouseOverState();
        }
        else{
            if(tileState != TileState.MOVEABLE){
                tileState = TileState.IDLE;
                doIdleState();
            }            
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

    public bool isInTile(Vector3 position){
        if(collider.bounds.Contains (position)){
            return true;
        }
        return false;
    }

    void doIdleState(){
        sprite.color = defaultColor;
    }

    void doMouseOverState(){
        sprite.color = selectedColor;
    }

    void doTileMoveable(){
        sprite.color = moveableColor;
    }

    public bool isMoveable(){return tileState == TileState.MOVEABLE;}

    public void setMove(){tileState = TileState.MOVEABLE;}

    public bool isMouseOver(){return tileState == TileState.MOUSEOVER;}
}