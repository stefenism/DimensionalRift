using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class threeByGrid : MonoBehaviour {

    enum GridState{
        IDLE,
        MOUSEOVER,
    }

    SpriteRenderer sprite;
    Collider2D collider;
    GridState gridState = GridState.IDLE;

    Color defaultColor = new Color(0.95f, 0.875f, 0, 0);
    Color selectedColor = new Color(0.95f, 0.875f, 0, 0.42f);

    void Awake() {
        addToGridList();    
    }

    void Start(){
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    void Update(){
        if(GameManager.gameDaddy.isPlacementPhase()){
            CheckMouseOver();
        }
    }

    void CheckMouseOver(){
        if(collider.bounds.Contains(MouseUtilities.getMouseWorldPosition())){
            gridState = GridState.MOUSEOVER;
            doMouseOverState();
        }
        else{
            gridState = GridState.IDLE;
            doIdleState();
        }
    }

    void doIdleState(){
        sprite.color = defaultColor;
    }

    void doMouseOverState(){
        sprite.color = selectedColor;
    }

    public bool isMouseOver(){return gridState == GridState.MOUSEOVER;}
    
    public Vector2 getGridPosition(){return (Vector2)transform.position;}
    public void addToGridList(){GridManager.addThreeBy(this);}
}