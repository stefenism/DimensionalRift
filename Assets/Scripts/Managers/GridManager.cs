using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
    
    public static GridManager gridDaddy = null;
    public threeByGrid selectedThreeBy = null;
    public List<threeByGrid> threeByList = new List<threeByGrid>();

    void Awake(){
        if(gridDaddy == null){
            gridDaddy = this;
        }
        else if(gridDaddy == this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
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