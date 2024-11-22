using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Deck[] decks; // Массив колод
    private Player _player1;
    private Player _player2;

    private int _currentPlayerIndex;

    private void Start()
    {
        _player1 = gameObject.AddComponent<Player>();
        _player1.InitializePlayer("Player 1");

        _player2 = gameObject.AddComponent<Player>();
        _player2.InitializePlayer("Player 2");

        _currentPlayerIndex = 0; 
        StartGame();
    }

    private void StartGame()
    {
        while (!IsGameOver())
        {
            PlayTurn();
            _currentPlayerIndex = (_currentPlayerIndex + 1) % 2; 
        }

        DetermineWinner();
    }

    private void PlayTurn()
    {
        List<CardData> drawnCards = decks[_currentPlayerIndex].DrawCards(2);

        foreach (var card in drawnCards)
        {
            GetCurrentPlayer().AddCard(card);
            Debug.Log($"{GetCurrentPlayer().PlayerName} drew {card.GetCardInfo()}");
            // Здесь можно добавить логику для выбора карты игроком через UI
        }
        
        CheckEndConditions();
    }

    private bool IsGameOver()
    {
        // Замените на вашу логику для проверки условий окончания игры
        return false; 
    }

    private void DetermineWinner()
    {
        Debug.Log("Determining winner...");

        if (_player1.VictoryPoints > _player2.VictoryPoints)
            Debug.Log($"{_player1.PlayerName} wins!");
        else if (_player2.VictoryPoints > _player1.VictoryPoints)
            Debug.Log($"{_player2.PlayerName} wins!");
        else
            Debug.Log("It's a tie!");
    }

    private void CheckEndConditions()
    {
        // Логика проверки условий окончания игры
    }

    private Player GetCurrentPlayer()
    {
        return _currentPlayerIndex == 0 ? _player1 : _player2;
    }
}