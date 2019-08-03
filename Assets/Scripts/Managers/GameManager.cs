using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

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
}
