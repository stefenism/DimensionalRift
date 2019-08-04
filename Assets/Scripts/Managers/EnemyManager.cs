using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    
    public static EnemyManager enemyDaddy = null;

    public List<Slime> slimeList = new List<Slime>();
    public List<Tile> availableMoves = new List<Tile>();
    public Tile juiceMove = null;
    
    Coroutine doMovement = null;
    Coroutine resolveEnemyTurn = null;
    Coroutine slimeTurn = null;

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
        CheckEnemies();        
    }

    void CheckEnemies(){
        if(enemyDaddy.slimeList.Count <= 0){
            Debug.Log("win game");
            GameManager.gameDaddy.endGamePrompt.SetActive(true);
        }
    }

    static public void doEnemyTurn(){
        // foreach(Slime s in enemyDaddy.slimeList){            
        //     if(s.isAttacking()){
        //         enemyDaddy.attack(s);
        //         continue;
        //     }
        //     else{
        //         enemyDaddy.getAvailableMoves(s);
        //     }
        //     enemyDaddy.availableMoves.Clear();
        //     enemyDaddy.juiceMove = null;
        //     enemyDaddy.doMovement = null;
        // }
        enemyDaddy.resolveEnemyTurn = enemyDaddy.StartCoroutine(enemyDaddy.processTurn());
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
            slime.setAttackReady();
            return;
        }
        foreach(Tile t in availableMoves){
            Vector3 closestHero = getClosestHero(slime);
            Tile bestTile = getBestTile(slime, closestHero);
            doMovement = StartCoroutine(moveSlime(slime, bestTile));
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

        // slime.setReady();
    }

    void attackTile(Vector3 tilePosition){
        Tile tileToAttack = TileManager.getTileAt(tilePosition);
        if(tileToAttack != null){
            Actor containedActor = tileToAttack.containedActor;
            if(containedActor != null && containedActor is Hero){
                //kill hero
                Debug.Log("you killed a hero");
                containedActor.kill();
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

    static public void addSlime(Slime newSlime){
        if(!enemyDaddy.slimeList.Contains(newSlime)){
            enemyDaddy.slimeList.Add(newSlime);
        }
    }

    static public void removeSlime(Slime removeSlime){
        if(enemyDaddy.slimeList.Contains(removeSlime)){
            enemyDaddy.slimeList.Remove(removeSlime);
        }
    }

    IEnumerator moveSlime(Slime slime, Tile tile){
        Vector3 originalPosition = slime.transform.position;
        TileManager.getTileAt(originalPosition).containedActor = null;
        int cycle = 0;
        slime.setMoving();
        while(slime.transform.position != tile.transform.position){
            slime.transform.position = Vector3.Lerp(originalPosition, tile.transform.position, 0.05f * cycle);
            cycle++;
            yield return 0;
        }
        Tile newTile = TileManager.getTileAt(slime.transform.position);        
        slime.transform.SetParent(newTile.transform);
        slime.setReady();
        newTile.containedActor = slime;
        doMovement = null;        
    }

    IEnumerator doSlimeTurn(Slime slime){
        if(slime.isAttakReady()){            
            slime.setAttacking();
            enemyDaddy.attack(slime);
        }
        else{
            // enemyDaddy.getAvailableMoves(slime);
            Vector3 originalPosition = slime.transform.position;
            enemyDaddy.checkAdjacentPositions(originalPosition);
            if(enemyDaddy.availableMoves.Count <= 0){
                Debug.Log("AINT NO FRIGGAN MOVES");                
            }
            else{
                // enemyDaddy.evaluateChoices(slime);
                //go through moves
                //if move has a hero => attack
                //otherwise move towards the closest hero
                if(juiceMove != null){                
                    //attack
                    slime.setAttackReady();
                    if(slimeTurn != null){
                        StopCoroutine(slimeTurn);
                        slimeTurn = null;
                    }                
                }
                else{
                    Vector3 closestHero = getClosestHero(slime);
                    Tile bestTile = getBestTile(slime, closestHero);                                
                    yield return doMovement = StartCoroutine(moveSlime(slime, bestTile));        
                }            
            }            
        }            
        enemyDaddy.availableMoves.Clear();
        enemyDaddy.juiceMove = null;
        enemyDaddy.doMovement = null;        
    }

    IEnumerator processTurn(){        
        foreach(Slime s in enemyDaddy.slimeList){
            yield return slimeTurn = StartCoroutine(doSlimeTurn(s));                            
            
        }

        resolveEnemyTurn = null;
        GameManager.start_placement_phase();
    }
}