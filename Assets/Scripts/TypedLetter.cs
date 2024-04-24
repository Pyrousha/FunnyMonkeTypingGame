using UnityEngine;

public class TypedLetter : MonoBehaviour
{
    public void OnTyped()
    {
        SFXController.Instance.PlayTypeSFX();
    }
}
