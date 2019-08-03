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
        Debug.Log("gonna clone a threeby: " + selectedGrid);
        copiedThreeBy = Object.Instantiate(selectedGrid) as threeByGrid;
        copiedThreeBy.setGridDragged();
    }

    void getAvailablePlacementPositions(){

    }

    void removeCopiedThreeBy(){
        Destroy(gridDaddy.copiedThreeBy.gameObject);
        gridDaddy.copiedThreeBy = null;
    }
    void set_select_grid(){placementState = PlacementState.SELECT_GRID;}
    void start_place_grid(){placementState = PlacementState.PLACE_GRID;}

    static public threeByGrid getCurrentThreeBy(){return gridDaddy.selectedThreeBy;}
    static public void setCurrentThreeBy(threeByGrid selectedGrid){
        gridDaddy.selectedThreeBy = selectedGrid;        
        if(selectedGrid == null){
            gridDaddy.removeCopiedThreeBy();
        }
        else{
            gridDaddy.cloneCurrentThreeBy(selectedGrid);
            gridDaddy.getAvailablePlacementPositions();
        }
    }
    static public void addThreeBy(threeByGrid newGrid){
        Debug.Log("adding a three by");
        Debug.Log("griddaddy is: " + gridDaddy.gameObject.name);
        if(!gridDaddy.threeByList.Contains(newGrid)){
            Debug.Log("adding a grid");
            gridDaddy.threeByList.Add(newGrid);
        }        
    }

    static public List<threeByGrid> getThreeByList(){return gridDaddy.threeByList;}
    static public threeByGrid getSelectedThreeBy(){return gridDaddy.selectedThreeBy;}    
}