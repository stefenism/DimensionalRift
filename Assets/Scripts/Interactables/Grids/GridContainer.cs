using UnityEngine;

public class GridContainer : MonoBehaviour {
    
    Collider2D containerCollider;
    private void Awake() {
        containerCollider = GetComponent<Collider2D>();
    }

    public bool isInMap(Vector3 position){
        if(containerCollider.bounds.Contains(position)){
            return true;
        }
        return false;
    }
}