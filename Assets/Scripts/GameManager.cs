using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private SoundPlayer _soundPlayer;
    private DiodesManager _diodesManager;

    [SerializeField] private GameObject terminal;
    [SerializeField] private SwitchButton onSwitch;
    [SerializeField] private TMP_Text inputField;
    
    public bool IsEnterPressed { get; private set; } = false;
    public bool isComputerOn = false; 

    private void Awake()
    {
        onSwitch.onSwitchToggle.AddListener(OnToggleOnSwitch);
        
        _diodesManager = FindObjectOfType<DiodesManager>();
        
        _soundPlayer = FindObjectOfType<SoundPlayer>();
        if (_soundPlayer == null)
        {
            Debug.LogError("SoundPlayer not found in the scene.");
            return;
        }

        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(_soundPlayer.PlayClickSound);
        }
    }

    private void Update()
    {
    }

    public List<string> Interpret(string input)
    {
        var text = new List<string>();
        
        text.Add("Test String");

        return text;
    }

    public void OnEnterPressed()
    {
        IsEnterPressed = true;
    }

    public void OnCatPressed()
    {
        if (!isComputerOn)
            return;
        
        inputField.text = "cat";
    }

    public void OnToggleOnSwitch(bool isOn)
    {
        StartCoroutine(ToggleOnButton(isOn));
    }

    private IEnumerator ToggleOnButton(bool isOn)
    {
        yield return new WaitForSeconds(0.2f);
        _soundPlayer.PlayPeepSound();
        _diodesManager.ToggleDiode(0, isOn);
        yield return new WaitForSeconds(0.8f);
        terminal.SetActive(isOn);
        isComputerOn = isOn;
    }

    public void ReleaseEnter()
    {
        IsEnterPressed = false;
    }
}
