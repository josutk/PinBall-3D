using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicBehaviour : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip musicClip;
    public bool paused;
    private SignalHandlerScript signalHandler;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        if (musicSource == null) musicSource = GetComponent<AudioSource>();
        signalHandler = Finder.GetSignalHandler();
        musicSource.clip = musicClip;
        musicSource.volume = 0.5F;
        paused = false;
        elapsedTime = 0F;

        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        PauseMusicBehavior();
    }

    public void PauseMusicBehavior()
    {
        if (signalHandler.buttons.select && !paused)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 3.0)
            {
                musicSource.Pause();
                elapsedTime = 0;
                paused = true;
            }
        }
        else if (signalHandler.buttons.select && paused)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 3.0)
            {
                musicSource.UnPause();
                paused = false;
                elapsedTime = 0;
            }
        }
        else
        {
            elapsedTime = 0;
        }
    }

    public void ChangeMusic(AudioClip nextMusicClip)
    {
        musicSource.Stop();
        musicSource.clip = nextMusicClip;
        musicSource.Play();
    }
}
