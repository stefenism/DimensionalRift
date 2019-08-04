using UnityEngine;

public class Actor : MonoBehaviour {
    
    public enum ActorState{
        READY,
        MOUSEOVER,
        SELECTED,
        MOVING,
        ATTACKING,
        FINISHED,
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

    public bool isReady(){return actorState == ActorState.READY;}
    public bool isSelected(){return actorState == ActorState.SELECTED;}
    public bool isMoving(){return actorState == ActorState.MOVING;}
    public bool isAttacking(){return actorState == ActorState.ATTACKING;}
    public bool isFinished(){return actorState == ActorState.FINISHED;}

    public void setReady(){actorState = ActorState.READY;}
    public void setMouseOver(){actorState = ActorState.MOUSEOVER;}
    public void setMoving(){actorState = ActorState.MOVING;}
    public void setAttacking(){actorState = ActorState.ATTACKING;}
    public void setFinished(){actorState = ActorState.FINISHED;}

    public Collider2D GetCollider(){return collider;}
}