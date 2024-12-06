using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwitchButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite spriteOn;
    [SerializeField] private Sprite spriteOff;
    [SerializeField] private Image buttonImage;
    
    private bool _isOn = false;

    public UnityEvent<bool> onSwitchToggle = new UnityEvent<bool>();

    private void Start()
    {
        UpdateSprite();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }

    private void Toggle()
    {
        _isOn = !_isOn;
        UpdateSprite();
        onSwitchToggle.Invoke(_isOn);
    }

    private void UpdateSprite()
    {
        buttonImage.sprite = _isOn ? spriteOn : spriteOff;
    }
}
