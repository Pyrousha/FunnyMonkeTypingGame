using UnityEngine;

public class MonkeySlot : MonoBehaviour
{
    [SerializeField] private Animator shadowAnim;

    public void ShowShadow()
    {
        shadowAnim.SetTrigger("Show");
    }
}
