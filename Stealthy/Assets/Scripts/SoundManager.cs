using nvp.events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource soundManager;
    public AudioClip chill;
    public AudioClip death;

    private void OnEnable()
    {
        soundManager = GetComponent<AudioSource>();

        PlayOtherMusik(chill);
        NvpEventController.Events("PlayerSeesEnemy").GameEventHandler += SoundManager_PlayerSeesEnemy;
        NvpEventController.Events("EnemySeesPlayer").GameEventHandler += SoundManager_EnemySeesPlayer;
        NvpEventController.Events("PlayerDies").GameEventHandler += SoundManager_PlayerDies;
    }

    private void SoundManager_PlayerDies(object sender, EventArgs e)
    {
        PlayOtherMusik(death);
    }

    private void OnDisable()
    {
        NvpEventController.Events("PlayerSeesEnemy").GameEventHandler -= SoundManager_PlayerSeesEnemy;
        NvpEventController.Events("EnemySeesPlayer").GameEventHandler -= SoundManager_EnemySeesPlayer;
        NvpEventController.Events("PlayerDies").GameEventHandler -= SoundManager_PlayerDies;
    }

    private void SoundManager_EnemySeesPlayer(object sender, System.EventArgs e)
    {
        soundManager.Stop();
    }


    private void SoundManager_PlayerSeesEnemy(object sender, System.EventArgs e)
    {
        soundManager.Stop();
    }

    private void PlayOtherMusik(AudioClip clip, float? delay = null)
    {
        if (clip is null)
            return;

        soundManager.Stop();
        soundManager.clip = clip;
        if (delay == null)
            soundManager.Play();
        else
            soundManager.PlayDelayed((float)delay);
    }
}
