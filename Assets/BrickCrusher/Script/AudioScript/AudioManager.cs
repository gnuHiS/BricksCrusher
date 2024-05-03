using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    public AudioSource audioSource;

    public AudioClip[] audioClip;

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }
}
