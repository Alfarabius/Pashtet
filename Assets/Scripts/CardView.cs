using UnityEngine;
using TMPro;

public class CardView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer picture;
    [SerializeField] private GameObject nameObject;
    [SerializeField] private GameObject effectObject;
    [SerializeField] private GameObject colors;
    private TextMeshProUGUI _nameText;
    private TextMeshProUGUI _effectText;

    private void Awake()
    {
        _nameText = nameObject.GetComponent<TextMeshProUGUI>();
        _effectText = effectObject.GetComponent<TextMeshProUGUI>();
    }

    public void Initialize(CardData card)
    {
        picture.sprite = card.Sprite;
        _nameText.text = card.CardName;
        _effectText.text = GetEffectsString(card);
        var colorTransform = colors.transform.Find(card.Color.ToString());
        colorTransform.gameObject.SetActive(true);
    }

    private string GetEffectsString(CardData card)
    {
        string effects = "";

        effects += $"Cost: {card.Cost}\n";
        foreach (var bonus in card.Bonuses)
        {
            effects += $"{bonus.type}: {bonus.value}\n"; // Используем тип бонуса из enum
        }
        return effects.TrimEnd('\n');
    }
}