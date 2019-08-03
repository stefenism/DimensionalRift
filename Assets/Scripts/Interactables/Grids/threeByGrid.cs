using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class threeByGrid : MonoBehaviour {

    enum GridState{
        IDLE,
        MOUSEOVER,
        SELECTED,
        DRAGGED,
    }

    SpriteRenderer sprite;
    Collider2D collider;
    GridState gridState = GridState.IDLE;

    Color defaultColor = new Color(0.95f, 0.875f, 0, 0);
    Color selectedColor = new Color(0.95f, 0.875f, 0, 0.42f);

    Color draggedColor = new Color(0, 0.08f, 1, 0.42f);

    void Awake() {
        addToGridList();    
    }

    void Start(){
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    void Update(){
        if(GameManager.gameDaddy.isPlacementPhase()){
            // CheckMouseOver();
            CheckMouseClick();
            checkMouseDrop();
            if(isDragged()){
                transform.position = GridManager.getClosestPosition();
                doDraggedState();
            }
            else if(isSelected()){
                doSelectedState();
            }
            else{                
                CheckMouseOver();                
            }
        }
    }

    void CheckMouseOver(){
        if(collider.bounds.Contains(MouseUtilities.getMouseWorldPosition())){
            setGridMouseOver();
            doMouseOverState();
        }
        else{            
            setGridIdle();
            doIdleState();                  
        }
    }

    void CheckMouseClick(){
        if(this.gridState == GridState.MOUSEOVER){
            if(Input.GetMouseButtonDown(0)){                
                if(GridManager.getCurrentThreeBy() == null){
                    GridManager.setCurrentThreeBy(this);
                }
            }            
        }
        else if(isDragged()){
            if(Input.GetMouseButtonDown(0)){
                setGridIdle();
                GridManager.setCurrentThreeBy(null);
            }
        }
    }

    void checkMouseDrop(){
        if(Input.GetMouseButtonDown(1)){
                if(GridManager.getCurrentThreeBy() == this){
                    GridManager.removeCopiedThreeBy();
                    GridManager.setCurrentThreeBy(null);                    
                }
            }
    }

    void doIdleState(){
        sprite.color = defaultColor;
    }

    void doMouseOverState(){
        sprite.color = selectedColor;
    }

    void doDraggedState(){
        sprite.color = draggedColor;
    }

    void doSelectedState(){
        sprite.color = selectedColor;
    }

    public bool isGridAtPosition(Vector3 position){
        if(transform.position == position){
            return true;
        }
        return false;
    }

    public bool isIdle(){return gridState == GridState.IDLE;}
    public bool isMouseOver(){return gridState == GridState.MOUSEOVER;}
    public bool isSelected(){return gridState == GridState.SELECTED;}
    public bool isDragged(){return gridState == GridState.DRAGGED;}

    public void setGridIdle(){gridState = GridState.IDLE;}
    public void setGridMouseOver(){gridState = GridState.MOUSEOVER;}
    public void setGridSelected(){gridState = GridState.SELECTED;}
    public void setGridDragged(){gridState = GridState.DRAGGED;}
    
    public Vector2 getGridPosition(){return (Vector2)transform.position;}
    public void addToGridList(){GridManager.addThreeBy(this);}
}