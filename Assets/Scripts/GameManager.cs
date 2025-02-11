using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    private Localization _localization;
    
    [SerializeField] Volume postProcessVolume;
    private Vignette _vignette;
    
    //[SerializeField] private Sprite cursorSprite;
    //private GameObject _cursorObject;
    
    private SoundPlayer _soundPlayer;
    private DiodesManager _diodesManager;
    
    [Header("Terminal")]
    [SerializeField] private GameObject terminal;
    [SerializeField] private TMP_Text inputField;
    
    [Header("Game")]
    [SerializeField] private SwitchButton onSwitch;
    public bool IsEnterPressed { get; private set; } = false;
    public bool isComputerOn = false; 
    
    private bool _isShift = false;
    private int _currentStage = 0;
    
    [SerializeField] private SpriteRenderer spriteRenderer;

    public UnityEvent onButtonsFind;
    public UnityEvent gameEnded;
    
    [ContextMenu("Quit")]
    private void FadeOutAndQuit()
    {
        if (!spriteRenderer) 
            return;

        spriteRenderer.DOFade(1f, 1f).OnComplete(QuitApplication);
    }

    private void QuitApplication()
    {
        Application.Quit();

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
    
    private void Awake()
    {
        postProcessVolume.profile.TryGet(out _vignette);
        _localization = GetComponent<Localization>();
        // Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
        // Cursor.visible = false;
        // CreateCursorObject();
        
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
        //DrawCursor();
    }

    public List<string> Interpret(string input)
    {
        var output = new List<string>();
        
        if (input == "cat hint.txt")
            output.AddRange(HintsText());

        if (_currentStage == 0)
        {
            if (_localization.Language == Localization.Languages.English)
            {
                output.Add("If you need help, please enter: cat hint.txt");
                output.Add("Let's start with something simple, type: capirotada");
            }
            else if (_localization.Language == Localization.Languages.Russian)
            {
                output.Add("Если нужна помощь, введи: cat hint.txt");
                output.Add("Давай начнем с чего-нибудь простого, введи: capirotada");
            }

            _currentStage = 1;
            return output;
        }

        if (input == "cat")
        {
            if (_localization.Language == Localization.Languages.English)
            {
                output.Add("Meow");
            }
            else if (_localization.Language == Localization.Languages.Russian)
            {
                output.Add("Мяу");
                
            }
        }

        if (input == "cat daruda_sandstorm.mp3")
        {
            if (_localization.Language == Localization.Languages.English)
            {
                output.Add("cat can read only text files");
            }
            else if (_localization.Language == Localization.Languages.Russian)
            {
                output.Add("cat может читать только текстовые файлы");
            }
        }

        if (input is "files list" or "files list --all")
        {
            if (input == "files list --all")
                output.Add(".top_secret.txt");
            if (_currentStage == 5)
            {
                output.Add("golden_plus.txt");
            }
            if (_currentStage >= 3)
            {
                output.Add("ascii.txt");
                output.Add("buttons.txt");
                output.Add("daruda_sanstorm.mp3");
                output.Add("golden_plus.zip");
                output.Add("hint.txt");
            }
            else
            {
                output.Add("files are available only for turbo users))");
            }
        }

        if (input == "cat .top_secret.txt" && _currentStage >= 3)
        {
            if (_localization.Language == Localization.Languages.English)
            {
                output.Add("Do you remember? This is your first code in QBASIC");
                output.Add("I was very proud of you)");
            }
            else if (_localization.Language == Localization.Languages.Russian)
            {
                output.Add("Помнишь? Это твой первый код на QBASIC");
                output.Add("Я очень гордился тобой)");
            }
            output.Add("");
            output.Add("INPUT \"Enter your name: \", name$");
            output.Add("PRINT \"Hello, my name is \"; name$; \"! This is my first QBASIC program.\"");
        }

        if (input == "cat golden_plus.txt" && _currentStage < 3)
        {
            output.Add("golden_plus.txt is available only for turbo users \u00af\\_( \u0361\u00b0 \u035cʖ \u0361\u00b0)_/\u00af");
        }
        
        if (input == "cat ascii.txt")
        {
            if (_currentStage > 1)
                output.AddRange(AsciiText());
            else
                output.Add("ascii.txt is not available");
        }

        if (_currentStage == 1)
        {
            if (input == "capirotada")
            {
                _currentStage = 2;
                _soundPlayer.PlayPeepSound();
                _diodesManager.ToggleXDiodes(_currentStage, isComputerOn);

                DOVirtual.Float(_vignette.intensity.value, _vignette.intensity.value + 0.05f,
                    1f, x => _vignette.intensity.value = x).SetEase(Ease.Linear);
                
                output.Add(@"    ___       ___       ___       ___       ___   ");
                output.Add(@"   /\  \     /\  \     /\  \     /\  \     /\  \  ");
                output.Add(@"  /::\  \   /::\  \   /::\  \   /::\  \    \:\  \ ");
                output.Add(@" /:/\:\__\ /::\:\__\ /::\:\__\ /::\:\__\   /::\__\");
                output.Add(@" \:\:\/__/ \;:::/  / \:\:\/  / \/\::/  /  /:/\/__/");
                output.Add(@"  \::/  /   |:\/__/   \:\/  /    /:/  /   \/__/   ");
                output.Add(@"   \/__/     \|__|     \/__/     \/__/            ");
                output.Add("\n");
                output.Add("ascii.txt is now available");
                output.Add("Enter turbo (\u2310\u25a0_\u25a0) user password:");
                
                return output;
            }
            output.Add("Sorry, this is not a capirotada. Just type: capirotada");
        }

        if (_currentStage == 2)
        {
            if (input == "{)]")
            {
                _currentStage = 3;
                _soundPlayer.PlayPeepSound();
                _diodesManager.ToggleXDiodes(_currentStage, isComputerOn);
                DOVirtual.Float(_vignette.intensity.value, _vignette.intensity.value + 0.05f,
                    1f, x => _vignette.intensity.value = x).SetEase(Ease.Linear);
                output.Add(@"  ___ ___  ___  _   _ ___     ___  ___  __   _____  _   _ ");
                output.Add(@" | _ \ _ \/ _ \| | | |   \   / _ \| __| \ \ / / _ \| | | |");
                output.Add(@" |  _/   / (_) | |_| | |) | | (_) | _|   \ V / (_) | |_| |");
                output.Add(@" |_| |_|_\\___/ \___/|___/   \___/|_|     |_| \___/ \___/ ");
                output.Add("\n");
                output.Add("Access to files is open");
            }
            else
            {
                output.Add("Wrong password (OᴥOʋ)");
                output.Add("If you need help, please enter: cat hint.txt");
                output.Add("Enter turbo (\u2310\u25a0_\u25a0) user password:");
            }
        }

        if (_currentStage >= 3)
        {
            if (input == "cat buttons.txt")
            {
                if (_currentStage == 3)
                {
                    _currentStage = 4;
                    _soundPlayer.PlayPeepSound();
                    _diodesManager.ToggleXDiodes(_currentStage, isComputerOn);
                    DOVirtual.Float(_vignette.intensity.value, _vignette.intensity.value + 0.05f,
                        1f, x => _vignette.intensity.value = x).SetEase(Ease.Linear);
                    StartCoroutine(Buttons());
                }
                
                if (_localization.Language == Localization.Languages.English)
                {
                    output.Add("I ordered the missing buttons from BabaExpress.");
                    output.Add("If I don't manage to install them, they will be in the box of this computer.");
                }
                else if (_localization.Language == Localization.Languages.Russian)
                {
                    output.Add("Недостающие кнопки я заказал на BabaExpress.");
                    output.Add("Если не успею их установить, они будут лежать в коробке от этого компьютера.");
                }

            }
        }

        if (_currentStage >= 4)
        {
            if (input == "unzip golden_plus.zip")
            {
                if (_currentStage == 4)
                {
                    _currentStage = 5;
                    _soundPlayer.PlayPeepSound();
                    _diodesManager.ToggleXDiodes(_currentStage, isComputerOn);
                    DOVirtual.Float(_vignette.intensity.value, _vignette.intensity.value + 0.05f,
                        1f, x => _vignette.intensity.value = x).SetEase(Ease.Linear);
                }
                output.Add("Progress: [#####################################] 100 %");
            }
        }

        if (_currentStage >= 5)
        {
            if (input != "cat golden_plus.txt") 
                return output;

            if (_currentStage == 5)
            {
                _currentStage = 6;
                _soundPlayer.PlayPeepSound();
                _diodesManager.ToggleXDiodes(_currentStage, isComputerOn);
                DOVirtual.Float(_vignette.intensity.value, _vignette.intensity.value + 0.05f,
                    1f, x => _vignette.intensity.value = x).SetEase(Ease.Linear);
                StartCoroutine(FinalPaper());
            }

            if (_localization.Language == Localization.Languages.English)
            {
                output.Add("Hi, if you are reading these, it means it's time to say goodbye.");
                output.Add("And as a final surprise for you and your sister, about 15 years ago");
                output.Add("I bought some of those now-famous Golden Plus coins.");
                output.Add("Here is my wallet: 90df#sgFFkKeM=dSL+%icxGH*GV#.");
                output.Add("The keywords are in my notebook in the closet.");
                output.Add("There isn't much in the wallet, but I think it should be enough ");
                output.Add("for a cozy home for you and your sister. With love, Dad =)");
            }
            else if (_localization.Language == Localization.Languages.Russian)
            {
                output.Add("Привет, если ты это читаешь, значит, пришло время прощаться.");
                output.Add("И в качестве последнего сюрприза для тебя и твоей сестры, около 15 лет назад");
                output.Add("Я купил несколько ныне знаменитых монет Golden Plus.");
                output.Add("Вот мой кошелек: 90df#sgFFkKeM=dSL+%icxGH*GV#.");
                output.Add("Ключевые слова находятся в моем блокноте в шкафу.");
                output.Add("В кошельке не так много денег, но я думаю, что должно хватить");
                output.Add("на уютный дом для тебя и твоей сестры. С любовью, Папа =)");
            }
        }
        
        return output;
    }

    public void OnEnterPressed()
    {
        IsEnterPressed = true;
    }

    public void OnCatPressed()
    {
        if (!isComputerOn)
            return;
        
        var output = !_isShift ? "cat" : "(= Ф_Ф=)";
        inputField.text += output;
    }

    public void OnBackSpacePressed()
    {
        if (!isComputerOn || inputField.text == "")
            return;
        
        var text = inputField.text;
        inputField.text = text[..^1]; // text.Substring(0, text.Length - 1);
    }

    public void OnCookiePressed()
    {
        if (!isComputerOn)
            return;
        
        var output = !_isShift ? "O8\u2248" : ")8\u2248";
        inputField.text += output;
    }

    public void AddStringToInput(string input)
    {
        if (!isComputerOn)
            return;
        
        inputField.text += input;
    }

    public void OnToggleOnSwitch(bool isOn)
    {
        StartCoroutine(ToggleOnButton(isOn));
    }

    public void OnShiftToggled()
    {
        // if (!isComputerOn)
        //     return;
        _isShift = !_isShift;
    }

    public void OnLBracketPressed()
    {
        if (!isComputerOn)
            return;
        
        var output = !_isShift ? "[" : "{";
        inputField.text += output;
    }
    
    public void OnRBracketPressed()
    {
        if (!isComputerOn)
            return;
        
        var output = !_isShift ? "]" : "}";
        inputField.text += output;
    }

    public void OnPiPressed()
    {
        if (!isComputerOn)
            return;
        
        var output = !_isShift ? "3.14" : "pi";
        inputField.text += output;
    }

    private IEnumerator FinalPaper()
    {
        yield return new WaitForSeconds(10f);
        gameEnded.Invoke();
    }

    private IEnumerator ToggleOnButton(bool isOn)
    {
        yield return new WaitForSeconds(0.2f);
        _soundPlayer.PlayPeepSound();
        _diodesManager.ToggleXDiodes(_currentStage ,isOn);
        yield return new WaitForSeconds(0.8f);
        terminal.SetActive(isOn);
        isComputerOn = isOn;
        
        if (_currentStage == 6)
        {
            FadeOutAndQuit();
        }
    }

    private IEnumerator Buttons()
    {
        yield return new WaitForSeconds(2f);
        onButtonsFind.Invoke();
    }

    public void ReleaseEnter()
    {
        IsEnterPressed = false;
    }

    private List<string> AsciiText()
    {
        var output = new List<string>();

        output.Add("! 33 \" 34 # 35 $ 36 % 37 & 38 ' 39 ( 40 ) 41 * 42");
        output.Add("+ 43 , 44 - 45 . 46 / 47 0 48 1 49 2 50 3 51 4 52");
        output.Add("5 53 6 54 7 55 8 56 9 57");
        output.Add(": 58 ; 59 < 60 = 61 > 62 ? 63 @ 64 A 65 B 66 C 67");
        output.Add("D 68 E 69 F 70 G 71 H 72 I 73 J 74 K 75 L 76 M 77");
        output.Add("N 78 O 79 P 80 Q 81 R 82 S 83 T 84 U 85 V 86 W 87");
        output.Add("X 88 Y 89 Z 90 [ 91 \\ 92 ] 93 ^ 94 _ 95 ` 96 a 97");
        output.Add("b 98 c 99 d 100 e 101 f 102 g 103 h 104 i 105 j 106");
        output.Add("k 107 l 108 m 109 n 110 o 111 p 112 q 113 r 114");
        output.Add("s 115 t 116 u 117 v 118 w 119 x 120 y 121 z 122");
        output.Add("{ 123 | 124 } 125 ~ 126 DEL 127");
        
        return output;
    }

    private List<string> HintsText()
    {
        var output = new List<string>();

        if (_currentStage == 0)
        {
            if (_localization.Language == Localization.Languages.English)
                output.Add("hint.txt is not available");
            else if (_localization.Language == Localization.Languages.Russian)
                output.Add("hint.txt недоступен");
        }

        if (_currentStage > 0)
        {
            if (_localization.Language == Localization.Languages.English)
                output.Add("Make the right word from others, erase unnecessary letters with Back Space");
            else if (_localization.Language == Localization.Languages.Russian)
                output.Add("Составь нужное слово из других, сотри ненужные буквы с помощью Back Space");
        }

        if (_currentStage > 1)
        {
            if (_localization.Language == Localization.Languages.English)
            {
                output.Add("Password is 3 ASCII symbols, use cat ascii.txt");
                output.Add("You need bunny for )");
            }
            else if (_localization.Language == Localization.Languages.Russian)
            {
                output.Add("Пароль состоит из 3 символов ASCII, используйте cat ascii.txt");
                output.Add("Кролик поможет с )");
            }
        }

        if (_currentStage > 2)
        {
            if (_localization.Language == Localization.Languages.English)
                output.Add("buttons.txt now available");
            else if (_localization.Language == Localization.Languages.Russian)
                output.Add("buttons.txt теперь доступен");
        }
        
        if (_currentStage > 3)
        {
            output.Add("unzip golden_plus.zip");
            if (_localization.Language == Localization.Languages.English)
                output.Add("top_secret.txt is hidden file");
            else if (_localization.Language == Localization.Languages.Russian)
                output.Add("top_secret.txt скрытый файл");
        }
        
        if (_currentStage > 4)
        {
            if (_localization.Language == Localization.Languages.English)
                output.Add("golden_plus.txt now available");
            else if (_localization.Language == Localization.Languages.Russian)
                output.Add("golden_plus.txt теперь доступен");
        }

        if (_currentStage > 5)
        {
            if (_localization.Language == Localization.Languages.English)
                output.Add("You can turn off PDA now. Thank you for playing!");
            else if (_localization.Language == Localization.Languages.Russian)
                output.Add("Можешь выключить КПК. Спасибо за игру!");
        }
        
        return output;
    }
    
    public void ChangeMasterVolume(float value)
    {
        audioMixer.GetFloat("Master", out float volume);
        volume += value;

        switch (volume)
        {
            case > 50:
            case < -70:
                return;
            default:
                audioMixer.SetFloat("Master", volume);
                break;
        }
    }
    
    // private void CreateCursorObject()
    // {
    //     _cursorObject = new GameObject("Cursor")
    //     {
    //         transform =
    //         {
    //             localPosition = Vector3.zero
    //         }
    //     };
    //     
    //     _cursorObject.transform.SetParent(transform);
    //     var sR = _cursorObject.AddComponent<SpriteRenderer>();
    //     sR.sprite = cursorSprite;
    //     sR.sortingOrder = 102;
    // }
    //
    // private void DrawCursor()
    // {
    //     Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //
    //     _cursorObject.transform.position = cursorPos;
    // }
}
