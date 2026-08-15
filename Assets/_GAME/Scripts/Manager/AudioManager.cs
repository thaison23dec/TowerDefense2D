using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("SFX")]
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip arrowShootClip;
    [SerializeField] private AudioClip dieClip;
    [SerializeField] private AudioClip explodeClip;
    [SerializeField] private AudioClip startWaveClip;

    [Header("Music")]
    [SerializeField] private List<AudioClip> musicList;

    private Coroutine musicCoroutine;
    private int currentMusicIndex = -1;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void PlayHit()
    {
        sfxSource.PlayOneShot(hitClip);
    }

    public void PlayArrowShoot()
    {
        sfxSource.PlayOneShot(arrowShootClip);
    }

    public void PlayDie()
    {
        sfxSource.PlayOneShot(dieClip);
    }

    public void PlayExplode()
    {
        sfxSource.PlayOneShot(explodeClip);
    }

    public void PlayStartWave()
    {
        sfxSource.PlayOneShot(startWaveClip);
    }

    public void PlayRandomMusic()
    {
        if (musicCoroutine != null)
        {
            StopCoroutine(musicCoroutine);
        }

        musicSource.Stop();

        musicCoroutine = StartCoroutine(MusicCoroutine());
    }

    private IEnumerator MusicCoroutine()
    {
        while (true)
        {
            int rand = GetRandomMusicIndex();

            currentMusicIndex = rand;

            musicSource.clip = musicList[rand];
            musicSource.Play();

            yield return new WaitForSeconds(musicSource.clip.length);
        }
    }

    private int GetRandomMusicIndex()
    {
        if (musicList.Count <= 1)
            return 0;

        int rand;

        do
        {
            rand = Random.Range(0, musicList.Count);
        }
        while (rand == currentMusicIndex);

        return rand;
    }
}
