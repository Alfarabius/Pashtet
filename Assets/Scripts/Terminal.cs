using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Terminal : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject startLine;
    [SerializeField] private GameObject outputLine;
    
    [Header("UI")]
    [SerializeField] private TMP_Text inputField;
    [SerializeField] private GameObject inputLine;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject terminalContent;

    private RectTransform _terminalContentRect;
    
    [SerializeField] private GameManager interpreter;
    
    private void Start()
    {
        _terminalContentRect = terminalContent.GetComponent<RectTransform>();
    }

    private void OnGUI()
    {
        if (!interpreter.isComputerOn)
            return;
        
        if (inputField.text != "" && interpreter.IsEnterPressed)
        {
            interpreter.ReleaseEnter();
            var userInput = inputField.text;
            ClearInputField();
            AddStartLine(userInput);
            var lineCount = AddInterpreterLines(interpreter.Interpret(userInput));
            ScrollToBottom(lineCount);
            inputLine.transform.SetAsLastSibling();
        }
    }

    private void ScrollToBottom(int lineCount)
    {
        if (lineCount > 4)
        {
            scrollRect.velocity = new Vector2(0, 450f);
        }
        else
        {
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }

    private void AddStartLine(string userInputText)
    {
       var terminalContentSize = _terminalContentRect.sizeDelta;
       _terminalContentRect.sizeDelta = new Vector2(terminalContentSize.x, terminalContentSize.y + 35f);
       
       var newStartLine = Instantiate(startLine, terminalContent.transform);
       
       newStartLine.transform.SetSiblingIndex(terminalContent.transform.childCount - 1); // Set as last child
       
       newStartLine.transform.Find("Input Text (TMP)").GetComponent<TextMeshProUGUI>().text = userInputText;
    }

    private int AddInterpreterLines(List<string> outputLines)
    {
        for (int i = 0; i < outputLines.Count; i++)
        {
            var responseLine = Instantiate(outputLine, terminalContent.transform);
            responseLine.transform.SetAsLastSibling();
            var terminalContentSize = _terminalContentRect.sizeDelta;
            _terminalContentRect.sizeDelta = new Vector2(terminalContentSize.x, terminalContentSize.y + 35f);
            responseLine.GetComponentInChildren<TextMeshProUGUI>().text = outputLines[i];
        }
        
        return outputLines.Count;
    }

    private void ClearInputField()
    {
        inputField.text = string.Empty;
    }
}
