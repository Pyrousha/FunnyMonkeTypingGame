using UnityEngine;

public class EndscreenMonkey : MonoBehaviour
{
    [SerializeField] private Animator letterAnim;

    public void TypeLetter()
    {
        letterAnim.SetTrigger("Type");
    }
}
