using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    
    public static EnemyManager enemyDaddy = null;

    public List<Slime> slimeList = new List<Slime>();
    public List<Tile> availableMoves = new List<Tile>();
    public Tile juiceMove = null;
    
    Coroutine doMovement = null;

    void Awake(){
        if(enemyDaddy == null){
            enemyDaddy = this;
        }
        else if(enemyDaddy == this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.F)){
            doEnemyTurn();
        }
    }

    static public void doEnemyTurn(){
        foreach(Slime s in enemyDaddy.slimeList){            
            if(s.isAttacking()){
                enemyDaddy.attack(s);
                continue;
            }
            else{
                enemyDaddy.getAvailableMoves(s);
            }
            enemyDaddy.availableMoves.Clear();
            enemyDaddy.juiceMove = null;
            enemyDaddy.doMovement = null;
        }
    }

    void getAvailableMoves(Slime slime){
        Vector3 originalPosition = slime.transform.position;
        enemyDaddy.checkAdjacentPositions(originalPosition);
        enemyDaddy.evaluateChoices(slime);
    }

    void checkAdjacentPositions(Vector3 positionToCheck){
        Vector3 up = new Vector3(positionToCheck.x, positionToCheck.y -1, positionToCheck.z);
        Vector3 down = new Vector3(positionToCheck.x, positionToCheck.y + 1, positionToCheck.z);
        Vector3 left = new Vector3(positionToCheck.x -1, positionToCheck.y, positionToCheck.z);
        Vector3 right = new Vector3(positionToCheck.x + 1, positionToCheck.y, positionToCheck.z);

        checkAddTile(up);
        checkAddTile(down);
        checkAddTile(left);
        checkAddTile(right);
    }

    void checkAddTile(Vector3 checkTilePosition){
        Tile checkTile = TileManager.getTileAt(checkTilePosition);
        if(checkTile != null){
            if(checkTile.containedActor == null){
                enemyDaddy.availableMoves.Add(checkTile);
            }
            else if(checkTile.containedActor is Hero){
                enemyDaddy.availableMoves.Add(checkTile);
                juiceMove = checkTile;
            }            
        }
    }

    void evaluateChoices(Slime slime){
        //go through moves
        //if move has a hero => attack
        //otherwise move towards the closest hero
        if(juiceMove != null){
            //attack
            slime.setAttacking();
            return;
        }
        foreach(Tile t in availableMoves){
            Vector3 closestHero = getClosestHero(slime);
            Tile bestTile = getBestTile(slime, closestHero);
            enemyDaddy.moveSlime(slime, bestTile);
        }
    }

    void attack(Slime slime){
        attackAdjacentPositions(slime);
    }

    void attackAdjacentPositions(Slime slime){
        Vector3 position = slime.transform.position;
        Vector3 up = new Vector3(position.x, position.y -1, position.z);
        Vector3 down = new Vector3(position.x, position.y + 1, position.z);
        Vector3 left = new Vector3(position.x -1, position.y, position.z);
        Vector3 right = new Vector3(position.x + 1, position.y, position.z);

        attackTile(up);
        attackTile(down);
        attackTile(left);
        attackTile(right);

        slime.setReady();
    }

    void attackTile(Vector3 tilePosition){
        Tile tileToAttack = TileManager.getTileAt(tilePosition);
        if(tileToAttack != null){
            Actor containedActor = tileToAttack.containedActor;
            if(containedActor != null && containedActor is Hero){
                //kill hero
                Debug.Log("you killed a hero");
            }
        }                
    }

    Vector3 getClosestHero(Slime slime){
        Vector3 closestPosition = Vector3.zero;
        float smallestDistance = Mathf.Infinity;
        float distance = 0;

        foreach(Hero h in HeroManager.heroDaddy.heroList){
            distance = Mathf.Abs((h.transform.position - slime.transform.position).magnitude);
            if(distance < smallestDistance){
                smallestDistance = distance;
                closestPosition = h.transform.position;
            }
        }

        return closestPosition;
    }

    Tile getBestTile(Slime slime, Vector3 closestHero){
        Tile closestPosition = null;
        float smallestDistance = Mathf.Infinity;
        float distance = 0;

        foreach(Tile t in enemyDaddy.availableMoves){
            distance = Mathf.Abs((t.transform.position - closestHero).magnitude);
            if(distance < smallestDistance){
                smallestDistance = distance;
                closestPosition = t;
            }
        }

        return closestPosition;
    }

    void moveSlime(Slime slime, Tile tile){
        Vector3 originalPosition = slime.transform.position;
        TileManager.getTileAt(originalPosition).containedActor = null;
        int cycle = 0;
        while(slime.transform.position != tile.transform.position){
            slime.transform.position = Vector3.Lerp(originalPosition, tile.transform.position, 0.05f * cycle);
            cycle++;
        }
        Tile newTile = TileManager.getTileAt(slime.transform.position);        
        slime.transform.SetParent(newTile.transform);
        newTile.containedActor = slime;
    }

    static public void addSlime(Slime newSlime){
        if(!enemyDaddy.slimeList.Contains(newSlime)){
            enemyDaddy.slimeList.Add(newSlime);
        }
    }
}