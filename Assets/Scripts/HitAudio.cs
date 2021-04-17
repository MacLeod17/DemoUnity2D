using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAudio : MonoBehaviour
{
    public int audioIndex = 0;
    public bool useAudioManager = true;
    AudioSource audioSource = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (useAudioManager)
        {
            AudioManager.Instance.PlayAudio(audioIndex);
        }
        else
        {
            audioSource?.Play();
        }
    }
}
