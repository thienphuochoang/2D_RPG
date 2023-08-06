using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : PersistentObject<AudioManager>
{
    [SerializeField] private AudioSource[] _sfx;
    [SerializeField] private AudioSource[] _backgroundMusic;

    public bool isPlayingBGM;
    private int _bgmIndex;
    public void PlaySFX(int index)
    {
        if (index < _sfx.Length)
            _sfx[index].Play();
    }

    private IEnumerator DecreaseVolume(AudioSource audio)
    {
        float defaultVolume = audio.volume;
        while (audio.volume > 0.1f)
        {
            audio.volume -= audio.volume * 0.4f;
            yield return new WaitForSeconds(0.6f);
            if (audio.volume <= 0.1f)
            {
                audio.Stop();
                audio.volume = defaultVolume;
                break;
            }
        }
    }

    private void Update()
    {
        if (isPlayingBGM == false)
            StopAllBGM();
        else
        {
            if (!_backgroundMusic[_bgmIndex].isPlaying)
                PlayBGM(_bgmIndex);
        }
    }

    public void PlayRandomBGM()
    {
        _bgmIndex = Random.Range(0, _backgroundMusic.Length);
        PlayBGM(_bgmIndex);
    }

    public void StopSFX(int index) => _sfx[index].Stop();

    public void PlayBGM(int index)
    {
        _bgmIndex = index;
        _backgroundMusic[_bgmIndex].Play();
    }
    
    public void PlayBGMWithTime(int index)
    {
        StartCoroutine(DecreaseVolume(_backgroundMusic[_bgmIndex]));
        PlayBGM(index);
    }

    public void StopAllBGM()
    {
        for (int i = 0; i < _backgroundMusic.Length; i++)
        {
            _backgroundMusic[i].Stop();
        }
    }
}
