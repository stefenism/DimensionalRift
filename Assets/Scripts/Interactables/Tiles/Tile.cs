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