using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour {

    public static HeroManager heroDaddy = null;

    public List<Hero> heroList = new List<Hero>();
    public Hero selectedHero = null;
    public List<Tile> availableMoves = new List<Tile>();
    public List<Vector3> moveSearchList = new List<Vector3>();    

    void Awake(){
        if(heroDaddy == null){
            heroDaddy = this;
        }
        else if(heroDaddy == this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void getAvailableMoves(){
        Vector3 originalPosition = heroDaddy.selectedHero.transform.position;
        heroDaddy.checkAdjacentPositions(originalPosition);
        foreach(Vector3 p in heroDaddy.moveSearchList){
            heroDaddy.checkAdjacentPositions(p, false);
        }        
        foreach(Tile t in heroDaddy.availableMoves){
            t.setMove();
        }
    }

    void checkAdjacentPositions(Vector3 positionToCheck, bool checkMoveable = true){
        Vector3 up = new Vector3(positionToCheck.x, positionToCheck.y -1, positionToCheck.z);
        Vector3 down = new Vector3(positionToCheck.x, positionToCheck.y + 1, positionToCheck.z);
        Vector3 left = new Vector3(positionToCheck.x -1, positionToCheck.y, positionToCheck.z);
        Vector3 right = new Vector3(positionToCheck.x + 1, positionToCheck.y, positionToCheck.z);

        checkAddTile(up, checkMoveable);
        checkAddTile(down, checkMoveable);
        checkAddTile(left, checkMoveable);
        checkAddTile(right, checkMoveable);
    }

    void checkAddTile(Vector3 checkTilePosition, bool checkMoveable){
        Tile checkTile = TileManager.getTileAt(checkTilePosition);
        Debug.Log("checking movement tile: " + checkTile);
        if(checkTile != null){
            if(checkTile.containedActor == null){
                if(!heroDaddy.availableMoves.Contains(checkTile)){
                    heroDaddy.availableMoves.Add(checkTile);                    
                }
                if(!heroDaddy.moveSearchList.Contains(checkTile.transform.position) && checkMoveable){
                    heroDaddy.moveSearchList.Add(checkTile.transform.position);   
                }                
            }
            else if(checkTile.containedActor is Hero && checkMoveable){
                if(!heroDaddy.moveSearchList.Contains(checkTile.transform.position)){
                    heroDaddy.moveSearchList.Add(checkTile.transform.position);
                }                
            }
        }
    }

    static public void clearMove(){
        TileManager.resetTiles();
        heroDaddy.availableMoves.Clear();
        heroDaddy.moveSearchList.Clear();
        heroDaddy.selectedHero = null;
    }

    static public void addHero(Hero newHero){
        if(!heroDaddy.heroList.Contains(newHero)){
            heroDaddy.heroList.Add(newHero);
        }
    }

    static public void removeHero(Hero hero){
        if(heroDaddy.heroList.Contains(hero)){
            heroDaddy.heroList.Remove(hero);
        }
    }

    static public Hero getCurrentHero(){return heroDaddy.selectedHero;}

    static public void setCurrentHero(Hero newHero){
        clearMove();
        heroDaddy.selectedHero = newHero;
        heroDaddy.getAvailableMoves();
    }
    
}