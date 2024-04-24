using UnityEngine;

public class Music : Singleton<Music>
{
    public void PlayMusic()
    {
        GetComponent<AudioSource>().Play();
    }
}
