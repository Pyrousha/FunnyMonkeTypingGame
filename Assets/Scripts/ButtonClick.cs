using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        SFXController.Instance.PlayTypeSFX(true);
    }
}
