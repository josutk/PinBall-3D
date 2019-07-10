using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicBehaviour : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip[] soundtrack;
    public int currentMusic;
    private SignalHandlerScript signalHandler;
    private float elapsedTime;
    private bool musicPassed;

    // Start is called before the first frame update
    void Start()
    {
        if (musicSource == null) musicSource = GetComponent<AudioSource>();
        signalHandler = Finder.GetSignalHandler();
        musicSource.clip = soundtrack[currentMusic];
        musicSource.volume = 0.5F;
        musicPassed = false;
        elapsedTime = 0F;

        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        CountTimeSelectPressed();
        StopMusic();
        PlayNextMusic();
    }

    void CountTimeSelectPressed()
    {
        if (signalHandler.buttons.select)
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {
            musicPassed = false;
            elapsedTime = 0;
        }
    }

    void StopMusic()
    {
        if (signalHandler.buttons.select && musicSource.isPlaying && elapsedTime >= 6.0)
        {
            musicSource.Stop();
        }
    }

    void PlayNextMusic()
    {
        if (signalHandler.buttons.select && !musicPassed && elapsedTime >= 3.0)
        {
            ChangeMusic(soundtrack[GetValidNextMusic()]);
            musicPassed = true;
        }
    }

    void ChangeMusic(AudioClip nextMusicClip)
    {
        musicSource.Stop();
        musicSource.clip = nextMusicClip;
        musicSource.Play();
    }

    int GetValidNextMusic()
    {
        int nextMusic = currentMusic + 1;
        if (nextMusic > soundtrack.Length)
        {
            nextMusic = 0;
            currentMusic = nextMusic;
            return nextMusic;
        } else
        {
            currentMusic = nextMusic;
            return nextMusic;
        }
    }
}
