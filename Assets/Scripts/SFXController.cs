using BeauRoutine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : Singleton<SFXController>
{
    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> typeClips;
    [SerializeField] private List<AudioClip> monkeyClips;
    [SerializeField] private List<AudioClip> gorillaClips;
    [SerializeField] private AudioClip yippieSFX;
    [SerializeField] private AudioClip failSFX;
    private int maxSFX = 3;
    private int currSFX = 0;

    private Routine routine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        routine = Routine.Start(this, TypeRoutine());
    }

    public void PlayTypeSFX(bool doAnyways = false)
    {
        if ((currSFX < maxSFX) || doAnyways)
        {
            audioSource.PlayOneShot(typeClips[Random.Range(0, typeClips.Count - 1)]);
            currSFX++;
        }
    }

    public void PlayMonkeySFX()
    {
        audioSource.PlayOneShot(monkeyClips[Random.Range(0, monkeyClips.Count - 1)]);
    }

    public void PlayGorillaSFX()
    {
        audioSource.PlayOneShot(gorillaClips[Random.Range(0, gorillaClips.Count - 1)]);
    }

    public void PlayYippieSFX()
    {
        audioSource.PlayOneShot(yippieSFX);
    }

    public void PlayFailSFX()
    {
        audioSource.PlayOneShot(failSFX);
    }

    private IEnumerator TypeRoutine()
    {
        while (true)
        {
            yield return 0.2f;
            currSFX = 0;
        }
    }
}
