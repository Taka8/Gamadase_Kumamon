using UnityEngine;
using DG.Tweening;

public class SoundManager : SingletonMonoBehaviour<SoundManager> {

    [SerializeField] AudioSource seSource;
    [SerializeField] AudioSource bgmSource;

    public void PlaySe(AudioClip clip)
    {
        seSource.PlayOneShot(clip);
    }

    public void PlaySe(AudioClip clip, float volumeScale)
    {
        seSource.PlayOneShot(clip, volumeScale);
    }

    public void PlayBgm(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.volume = 1;
        bgmSource.Play();
    }

    public void StopBgm()
    {
        bgmSource.Stop();
    }

    public void StopBgm(float duration)
    {
        bgmSource.DOFade(0, duration);
    }

}
