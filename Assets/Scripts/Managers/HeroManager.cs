using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour {

    public static HeroManager heroDaddy = null;

    public List<Hero> heroList = new List<Hero>();
    public Hero selectedHero = null;
    public List<Tile> availableMoves = new List<Tile>();
    public List<MovementInstructions> movements = new List<MovementInstructions>();
    public List<Vector3> moveSearchList = new List<Vector3>();  

    Coroutine doMovements = null;  

    void Awake(){
        if(heroDaddy == null){
            heroDaddy = this;
        }
        else if(heroDaddy == this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update(){
        checkAllMoves();
    }

    void checkAllMoves(){
        foreach (Hero h in heroDaddy.heroList){
            if(!h.isFinished()){
                // Debug.Log("we ain't done yet");
                return;
            }
        }
        // Debug.Log("all heros are done");
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

        checkAddTile(up, positionToCheck, checkMoveable);
        checkAddTile(down, positionToCheck, checkMoveable);
        checkAddTile(left, positionToCheck, checkMoveable);
        checkAddTile(right, positionToCheck, checkMoveable);
    }

    void checkAddTile(Vector3 checkTilePosition, Vector3 checkFromPosition, bool checkMoveable){
        Tile checkTile = TileManager.getTileAt(checkTilePosition);
        Debug.Log("checking movement tile: " + checkTile);
        if(checkTile != null){
            if(checkTile.containedActor == null){
                if(!heroDaddy.availableMoves.Contains(checkTile)){
                    heroDaddy.availableMoves.Add(checkTile);
                    if(checkFromPosition != heroDaddy.selectedHero.transform.position){
                        MovementInstructions newMovement = new MovementInstructions();
                        newMovement.name = newMovement.name + " " + heroDaddy.movements.Count;
                        newMovement.addTile(TileManager.getTileAt(checkFromPosition));
                        newMovement.addTile(checkTile);
                        movements.Add(newMovement);
                    }
                    else{
                        MovementInstructions newMovement = new MovementInstructions();
                        newMovement.name = newMovement.name + " " + heroDaddy.movements.Count;
                        newMovement.addTile(checkTile);
                        movements.Add(newMovement);
                    }
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

    public void doMovement(Tile tile){
        MovementInstructions movement = heroDaddy.getMovement(tile);
        if(movement != null){
            doMovements = StartCoroutine(doInstruction(movement));
        }
    }

    public MovementInstructions getMovement(Tile tile){
        foreach(MovementInstructions m in movements){
            if(m.isDestination(tile)){
                Debug.Log("the movement you're looking for is: " + m);
                return m;
            }
        }
        return null;
    }

    static public void clearMove(){
        TileManager.resetTiles();
        heroDaddy.availableMoves.Clear();
        heroDaddy.moveSearchList.Clear();
        heroDaddy.movements.Clear();
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

    void resolve_instruction(MovementInstructions movement, Vector3 originalHeroPosition, int cycle){
        // Vector3 currentPosition = heroDaddy.selectedHero.transform.position;
        // currentPosition = Vector3.Lerp(currentPosition, movement.getTileList()[0].transform.position, 0.1f + cycle);
        heroDaddy.selectedHero.transform.position = Vector3.Lerp(originalHeroPosition, movement.getTileList()[0].transform.position, 0.05f * cycle);
    }

    IEnumerator doInstruction(MovementInstructions currentMovement){
        Vector3 originalHeroPosition = heroDaddy.selectedHero.transform.position;
        int cycle = 0;
        heroDaddy.selectedHero.setMoving();
        while(heroDaddy.selectedHero.transform.position != currentMovement.getTileList()[0].transform.position){
            resolve_instruction(currentMovement, originalHeroPosition, cycle);
            cycle += 1;
            yield return 0;
        }        
        currentMovement.getTileList().RemoveAt(0);
        StopCoroutine(doMovements);
        if(currentMovement.getTileList().Count > 0){
            doMovements = StartCoroutine(doInstruction(currentMovement));
        }
        else{
            doMovements = null;
            Debug.Log("weve arrived");
            heroDaddy.selectedHero.setFinished();
            Tile newTile = TileManager.getTileAt(heroDaddy.selectedHero.transform.position);
            heroDaddy.selectedHero.transform.SetParent(newTile.gameObject.transform);
            HeroManager.clearMove();
        }
    }
    
}