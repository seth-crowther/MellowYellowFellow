using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource nextLevel;

    [SerializeField]
    AudioSource death;

    [SerializeField]
    AudioSource backgroundMusic;

    public IEnumerator LevelCompleteSound()
    {
        backgroundMusic.Pause();
        nextLevel.Play();
        yield return new WaitUntil(() => !nextLevel.isPlaying);
        backgroundMusic.UnPause();
    }

    public IEnumerator DeathSound()
    {
        backgroundMusic.Pause();
        death.Play();
        yield return new WaitUntil(() => !death.isPlaying);
        backgroundMusic.UnPause();
    }
}
