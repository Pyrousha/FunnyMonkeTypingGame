using UnityEngine;

public class CorrectLetterSFX : Singleton<CorrectLetterSFX>
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(float percent)
    {
        audioSource.pitch = 1 + percent;
        audioSource.Play();
    }
}
