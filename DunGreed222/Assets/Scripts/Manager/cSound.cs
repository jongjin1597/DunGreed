using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//사운드 클래스
public class cSound : MonoBehaviour
{
    private AudioSource _audioSource = null;

    //볼륨 프로퍼티
    public float Volume
    {
        get
        {
            return _audioSource.volume;
        }
        set
        {
            _audioSource.volume = value;
        }
    }
    //음소거 프로퍼티.
    public bool Mute
    {
        get
        {
            return _audioSource.mute;
        }
        set
        {
            _audioSource.mute = value;
        }
    }

    public bool IsPlaying
    {
        get
        {
            return _audioSource.isPlaying;
        }
    }

    //초기화
    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    //재생한다
    public void PlaySound(string Filepath, bool blsLoop, bool bls3Dsound)
    {
        //리소스 매니져를 통해 클립을 가져온다
        _audioSource.clip = cResourceManager.GetInstance.
            GetAudioClipForKey(Filepath);

        _audioSource.loop = blsLoop;
        _audioSource.spatialBlend = bls3Dsound ? 1f : 0f;

        _audioSource.Play();
    }
    //정지한다.
    public void PauseSound()
    {
        _audioSource.Pause();
    }
    //중지한다.
    public void StopSound()
    {
        _audioSource.Stop();
    }

}
