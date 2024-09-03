using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance to ensure only one AudioManager exists.
    public static AudioManager instance;

    // Audio sources for different types of game music.
    public AudioSource mainMenuMusic, levelMusic, bossMusic;

    // Array of sound effects (SFX).
    public AudioSource[] sfx;

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // Implement Singleton pattern.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene loads.
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate.
        }
    }

    // Plays music for the main menu, stopping other music.
    public void PlayMainMenuMusic()
    {
        levelMusic.Stop();
        bossMusic.Stop();
        mainMenuMusic.Play();
    }

    // Plays level music if not already playing, stopping other music.
    public void PlayLevelMusic()
    {
        if (!levelMusic.isPlaying)
        {
            bossMusic.Stop();
            mainMenuMusic.Stop();
            levelMusic.Play();
        }
    }

    // Plays boss music, stopping level music.
    public void PlayBossMusic()
    {
        levelMusic.Stop();
        bossMusic.Play();
    }

    // Plays a specific sound effect from the sfx array.
    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop(); // Ensure it's not already playing.
        sfx[sfxToPlay].Play();
    }

    // Plays a sound effect with a random pitch, for variety.
    public void PlaySFXAdjusted(int sfxToAdjust)
    {
        sfx[sfxToAdjust].pitch = Random.Range(.8f, 1.2f); // Adjust pitch.
        PlaySFX(sfxToAdjust); // Utilize existing method to play SFX.
    }
}
