using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//사운드 매니저
public class cSoundManager : cSingleton<cSoundManager>
{
    private bool _BackgroundMute = false;
    private bool _effectMute = false;

    private float _effectVolume = 1f;
    private float _backgroundVolume = 1f;

    private cSound _backGround = null;
    private Dictionary<string, List<cSound>> _effectSoundList = null;

    public float EffectVolume
    {
        get
        {
            return _effectVolume;
        }
        set
        {
            _effectVolume = value;
            //사운드 돌려서
        }
    }

    public float BackfroundVolue
    {
        get
        {
            return _backGround.Volume;
        }
        set
        {
            _backgroundVolume = value;
            _backGround.Volume = value;
        }
    }
    public bool EffectMute
    {
        get
        {
            return _effectMute;
        }
        set
        {
            _effectMute = value;
            //쿼리문으로 다 돌려서 값 수정해주기.
        }
    }

    public bool BackgroundMute
    {
        get
        {
            return _BackgroundMute;
        }
        set
        {
            _BackgroundMute = value;
            _backGround.Mute = value;
        }
    }

    //초기화
    protected override void Awake()
    {
        base.Awake();
        _effectSoundList = new Dictionary<string, List<cSound>>();

        
        GameObject obj = new GameObject();
        obj.AddComponent<cSound>();
        obj.name = "BackGroundSound";
        obj.transform.parent = this.transform;
        _backGround = obj.GetComponent<cSound>();

    }

    public void PlayBackgroundSound(string filepath, bool blsLoop = true)
    {
        this.BackfroundVolue = _backgroundVolume;
        _backGround.PlaySound(filepath, blsLoop, false);
    }

    public void PlayEffectSound(string filepath, bool blsLoop = false,
        bool bls3d = false)
    {
        var sound = this.FindPlayableEffectSound(filepath);

        if(sound != null)
        {
            this.EffectVolume = _effectVolume;
            sound.PlaySound(filepath, blsLoop, bls3d);
        }

    }

    //효과음 리스트를 순회한다.
    private void EnumerateEffectSoundList(System.Action<cSound>
        callback)
    {
        //using System.Linq 추가해줄것.
        var keyList = _effectSoundList.Keys.ToList();

        for(int i=0; i<keyList.Count; ++i)
        {
            string key = keyList[i];
            var soundList = _effectSoundList[key];
            for(int j=0; j< soundList.Count; ++j)
            {
                var sound = soundList[j];
                callback(sound);
            }
        }
    }
    //재생 가능한 효과음을 탐색한다.
    private cSound  FindPlayableEffectSound(string filepath)
    {
        if (!_effectSoundList.ContainsKey(filepath))
        {
            var tempSoundList = new List<cSound>();
            _effectSoundList.Add(filepath, tempSoundList);
        }
        var soundList = _effectSoundList[filepath];
        //최대 중첩 횟수를 벗어나지 않았을 경우.
        if (soundList.Count < 10)
        {
            GameObject obj = new GameObject();
            obj.AddComponent<cSound>();
            obj.name = "EffectSound";
            obj.transform.parent = this.transform;
            soundList.Add(obj.GetComponent<cSound>());

            return obj.GetComponent<cSound>();
        }
        else
        {

            for (int i = 0; i < soundList.Count; ++i)
            {
                var sound = soundList[i];

                if (!sound.IsPlaying)
                    return sound;
            }
        }
        return null;
    }
}
