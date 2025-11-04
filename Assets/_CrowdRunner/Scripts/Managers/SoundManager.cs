using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] AudioMixer mixer;
    [SerializeField] private AudioSource doorHitSound;
    [SerializeField] private AudioSource runnerDieSound;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource levelCompleteSound;

    private const string MASTER_VOLUME = "MasterVolume";

    // Start is called before the first frame update
    void OnEnable()
    {
        PlayerCollision.onDoorHit += PlayDoorHitSound;
        Enemy.onEnemyDead += PlayRunnerDieSound;

        GameManager.onGameStateChanged += GameStateChangedCallBack;
    }

    private void OnDisable()
    {
        PlayerCollision.onDoorHit -= PlayDoorHitSound;
        Enemy.onEnemyDead -= PlayRunnerDieSound;

        GameManager.onGameStateChanged -= GameStateChangedCallBack;
    }

    private void GameStateChangedCallBack(GameManager.GameState state)
    {
        if (state == GameManager.GameState.GameOver)
            PlayGameOverSound();
        else if (state == GameManager.GameState.LevelComplete)
            PlayLevelCompleteSound();
    }

    public void PlayDoorHitSound()
    {
        doorHitSound.Play();
    }

    public void PlayRunnerDieSound()
    {
        runnerDieSound.Play();
    }

    public void PlayGameOverSound()
    {
        gameOverSound.Play();
    }

    public void PlayLevelCompleteSound()
    {
        levelCompleteSound.Play();
    }

    public void EnableSounds()
    {
        mixer.SetFloat(MASTER_VOLUME, 0f);
    }

    public void DisableSounds()
    {
        mixer.SetFloat(MASTER_VOLUME, -80f);
    }

}
