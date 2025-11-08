using UnityEngine;
using System.Collections;

public class narrationManager : MonoBehaviour
{
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayNarration(AudioClip clip)
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
        source.clip = clip;
        source.Play();
    }
}
