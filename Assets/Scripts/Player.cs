using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string playerName;

    private List<CardData> _hand;

    public int Power { get; private set; }

    public int VictoryPoints { get; private set; }

    private HashSet<string> _uniqueSymbols;
    
    public string PlayerName => playerName;

    public void InitializePlayer(string plName)
    {
        playerName = plName;
        _hand = new List<CardData>();
        Power = 0;
        VictoryPoints = 0;
        _uniqueSymbols = new HashSet<string>();
    }

    public void AddCard(CardData card)
    {
        _hand.Add(card);

        if (card.Bonuses.TryGetValue("Power", out int powerValue))
            Power += powerValue;

        if (card.Bonuses.TryGetValue("VictoryPoints", out int victoryPointsValue))
            VictoryPoints += victoryPointsValue;

        if (card.Bonuses.TryGetValue("UniqueSymbol", out int uniqueSymbolValue))
            _uniqueSymbols.Add(uniqueSymbolValue.ToString());
    }

    public List<CardData> GetHand()
    {
        return _hand;
    }
}
