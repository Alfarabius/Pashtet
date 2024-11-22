using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bonus
{
    public string key; // Название бонуса (например, "Power")
    public int value;  // Значение бонуса (например, 2)

    public Bonus(string key, int value)
    {
        this.key = key;
        this.value = value;
    }
}

[CreateAssetMenu(fileName = "NewCard", menuName = "Card Game/Card")]
public class CardData : ScriptableObject
{
    [SerializeField] private string cardName;
    [SerializeField] private string color; // "Red", "Green", "Blue", "Yellow"
    [SerializeField] private int cost;
    [SerializeField] private List<Bonus> bonuses; // Список бонусов

    public string CardName => cardName;
    public string Color => color;
    public int Cost => cost;

    public Dictionary<string, int> Bonuses
    {
        get
        {
            Dictionary<string, int> bonusDict = new Dictionary<string, int>();
            foreach (var bonus in bonuses)
            {
                bonusDict[bonus.key] = bonus.value;
            }
            return bonusDict;
        }
    }

    public string GetCardInfo()
    {
        return $"{cardName} ({color}) - Cost: {cost}";
    }
}
