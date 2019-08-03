using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState{
        PLAYER_TURN,
        PLACEMENT_PHASE,
        ENEMY_TURN,
    }

    GameState gameState = GameState.PLAYER_TURN;

    public static GameManager gameDaddy = null; //Static instance of GameManager allows it to be called from anywhere    
    void Awake(){
        //Check to see if there's a gameDaddy
        if(gameDaddy == null){
            //if there isn't. this will be our gameDaddy
            gameDaddy = this;
        }
        //otherwise
        else if (gameDaddy == this){
            //KILL IT DEAD!
            Destroy(gameObject);
        }
        //Keep our GameDaddy 4 E-V-E-R
        DontDestroyOnLoad(gameObject);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            start_placement_phase();
        }
    }

    public bool isPlayerTurn(){return gameState == GameState.PLAYER_TURN;}
    public bool isPlacementPhase(){return gameState == GameState.PLACEMENT_PHASE;}

    void start_placement_phase(){gameState = GameState.PLACEMENT_PHASE;}
}
