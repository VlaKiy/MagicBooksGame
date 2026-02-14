using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [HideInInspector] public AudioSource src;
    public AudioClip[] clips;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
    }

    public void PlaySound(int i)
    {
        src.PlayOneShot(clips[i]);
    }
}
