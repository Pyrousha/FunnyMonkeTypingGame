using TMPro;
using UnityEngine;

public class Timer : Singleton<Timer>
{
    [SerializeField] private TextMeshProUGUI text;

    private float startTime;

    public void StartTimer()
    {
        startTime = Time.time;
    }

    public void StopTimer()
    {
        float totalTime = Time.time - startTime;
        text.text = "TIME:  " + Utils.TimeToString(totalTime);
    }
}
