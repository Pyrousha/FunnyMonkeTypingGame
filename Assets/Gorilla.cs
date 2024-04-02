using TMPro;
using UnityEngine;

public class Gorilla : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI typedLetterText1;
    [SerializeField] private TextMeshProUGUI typedLetterText2;
    [SerializeField] private Animator letterAnimator1;
    [SerializeField] private Animator letterAnimator2;
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

        letterAnimator1.Play(letterPopupAnimationHash);
        letterAnimator2.Play(letterPopupAnimationHash);

        letterAnimator1.speed = 1.0f / MonitorController.Instance.TickTimer;
        letterAnimator2.speed = 1.0f / MonitorController.Instance.TickTimer;
    }

    public void StopAnimation()
    {
        Anim.SetTrigger(STOP_TYPING_TRIGGER);
    }

    public (char, bool, char, bool) LetterTick()
    {
        char newLetter1 = Utils.IntToChar_ASCII(Random.Range(1, 26 + 1));

        typedLetterText1.text = newLetter1.ToString();

        bool isRightLetter1 = (newLetter1 == MonitorController.Instance.CorrectLetter);
        if (isRightLetter1)
            typedLetterText1.color = MonitorController.Instance.CorrectColor;
        else
            typedLetterText1.color = Color.white;


        char newLetter2 = Utils.IntToChar_ASCII(Random.Range(1, 26 + 1));

        typedLetterText2.text = newLetter2.ToString();

        bool isRightLetter2 = (newLetter2 == MonitorController.Instance.CorrectLetter);
        if (isRightLetter2)
            typedLetterText2.color = MonitorController.Instance.CorrectColor;
        else
            typedLetterText2.color = Color.white;


        letterAnimator1.Play(letterPopupAnimationHash, 0, 0);
        letterAnimator2.Play(letterPopupAnimationHash, 0, 0);

        return (newLetter1, isRightLetter1, newLetter2, isRightLetter2);
    }
}
