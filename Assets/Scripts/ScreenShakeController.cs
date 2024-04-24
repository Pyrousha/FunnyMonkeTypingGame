using BeauRoutine;
using System.Collections;
using UnityEngine;

public class ScreenShakeController : Singleton<ScreenShakeController>
{
    private RectTransform rect;

    private Vector3 maxOffset = new Vector3(90, 0, 50);
    [SerializeField] private float maxScale = 1.1f;

    private Routine shakeRoutine;

    [SerializeField] private float shakeDuration = 0.05f;

    private Vector3 startPos;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        startPos = rect.anchoredPosition;
    }

    public void DoScreenShake(float _powerPercent)
    {
        float xRand = Random.Range(0, 2);
        if (xRand == 0)
            xRand = -1;

        float yRand = Random.Range(0, 2);
        if (yRand == 0)
            yRand = -1;

        Vector3 newOffset = new Vector3(maxOffset.x * xRand, 0, maxOffset.y * yRand) * _powerPercent;
        shakeRoutine.Stop();
        shakeRoutine = Routine.Start(this, ShakeRoutine(_powerPercent, newOffset));
    }

    private IEnumerator ShakeRoutine(float _percent, Vector3 _offset)
    {
        float maxScaleToUse = Mathf.Lerp(1, maxScale, _percent);

        yield return Tween.Float(0, 1, (val) =>
        {
            rect.anchoredPosition = startPos + _offset * val;
            transform.localScale = Vector3.one * Mathf.Lerp(1, maxScaleToUse, val);
        }, shakeDuration / 2f).Ease(Curve.CubeOut);

        yield return Tween.Float(1, 0, (val) =>
        {
            rect.anchoredPosition = startPos + _offset * val;
            transform.localScale = Vector3.one * Mathf.Lerp(1, maxScaleToUse, val);
        }, shakeDuration / 2f).Ease(Curve.CubeOut);
    }
}
