using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    enum PlacementState{
        SELECT_GRID,
        PLACE_GRID,
    }
    
    PlacementState placementState = PlacementState.SELECT_GRID;
    
    public static GridManager gridDaddy = null;

    public GridContainer gridContainer;
    public threeByGrid selectedThreeBy = null;
    public threeByGrid copiedThreeBy = null;
    public List<threeByGrid> threeByList = new List<threeByGrid>();
    public List<Vector3> placementPositions = new List<Vector3>();

    void Awake(){
        if(gridDaddy == null){
            gridDaddy = this;
        }
        else if(gridDaddy == this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void cloneCurrentThreeBy(threeByGrid selectedGrid){        
        copiedThreeBy = Object.Instantiate(selectedGrid) as threeByGrid;
        copiedThreeBy.transform.SetParent(selectedGrid.transform.parent);
        copiedThreeBy.setGridDragged();
    }

    void getAvailablePlacementPositions(){
        foreach(threeByGrid g in gridDaddy.threeByList){
            gridDaddy.addAdjacentGridPositions(g);
        }
        for(int i = gridDaddy.placementPositions.Count - 1; i >= 0; i--){
            if(gridDaddy.placementPositions[i] == gridDaddy.selectedThreeBy.transform.position){
                gridDaddy.placementPositions.RemoveAt(i);
            }
        }        
    }

    void reset_copy(){        
        gridDaddy.selectedThreeBy.setGridIdle();            
        // gridDaddy.removeCopiedThreeBy();
        gridDaddy.placementPositions.Clear();        
        gridDaddy.selectedThreeBy = null;
        GameManager.start_player_phase();
        if(gridDaddy.copiedThreeBy != null){
            gridDaddy.copiedThreeBy.setGridIdle();
            gridDaddy.copiedThreeBy = null;
        }    
    }

    void cancel_copy(){
        removeCopiedThreeBy();        
    }

    static public void checkDeleteGrid(threeByGrid placedGrid){
        foreach (threeByGrid g in gridDaddy.threeByList){
            if(g.transform.position == placedGrid.transform.position){
                if(g != placedGrid){
                    Destroy(g.gameObject);
                    return;
                }
            }
        }        
    }

    static public Vector3 getClosestPosition(){
        Vector3 closestPosition = Vector3.zero;
        float smallestDistance = Mathf.Infinity;
        float distance = 0;

        foreach(Vector3 v in gridDaddy.placementPositions){
            distance = Mathf.Abs(((Vector3)MouseUtilities.getMouseWorldPosition() - v).magnitude);
            if(distance < smallestDistance){
                smallestDistance = distance;
                closestPosition = v;
            }
        }

        return closestPosition;
    }

    void addAdjacentGridPositions(threeByGrid gridToCheck){
        Vector3 currentPosition = gridToCheck.transform.position;        
        Vector3 up = new Vector3(currentPosition.x, currentPosition.y -3, currentPosition.z);
        Vector3 down = new Vector3(currentPosition.x, currentPosition.y + 3, currentPosition.z);
        Vector3 left = new Vector3(currentPosition.x -3, currentPosition.y, currentPosition.z);
        Vector3 right = new Vector3(currentPosition.x + 3, currentPosition.y, currentPosition.z);

        if(!gridDaddy.placementPositions.Contains(up) && gridDaddy.gridContainer.isInMap(up)){
            gridDaddy.placementPositions.Add(up);
        }
        if(!gridDaddy.placementPositions.Contains(down) && gridDaddy.gridContainer.isInMap(down)){
            gridDaddy.placementPositions.Add(down);
        }
        if(!gridDaddy.placementPositions.Contains(left) && gridDaddy.gridContainer.isInMap(left)){
            gridDaddy.placementPositions.Add(left);
        }
        if(!gridDaddy.placementPositions.Contains(right) && gridDaddy.gridContainer.isInMap(right)){
            gridDaddy.placementPositions.Add(right);
        }
    }

    static public void removeCopiedThreeBy(){
        gridDaddy.threeByList.Remove(gridDaddy.copiedThreeBy);
        Destroy(gridDaddy.copiedThreeBy.gameObject);
        gridDaddy.copiedThreeBy = null;
    }
    void set_select_grid(){placementState = PlacementState.SELECT_GRID;}
    void start_place_grid(){placementState = PlacementState.PLACE_GRID;}

    static public threeByGrid getCurrentThreeBy(){return gridDaddy.selectedThreeBy;}
    static public void setCurrentThreeBy(threeByGrid selectedGrid){        
        if(selectedGrid != null){
            gridDaddy.selectedThreeBy = selectedGrid;
            gridDaddy.cloneCurrentThreeBy(selectedGrid);
            gridDaddy.getAvailablePlacementPositions();
            selectedGrid.setGridSelected();
        }                
        if(selectedGrid == null){
            //send in null => reset copy
            gridDaddy.reset_copy();            
        }        
    }
    static public void addThreeBy(threeByGrid newGrid){        
        if(!gridDaddy.threeByList.Contains(newGrid)){            
            gridDaddy.threeByList.Add(newGrid);
        }        
    }

    static public List<threeByGrid> getThreeByList(){return gridDaddy.threeByList;}
    static public threeByGrid getSelectedThreeBy(){return gridDaddy.selectedThreeBy;}    
}