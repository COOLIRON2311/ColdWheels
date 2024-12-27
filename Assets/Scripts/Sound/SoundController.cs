using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MusicType { None, MainMenu, Phonk }

public class SoundController : MonoBehaviour
{
    public static SoundController Instance {  get; private set; }

    [SerializeField]
    private float fadeInSeconds = 5;
    [SerializeField]
    private float fadeOutSeconds = 2;
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource effectsSource;
    public AudioClip menuMusic;
    public List<AudioClip> phonkMusic;
    public List<AudioClip> crashSounds;

    public float MusicVolume {
        get => musicSource.volume;
        set {
            musicSource.volume = value;
            musicVolume = value;
        }
    }
    public float EffectsVolume {
        get => effectsSource.volume;
        set {
            effectsSource.volume = value;
            effectsVolume = value;
        }
    }

    public static float? effectsVolume;
    public static float? musicVolume;

    private MusicType musicType;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        musicType = MusicType.None;
        musicVolume ??= musicSource.volume;
        effectsVolume ??= effectsSource.volume;
        musicSource.volume = musicVolume.Value;
        effectsSource.volume = effectsVolume.Value;
    }

    private void Update()
    {
        if (musicType == MusicType.None)
            return;

        if (musicSource.isPlaying)
            return;

        AudioClip music = musicType switch
        {
            MusicType.MainMenu => menuMusic,
            MusicType.Phonk => Util.RandomListItem(phonkMusic),
            _ => null
        };

        musicSource.clip = music;
        musicSource.Play();
        StartCoroutine(FadeIn());
    }

    public void StopMusic()
    {
        musicType = MusicType.None;
        musicSource.Stop();
    }

    public void FadeOutMusic()
    {
        musicType = MusicType.None;
        StartCoroutine(FadeOut());
    }

    public void PlayMainMenuMusic()
    {
        musicType = MusicType.MainMenu;
        musicSource.Stop();
    }

    public void PlayPhonkMusic()
    {
        musicType = MusicType.Phonk;
        musicSource.Stop();
    }

    public void PlayCrashSound()
    {
        effectsSource.PlayOneShot(Util.RandomListItem(crashSounds));
    }

    IEnumerator FadeIn()
    {
        musicSource.volume = 0;
        float timeElapsed = 0;

        while (musicSource.volume < musicVolume)
        {
            musicSource.volume = Mathf.Lerp(0, musicVolume.Value, timeElapsed / fadeInSeconds);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float timeElapsed = 0;
        while (musicSource.volume > 0)
        {
            musicSource.volume = Mathf.Lerp(musicVolume.Value, 0, timeElapsed / fadeOutSeconds);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        musicSource.Stop();
    }
}
