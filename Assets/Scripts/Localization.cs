using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Localization : MonoBehaviour
{
    public enum Languages {
        English,
        Russian,
    }

    private static Languages _gameLanguage = Languages.English;
    
    [field: SerializeField] public Languages Language { get; private set; }

    public UnityEvent<Languages> languageChanged;
    
    private void Awake()
    {
        Language = _gameLanguage;
    }
    
    public Languages GetLanguage() => _gameLanguage;
    
    public void ToggleLanguage()
    {
        _gameLanguage = _gameLanguage switch
        {
            Languages.English => Languages.Russian,
            Languages.Russian => Languages.English,
            _ => _gameLanguage
        };
        
        Language = _gameLanguage;
        languageChanged?.Invoke(Language);
    }
}
