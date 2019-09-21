using nvp.events;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum SoundState
    {
        Chill = 0,
        PlayerSawEnemy,
        EnemySawPlayer,
    }

    private AudioSource soundManager;

    public AudioClip chill;
    public AudioClip death;
    public AudioClip playerSeesEnemy;
    public AudioClip enemySeesPlayer;
    public AudioClip hideInBoxChill;
    public AudioClip hideInBoxIntense;

    private AudioClip Clip => soundManager.clip;

    private SoundState currentState = SoundState.Chill;

    private int playerSawnCount = 0;

    private void OnEnable()
    {
        soundManager = GetComponent<AudioSource>();
        PlayOtherMusik(chill);
        NvpEventController.Events(MyEvent.PlayerSeesEnemy).GameEventHandler += SoundManager_PlayerSeesEnemy;
        NvpEventController.Events(MyEvent.EnemySeesPlayer).GameEventHandler += SoundManager_EnemySeesPlayer;
        NvpEventController.Events(MyEvent.PlayerDies).GameEventHandler += SoundManager_PlayerDies;
        NvpEventController.Events(MyEvent.HideInBox).GameEventHandler += Play_HideInBox;
        NvpEventController.Events(MyEvent.LeaveBox).GameEventHandler += Play_LeaveBox; ;
    }

    private void OnDisable()
    {
        NvpEventController.Events(MyEvent.PlayerSeesEnemy).GameEventHandler -= SoundManager_PlayerSeesEnemy;
        NvpEventController.Events(MyEvent.EnemySeesPlayer).GameEventHandler -= SoundManager_EnemySeesPlayer;
        NvpEventController.Events(MyEvent.PlayerDies).GameEventHandler -= SoundManager_PlayerDies;
        NvpEventController.Events(MyEvent.HideInBox).GameEventHandler -= Play_HideInBox;
        NvpEventController.Events(MyEvent.LeaveBox).GameEventHandler -= Play_LeaveBox; ;
    }

    private void Play_LeaveBox(object sender, EventArgs e)
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
        playerSawnCount++;
        ChangeState(SoundState.EnemySawPlayer);
        PlayOtherMusik(enemySeesPlayer);
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
        if(currentState <= SoundState.PlayerSawEnemy)
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
