using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour {

    public static HeroManager heroDaddy = null;

    public List<Hero> heroList = new List<Hero>();
    public Hero selectedHero = null;
    public List<Tile> availableMoves = new List<Tile>();

    void Awake(){
        if(heroDaddy == null){
            heroDaddy = this;
        }
        else if(heroDaddy == this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
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
        heroDaddy.selectedHero = newHero;
    }
    
}