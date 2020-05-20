using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//리소스 매니저
public class cResourceManager : cSingleton<cResourceManager>
{
    private Dictionary<string, AudioClip> _audioClipList = null;

    //!초기화.
    protected override void Awake()
    {
        base.Awake();
        _audioClipList = new Dictionary<string, AudioClip>();
    }

    public AudioClip GetAudioClipForKey(string Filepath, bool blsAutoCreate = true)
    {
        if(blsAutoCreate && !_audioClipList.ContainsKey(Filepath))
        {
            var AudioClip = Resources.Load<AudioClip>(Filepath);
            _audioClipList.Add(Filepath, AudioClip);
        }
        return _audioClipList[Filepath];
    }



}
