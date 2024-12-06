using UnityEngine;

public class CardClickHandler : MonoBehaviour
{
    private CardData _cardData;
    private GameController _gameController;

    public void Initialize(CardData card, GameController controller)
    {
        _cardData = card;
        _gameController = controller;
    }

    private void OnMouseDown()
    {
        _gameController.OnCardClicked(_cardData);
        
        // Удаляем объект карты после клика
        Destroy(gameObject);
    }
}
