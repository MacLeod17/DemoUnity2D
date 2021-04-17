using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;
    static AudioManager instance = null;

    private void Awake()
    {
        instance = this;
    }

    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void PlayAudio(int index)
    {
        audioSources[index].Play();
    }
}
