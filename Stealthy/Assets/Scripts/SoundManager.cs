﻿using nvp.events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource soundManager;
    public AudioClip chill;
    public AudioClip death;
    public AudioClip playerSeesEnemy;
    public AudioClip enemySeesPlayer;
    public AudioClip hideInBoxChill;
    public AudioClip hideInBoxIntense;

    private void OnEnable()
    {
        soundManager = GetComponent<AudioSource>();

        PlayOtherMusik(chill);
        NvpEventController.Events(MyEvent.PlayerSeesEnemy).GameEventHandler += SoundManager_PlayerSeesEnemy;
        NvpEventController.Events(MyEvent.EnemySeesPlayer).GameEventHandler += SoundManager_EnemySeesPlayer;
        NvpEventController.Events(MyEvent.PlayerDies).GameEventHandler += SoundManager_PlayerDies;
        NvpEventController.Events(MyEvent.HideInBox).GameEventHandler += Play_HideInBox;
    }

    private void Play_HideInBox(object sender, EventArgs e)
    {
        if (soundManager.clip == chill)
            PlayOtherMusik(hideInBoxChill);
        else
            PlayOtherMusik(hideInBoxIntense);
    }

    private void SoundManager_PlayerDies(object sender, EventArgs e)
    {
        PlayOtherMusik(death);
    }

    private void OnDisable()
    {
        NvpEventController.Events(MyEvent.PlayerSeesEnemy).GameEventHandler -= SoundManager_PlayerSeesEnemy;
        NvpEventController.Events(MyEvent.EnemySeesPlayer).GameEventHandler -= SoundManager_EnemySeesPlayer;
        NvpEventController.Events(MyEvent.PlayerDies).GameEventHandler -= SoundManager_PlayerDies;
        NvpEventController.Events(MyEvent.HideInBox).GameEventHandler -= Play_HideInBox;
    }

    private void SoundManager_EnemySeesPlayer(object sender, EventArgs e) => soundManager.Stop();

    private void SoundManager_PlayerSeesEnemy(object sender, EventArgs e)
    {
        if(soundManager.clip != enemySeesPlayer)
            PlayOtherMusik(playerSeesEnemy);
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
