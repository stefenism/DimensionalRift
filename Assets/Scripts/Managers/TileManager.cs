using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    
    public static TileManager tileDaddy = null;

    public List<Tile> tileList = new List<Tile>();

    void Awake(){
        if(tileDaddy == null){
            tileDaddy = this;
        }
        else if (tileDaddy == this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    static public void resetTiles(){
        foreach(Tile t in tileDaddy.tileList){
            t.setIdle();            
        }
    }

    static public void addTile(Tile newTile){
        if(!tileDaddy.tileList.Contains(newTile)){
            tileDaddy.tileList.Add(newTile);
        }
    }

    static public Tile getTileAt(Vector3 position){
        foreach (Tile t in tileDaddy.tileList){        
            if(t.isInTile(position)){
                return t;
            }
        }
        return null;
    }
}