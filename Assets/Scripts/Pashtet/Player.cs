using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string playerName;

    private List<CardData> _hand;

    public int Power { get; private set; }
    public int VictoryPoints { get; private set; }
    public int Income { get; private set; }
    public int Credits { get; private set; }
    
    private HashSet<string> uniqueSymbols;

    public string PlayerName => playerName;

    public void InitializePlayer(string plName)
    {
        playerName = plName;
        _hand = new List<CardData>();
        Power = 0;
        VictoryPoints = 0;
        uniqueSymbols = new HashSet<string>();
    }

    public void AddCard(CardData card)
    {
        _hand.Add(card);

        foreach (var bonus in card.Bonuses)
        {
            switch (bonus.type)
            {
                case BonusType.Power:
                    Power += bonus.value;
                    break;
                case BonusType.VictoryPoints:
                    VictoryPoints += bonus.value;
                    break;
                case BonusType.UniqueSymbol:
                    uniqueSymbols.Add(bonus.value.ToString());
                    break;
                case BonusType.Income:
                    Income += bonus.value;
                    break;
                case BonusType.Credits:
                    Credits += bonus.value;
                    break;
            }
        }
    }

    public List<CardData> GetHand()
    {
        return _hand;
    }
}
