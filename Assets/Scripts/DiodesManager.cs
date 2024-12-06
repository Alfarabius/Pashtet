using UnityEngine;
using System.Collections.Generic;

public class DiodesManager : MonoBehaviour
{
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;
    private List<SpriteRenderer> _spriteRenderers;

    private void Awake()
    {
        _spriteRenderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void ToggleDiode(int index, bool state)
    {
        if (index < 0 || index >= _spriteRenderers.Count)
        {
            Debug.LogError("Index out of range.");
            return;
        }
        
        var sprite = state ? onSprite : offSprite; 

        _spriteRenderers[index].sprite = sprite;
    }
}