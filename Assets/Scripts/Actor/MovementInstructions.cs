using System.Collections.Generic;
using UnityEngine;

public class MovementInstructions : ScriptableObject {

    enum MovementType{
        ATTACK,
        MOVE,
    }

    MovementType movementType = MovementType.MOVE;
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

    public bool isMove(){return movementType == MovementType.MOVE;}
    public bool isAttack(){return movementType == MovementType.ATTACK;}

    public void setAsMove(){movementType = MovementType.MOVE;}
    public void setAsAttack(){movementType = MovementType.ATTACK;}
}