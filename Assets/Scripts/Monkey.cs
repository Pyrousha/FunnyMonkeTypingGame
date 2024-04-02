using TMPro;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI typedLetterText;
    [SerializeField] private Animator letterAnimator;
    public Animator Anim { get; private set; }

    public static string START_TYPING_TRIGGER = "StartTyping";
    public static string STOP_TYPING_TRIGGER = "StopTyping";

    public static int letterPopupAnimationHash = Animator.StringToHash("LetterPopup");

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    public void StartAnimation()
    {
        Anim.SetTrigger(START_TYPING_TRIGGER);
        Anim.speed = 0.5f / MonitorController.Instance.TickTimer;

        //letterAnimator.Play(letterPopupAnimationHash);

        letterAnimator.speed = 1.0f / MonitorController.Instance.TickTimer;
    }

    public void StopAnimation()
    {
        Anim.SetTrigger(STOP_TYPING_TRIGGER);
    }

    public (char, bool) LetterTick()
    {
        char newLetter = Utils.IntToChar_ASCII(Random.Range(1, 26 + 1));

        typedLetterText.text = newLetter.ToString();

        bool isRightLetter = (newLetter == MonitorController.Instance.CorrectLetter);
        if (isRightLetter)
            typedLetterText.color = MonitorController.Instance.CorrectColor;
        else
            typedLetterText.color = Color.white;

        letterAnimator.Play(letterPopupAnimationHash, 0, 0);

        return (newLetter, isRightLetter);
    }
}
