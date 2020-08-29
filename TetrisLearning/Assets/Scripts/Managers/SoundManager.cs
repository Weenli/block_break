
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public bool MusicEnabled = true;
    public bool FxEnabled = true;
    [Range(0, 1)]
    public float MusicVolume = 1f;
    [Range(0, 1)]
    public float FxVolume = 1f;

    public AudioClip ClearRowSound;

    public AudioClip MoveSound;

    public AudioClip DropSound;

    public AudioClip ErrorSound;

    public AudioClip GameOverSound;

    public AudioSource MusicSource;
    public AudioClip[] RandomBackgroundMusic;

    public AudioClip[] RandomVoice;
    public AudioClip GameOverVoice;
    public IconToggle[] MusicIconToggle;
    public IconToggle[] FxIconToggle;


    AudioClip GetRandomMusic(AudioClip[] Music)
    {
        AudioClip random = Music[Random.Range(0, Music.Length)];
        return random;
    }
    void Start()
    {
        if (MusicEnabled)
        {
            PlayBackgroundMusic(GetRandomMusic(RandomBackgroundMusic));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (MusicIconToggle[i])
            {
                MusicIconToggle[i].ToggleIcon(MusicEnabled);
            }
            if (FxIconToggle[i])
            {
                FxIconToggle[i].ToggleIcon(FxEnabled);
            }
        }
    }


    public void PlayBackgroundMusic(AudioClip musicAudio)
    {
        if (!MusicEnabled || !musicAudio || !MusicSource)
        {
            return;
        }
        MusicSource.Stop();
        MusicSource.clip = musicAudio;
        MusicSource.volume = MusicVolume;
        MusicSource.loop = true;
        MusicSource.Play();
    }
    void UpdateMusic()
    {
        if (MusicSource.isPlaying != MusicEnabled)
        {
            if (MusicEnabled)
            {
                PlayBackgroundMusic(GetRandomMusic(RandomBackgroundMusic));
            }
            else
            {
                MusicSource.Stop();
            }
        }
    }
    public void ToggleMusic()
    {
        MusicEnabled = !MusicEnabled;
        UpdateMusic();
        for (int i = 0; i < 2; i++)
        {
            if (MusicIconToggle[i])
            {
                MusicIconToggle[i].ToggleIcon(MusicEnabled);
            }
        }

    }
    public void ToggleFx()
    {
        FxEnabled = !FxEnabled;
        for (int i = 0; i < 2; i++)
        {
            if (FxIconToggle[i])
            {
                FxIconToggle[i].ToggleIcon(FxEnabled);
            }
        }
    } 
}   
