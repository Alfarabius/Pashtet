using System.Collections.Generic;
using UnityEngine;

public enum CardColor
{
    Blue,
    Red,
    Green,
    Yellow
}

public enum BonusType
{
    Power,
    VictoryPoints,
    UniqueSymbol,
    Income,
    Credits
}

[System.Serializable]
public class Bonus
{
    public BonusType type; // Используем enum для типа бонуса
    public int value;

    public Bonus(BonusType type, int value)
    {
        this.type = type;
        this.value = value;
    }
}

[CreateAssetMenu(fileName = "NewCard", menuName = "Card Game/Card")]
public class CardData : ScriptableObject
{
    [SerializeField] private string cardName;
    [SerializeField] private CardColor color;
    [SerializeField] private int cost;
    [SerializeField] private List<Bonus> bonuses;
    [SerializeField] private Sprite sprite;

    public string CardName => cardName;
    public CardColor Color => color;
    public int Cost => cost;
    public List<Bonus> Bonuses => bonuses;
    public Sprite Sprite => sprite;

    public string GetCardInfo()
    {
        return $"{cardName} ({color}) - Cost: {cost}";
    }
}
