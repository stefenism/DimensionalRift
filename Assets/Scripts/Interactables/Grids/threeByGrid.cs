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

    Color defaultColor = new Color(1, 1, 1, 0);
    Color selectedColor = new Color(0.48f, 1, 0, 1);

    Color draggedColor = new Color(1, 0.45f, 0, 1);

    public List<Tile> gridTiles = new List<Tile>();

    void Start(){
        addToGridList();
        getTiles();
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
        else{
            setGridIdle();
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
                if(GridManager.getCurrentThreeBy() == null && GameManager.gameDaddy.isPlacementPhase()){
                    GridManager.setCurrentThreeBy(this);
                }
            }            
        }
        else if(isDragged()){
            if(Input.GetMouseButtonDown(0)){
                GridManager.checkDeleteGrid(this);
                GridManager.setCurrentThreeBy(null);
                GameManager.start_player_phase();
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

    void getTiles(){
        Tile[] allTiles;

        allTiles = transform.GetChild(0).GetComponentsInChildren<Tile>();
        foreach(Tile t in allTiles){
            gridTiles.Add(t);
            t.checkActors();
            TileManager.addTile(t);
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

    public void deleteGrid(){
        foreach(Tile t in gridTiles){
            if(t.containedActor != null){
                if(t.containedActor is Hero){
                    HeroManager.removeHero((Hero)t.containedActor);
                }
                else if(t.containedActor is Slime){
                    EnemyManager.removeSlime((Slime)t.containedActor);
                }
            }
            TileManager.removeTile(t);
        }
        GridManager.removeThreeBy(this);
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

    public void setGridIdle(){
        gridState = GridState.IDLE;
        doIdleState();
    }
    public void setGridMouseOver(){gridState = GridState.MOUSEOVER;}
    public void setGridSelected(){gridState = GridState.SELECTED;}
    public void setGridDragged(){gridState = GridState.DRAGGED;}
    
    public Vector2 getGridPosition(){return (Vector2)transform.position;}
    public void addToGridList(){GridManager.addThreeBy(this);}
}