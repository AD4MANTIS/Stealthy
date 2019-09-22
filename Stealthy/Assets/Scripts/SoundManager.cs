using nvp.events;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum SoundState
    {
        Chill = 0,
        PlayerSawEnemy,
        EnemySawPlayer,
    }

    public AudioSource musicManager;
    public AudioSource soundManager;

    public AudioClip chill;
    public AudioClip death;
    public AudioClip playerSeesEnemy;
    public AudioClip enemySeesPlayer;
    public AudioClip hideInBoxChill;
    public AudioClip hideInBoxIntense;

    private AudioClip Clip => musicManager.clip;

    private SoundState currentState = SoundState.Chill;

    private List<Enemy> playerSaw = new List<Enemy>();

    private void OnEnable()
    {
        NvpEventController.Events(MyEvent.PlayerSeesEnemy).GameEventHandler += SoundManager_PlayerSeesEnemy;
        NvpEventController.Events(MyEvent.EnemySeesPlayer).GameEventHandler += SoundManager_EnemySeesPlayer;
        NvpEventController.Events(MyEvent.PlayerDies).GameEventHandler += SoundManager_PlayerDies;
        NvpEventController.Events(MyEvent.HideInBox).GameEventHandler += Play_HideInBox;
        NvpEventController.Events(MyEvent.LeaveBox).GameEventHandler += Play_LeaveBox;
        NvpEventController.Events(MyEvent.EnemyLostPlayer).GameEventHandler += Play_LostPlayer;
    }

    private void OnDisable()
    {
        NvpEventController.Events(MyEvent.PlayerSeesEnemy).GameEventHandler -= SoundManager_PlayerSeesEnemy;
        NvpEventController.Events(MyEvent.EnemySeesPlayer).GameEventHandler -= SoundManager_EnemySeesPlayer;
        NvpEventController.Events(MyEvent.PlayerDies).GameEventHandler -= SoundManager_PlayerDies;
        NvpEventController.Events(MyEvent.HideInBox).GameEventHandler -= Play_HideInBox;
        NvpEventController.Events(MyEvent.LeaveBox).GameEventHandler -= Play_LeaveBox; ;
        NvpEventController.Events(MyEvent.EnemyLostPlayer).GameEventHandler -= Play_LostPlayer;
    }

    private void Play_LostPlayer(object sender, EventArgs e)
    {
        int index = playerSaw.IndexOf(sender as Enemy);
        if (index >= 0)
            playerSaw.RemoveAt(index);

        if (playerSaw.Count == 0)
        {
            currentState = SoundState.PlayerSawEnemy;
            PlayDefault();
        }
    }

    private void Play_LeaveBox(object sender, EventArgs e) => PlayDefault();

    private void PlayDefault()
    {
        if (currentState == SoundState.EnemySawPlayer)
            PlayOtherMusik(enemySeesPlayer);
        else
            PlayOtherMusik(chill);
    }

    private void Play_HideInBox(object sender, EventArgs e)
    {
        if (currentState == SoundState.EnemySawPlayer)
            PlayOtherMusik(hideInBoxIntense);
        else
            PlayOtherMusik(hideInBoxChill);
    }

    private void SoundManager_PlayerDies(object sender, EventArgs e)
    {
        PlayOtherMusik(death);
    }

    private void SoundManager_EnemySeesPlayer(object sender, EventArgs e)
    {
        var enemy = sender as Enemy;
        if (!playerSaw.Contains(enemy))
        {
            playerSaw.Add(enemy);

            ChangeState(SoundState.EnemySawPlayer);
            PlayOtherMusik(enemySeesPlayer); ;
        }
    }

    private void ChangeState(SoundState newState)
    {
        if (newState > currentState)
        {
            currentState = newState;
        }
    }

    private void SoundManager_PlayerSeesEnemy(object sender, EventArgs e)
    {
        ChangeState(SoundState.PlayerSawEnemy);
        if (currentState <= SoundState.PlayerSawEnemy)
            soundManager.Play();
    }

    private void PlayOtherMusik(AudioClip clip, float? delay = null)
    {
        if (clip is null)
            return;

        Debug.Log(clip.ToString());

        musicManager.Stop();
        musicManager.clip = clip;
        if (delay == null)
            musicManager.Play();
        else
            musicManager.PlayDelayed((float)delay);
    }
}
