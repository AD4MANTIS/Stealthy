using nvp.events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource soundManager;
    public AudioClip chill;

    private void OnEnable()
    {
        soundManager = GetComponent<AudioSource>();
        soundManager.loop = true;
        soundManager.clip = chill;
        soundManager.Play();
        NvpEventController.Events("PlayerSeesEnemy").GameEventHandler += SoundManager_GameEventHandler;
    }

    private void OnDisable()
    {
        NvpEventController.Events("PlayerSeesEnemy").GameEventHandler -= SoundManager_GameEventHandler;
    }


    private void SoundManager_GameEventHandler(object sender, System.EventArgs e)
    {
        soundManager.Stop();
    }
}
