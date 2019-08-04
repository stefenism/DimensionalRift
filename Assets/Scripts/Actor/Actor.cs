using UnityEngine;

public class Actor : MonoBehaviour {
    
    public enum ActorState{
        READY,
        MOUSEOVER,
        SELECTED,
        MOVING,
        ATTACKREADY,
        ATTACKING,
        FINISHED,
        DYING,
    }

    public ActorState actorState = ActorState.READY;

    public SpriteRenderer sprite;
    public Collider2D collider;
    public Animator anim;

    void Start(){
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    public virtual void kill(){}
    public bool isReady(){return actorState == ActorState.READY;}
    public bool isSelected(){return actorState == ActorState.SELECTED;}
    public bool isMoving(){return actorState == ActorState.MOVING;}
    public bool isAttakReady(){return actorState == ActorState.ATTACKREADY;}
    public bool isAttacking(){return actorState == ActorState.ATTACKING;}
    public bool isFinished(){return actorState == ActorState.FINISHED;}
    public bool isDying(){return actorState == ActorState.DYING;}

    public void setReady(){actorState = ActorState.READY;}
    public void setMouseOver(){actorState = ActorState.MOUSEOVER;}
    public void setMoving(){actorState = ActorState.MOVING;}
    public void setAttackReady(){actorState = ActorState.ATTACKREADY;}
    public void setAttacking(){actorState = ActorState.ATTACKING;}
    public void setFinished(){actorState = ActorState.FINISHED;}
    public void setDying(){actorState = ActorState.DYING;}

    public Collider2D GetCollider(){return collider;}
}