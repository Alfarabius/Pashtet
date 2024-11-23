using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Deck[] decks;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardsCanvasTransform;
    [SerializeField] private Transform cardSpawnTransform;
    
    private Player _player1;
    private Player _player2;
    
    [SerializeField] private PlayerView player1View;
    [SerializeField] private PlayerView player2View;
    
    private int _currentEpoch = 0;

    private int _currentPlayerIndex;
    private List<GameObject> _currentCards;

    private void Start()
    {
        _currentCards = new List<GameObject>();
        
        _player1 = gameObject.AddComponent<Player>();
        _player1.InitializePlayer("Player 1");
        player1View.Initialize(_player1);

        _player2 = gameObject.AddComponent<Player>();
        _player2.InitializePlayer("Player 2");
        player2View.Initialize(_player2);

        _currentPlayerIndex = 0; 
        
        PlayTurn();
    }

    private void PlayTurn()
    {
        List<CardData> drawnCards = decks[_currentEpoch].DrawCards(2);
        Vector3 offset = new Vector3(6f, 0, 0); // Смещение для карт
        
        Debug.Log(drawnCards.Count);

        for (int i = 0; i < drawnCards.Count; i++)
        {
            GameObject cardViewObject = Instantiate(cardPrefab, cardsCanvasTransform);
            CardView cardView = cardViewObject.GetComponent<CardView>();
            cardView.Initialize(drawnCards[i]);

            // Устанавливаем позицию с учетом смещения
            cardViewObject.transform.position = cardSpawnTransform.position + (offset * (i));
            _currentCards.Add(cardViewObject);

            // Добавляем коллайдер для обработки кликов
            BoxCollider2D boxCollider2D = cardViewObject.AddComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = true;

            // Добавляем компонент для обработки кликов
            CardClickHandler clickHandler = cardViewObject.AddComponent<CardClickHandler>();
            clickHandler.Initialize(drawnCards[i], this);
        }
    }
    
    public void OnCardClicked(CardData card)
    {
        if (_currentPlayerIndex == 0)
        {
            _player1.AddCard(card);
            player1View.OnPlayerDataChanged();
            Debug.Log($"{_player1.PlayerName} picked {card.CardName}");
        }
        else
        {
            _player2.AddCard(card);
            player2View.OnPlayerDataChanged();
            Debug.Log($"{_player2.PlayerName} picked {card.CardName}");
        }

        // Удаляем оставшиеся карты и передаем их другому игроку
        foreach (var cardObj in _currentCards)
        {
            Destroy(cardObj);
        }
        
        _currentCards.Clear();
        
        // Передаем оставшуюся карту другому игроку
        PlayTurn(); // Перераздаем карты
    }

    private bool IsGameOver()
    {
        return false; 
    }

    private void DetermineWinner()
    {
        Debug.Log("Determining winner...");
    }

    private void CheckEndConditions()
    {
        Debug.Log("Checking end conditions...");
    }

    private Player GetCurrentPlayer() 
    { 
        return _currentPlayerIndex == 0 ? _player1 : _player2; 
    }
}
