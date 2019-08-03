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

    Color defaultColor = new Color(1f,1f,1f);
    Color selectedColor = new Color(0.95f,0.875f,0, 0.42f);

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

    void doIdleState(){
        sprite.color = defaultColor;
    }

    void doMouseOverState(){
        sprite.color = selectedColor;
    }

    public bool isMouseOver(){return tileState == TileState.MOUSEOVER;}
}