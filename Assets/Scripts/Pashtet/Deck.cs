using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<CardData> cards;

    public List<CardData> DrawCards(int count)
    {
        List<CardData> drawnCards = new List<CardData>();

        for (int i = 0; i < count && cards.Count >= count; i++)
        {
            int randomIndex = Random.Range(0, cards.Count - 1);
            drawnCards.Add(cards[randomIndex]);
            cards.RemoveAt(randomIndex);
        }

        return drawnCards;
    }

    public bool IsEmpty()
    {
        return cards.Count == 0;
    }
}
