using BeauRoutine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonitorController : Singleton<MonitorController>
{
    [SerializeField] private TextMeshProUGUI bgText;
    [SerializeField] private TextMeshProUGUI typingText;
    [SerializeField] private TMP_InputField inputfield;

    [SerializeField] private TextMeshProUGUI keysNumText;
    [SerializeField] private RectTransform keysLayoutParent;

    [Space(10)]
    [SerializeField] private GameObject inputOverlay;
    [SerializeField] private GameObject winOverlay;

    private int keys = 0;

    [field: Space(10)]
    [field: SerializeField] public Color CorrectColor { get; private set; }
    [field: SerializeField] public Color WrongColor { get; private set; }

    private List<char> correctWord;
    private int currIndex = 0;
    public char CorrectLetter { get; private set; }

    private bool resetOnNextType = false;

    private bool gameWon = false;

    Routine tickRoutine;

    private RectTransform rect;

    [field: SerializeField] public float TickTimer { get; private set; } = 0.25f;
    [SerializeField] private RectTransform monkeyParent;

    [Space(10)]
    private List<Monkey> monkeys = new List<Monkey>();
    [SerializeField] private Button shopButton_monkey;
    [SerializeField] private TextMeshProUGUI shop_monkey_costText;
    private int cost_monkey = 10;
    private int cost_monkey_increase = 10;
    [SerializeField] private GameObject prefab_monkey;
    private List<Monkey> monkeysToStartTyping = new List<Monkey>();

    [field: Space(10)]
    private List<Gorilla> gorillas = new List<Gorilla>();
    [SerializeField] private Button shopButton_gorilla;
    [SerializeField] private TextMeshProUGUI shop_gorilla_costText;
    private int cost_gorilla = 30;
    private int cost_gorilla_increase = 35;
    [SerializeField] private GameObject prefab_gorilla;
    private List<Gorilla> gorillasToStartTyping = new List<Gorilla>();

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        monkeys = FindObjectsByType<Monkey>(FindObjectsSortMode.InstanceID).ToList();
        gorillas = FindObjectsByType<Gorilla>(FindObjectsSortMode.InstanceID).ToList();

        shop_monkey_costText.text = cost_monkey.ToString() + " keys";
        shopButton_monkey.onClick.AddListener(SpawnNewMonkey);

        shop_gorilla_costText.text = cost_gorilla.ToString() + " keys";
        shopButton_gorilla.onClick.AddListener(SpawnNewGorilla);
    }

    public void SpawnNewMonkey()
    {
        if (keys < cost_monkey)
            return;

        Monkey monke = Instantiate(prefab_monkey, monkeyParent).GetComponent<Monkey>();
        monkeysToStartTyping.Add(monke);
        monkeys.Add(monke);

        OnNumKeysChanged(keys - cost_monkey);

        cost_monkey += cost_monkey_increase;
        shop_monkey_costText.text = cost_monkey.ToString() + " keys";
    }

    public void SpawnNewGorilla()
    {
        if (keys < cost_gorilla)
            return;

        Gorilla gorilla = Instantiate(prefab_gorilla, monkeyParent).GetComponent<Gorilla>();
        gorillasToStartTyping.Add(gorilla);
        gorillas.Add(gorilla);

        OnNumKeysChanged(keys - cost_gorilla);

        cost_gorilla += cost_gorilla_increase;
        shop_gorilla_costText.text = cost_gorilla.ToString() + " keys";
    }

    public void OnInputChanged(string _input)
    {
        if (_input.Length == 0)
            return;

        char lastLetter = _input[^1];
        try
        {
            char upperLetter = lastLetter.ToString().ToUpper()[0];

            int val = Utils.CharToInt_ASCII(upperLetter);
            if (val >= 1 && val <= 26)
            {
                //Valid letter
                inputfield.text = _input.Substring(0, _input.Length - 1) + upperLetter;
            }
            else
                throw new System.Exception();
        }
        catch
        {
            //Remove last letter
            inputfield.text = _input.Substring(0, _input.Length - 1);
        }
    }

    public void OnWordSubmitted()
    {
        if (inputfield.text.Length > 0)
        {
            inputOverlay.SetActive(false);

            SetTargetWord(inputfield.text);
            tickRoutine = Routine.Start(this, TickRoutine());
        }
    }

    private void SetTargetWord(string _word)
    {
        _word = _word.ToUpper();

        bgText.text = _word;

        correctWord = new List<char>();
        for (int i = 0; i < _word.Length; i++)
        {
            char ch = _word[i];
            correctWord.Add(ch);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);

        ResetWord();
    }

    public void DoTick()
    {
        int numCorrectLetters = 0;

        //Monkeys
        List<char> typedLetters = new List<char>();
        foreach (Monkey monke in monkeys)
        {
            (char typedLetter, bool isCorrect) = monke.LetterTick();
            typedLetters.Add(typedLetter);

            if (isCorrect)
                numCorrectLetters++;
        }
        foreach (Monkey monke in monkeysToStartTyping)
        {
            monke.StartAnimation();
        }
        monkeysToStartTyping.Clear();

        //Gorillas
        foreach (Gorilla gorilla in gorillas)
        {
            (char letter1, bool isCorrect1, char letter2, bool isCorrect2) = gorilla.LetterTick();
            typedLetters.Add(letter1);
            typedLetters.Add(letter2);

            if (isCorrect1)
                numCorrectLetters++;
            if (isCorrect2)
                numCorrectLetters++;
        }
        foreach (Gorilla gorilla in gorillasToStartTyping)
        {
            gorilla.StartAnimation();
        }
        gorillasToStartTyping.Clear();


        TryTypeLetter(typedLetters, numCorrectLetters);
    }

    public void TryTypeLetter(List<char> _submittedLetters, int _numCorrectLetters)
    {
        if (resetOnNextType)
        {
            ResetWord();
            resetOnNextType = false;
        }

        OnNumKeysChanged(keys + _submittedLetters.Count + _numCorrectLetters);

        if (_numCorrectLetters > 0)
        {
            typingText.text += "<color=#" + Utils.ColorToHex(CorrectColor) + $">{CorrectLetter}</color>";

            currIndex++;

            if (currIndex < correctWord.Count)
            {
                CorrectLetter = correctWord[currIndex];
            }
            else
            {
                //You win!!
                gameWon = true;
                winOverlay.SetActive(true);

                foreach (Monkey monke in monkeys)
                {
                    monke.StopAnimation();
                }
                foreach (Gorilla gorilla in gorillas)
                {
                    gorilla.StopAnimation();
                }
            }
        }
        else
        {
            typingText.text += "<color=#" + Utils.ColorToHex(WrongColor) + $">{_submittedLetters[0]}</color>";

            resetOnNextType = true;
        }
    }

    void ResetWord()
    {
        currIndex = 0;
        CorrectLetter = correctWord[currIndex];
        typingText.text = "";
    }

    private IEnumerator TickRoutine()
    {
        foreach (Monkey monke in monkeys)
        {
            monke.StartAnimation();
        }

        while (gameWon == false)
        {
            yield return TickTimer;

            DoTick();
        }
    }

    private void OnNumKeysChanged(int _newKeys)
    {
        keys = _newKeys;
        keysNumText.text = keys.ToString();
        LayoutRebuilder.ForceRebuildLayoutImmediate(keysLayoutParent);

        shopButton_monkey.interactable = (keys >= cost_monkey);
        shopButton_gorilla.interactable = (keys >= cost_gorilla);
    }
}
