using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class endTurnButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {
    
    public Button button;
    public Sprite defaultSprite;
    public Sprite hoverSprite;
    public Sprite clickDownSprite;
    public Sprite finishedSprite;

    void Start(){
        button = GetComponent<Button>();
    }

    void Update(){
        if(!GameManager.gameDaddy.isPlayerTurn()){
            button.image.sprite = finishedSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData){
        button.image.sprite = hoverSprite;
    }
    public void OnPointerExit(PointerEventData eventData){
        button.image.sprite = defaultSprite;
    }

    public void OnPointerDown(PointerEventData eventData){
        button.image.sprite = clickDownSprite;
    }

    public void OnPointerUp(PointerEventData eventData){
        button.image.sprite = defaultSprite;
    }

    public void endTurn(){
        Debug.Log("ending turn");
        GameManager.start_enemy_phase();
    }

    public void startTurn(){
        button.image.sprite = defaultSprite;
    }

}