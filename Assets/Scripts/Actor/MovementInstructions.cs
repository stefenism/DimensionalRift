using System.Collections.Generic;
using UnityEngine;

public class MovementInstructions : ScriptableObject {
    List<Tile> tileList = new List<Tile>();

    public List<Tile> getTileList(){
        return tileList;
    }

    public bool isDestination(Tile tilePosition){
        return tileList[tileList.Count-1] == tilePosition;
    }

    public void addTile(Tile newTile){
        tileList.Add(newTile);
    }
}