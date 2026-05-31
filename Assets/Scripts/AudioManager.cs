using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundType
    {
        Move,
        Jump,
        JumpAlt,
        Hurt,
        Land,
        Portal,
        EndPortal,
        Click,
        MenuMusic,
        Level1Music,
        Level2Music,
        Level3Music
    }
    [System.Serializable]
    public class Sound
    {
        public SoundType Type;
        public AudioClip Clip;

        [Range(0f, 1f)]
        public float Volume = 1f;

        [HideInInspector]
        public AudioSource Source;
    }

    public static AudioManager Instance;
    public Sound[] AllSounds;
    private Dictionary<SoundType, Sound> _soundDictionary = new Dictionary<SoundType, Sound>();
    private Dictionary<SoundType, GameObject> _activeLongSounds = new Dictionary<SoundType, GameObject>();
    private AudioSource _musicSource;

    private void Awake()
    {
        Instance = this;

        foreach(var s in AllSounds)
        {
            _soundDictionary[s.Type] = s;
        }    
    }

    public SoundType SelectedSound;

    public void Play(SoundType type)
    {
        if (!_soundDictionary.TryGetValue(type, out Sound s))
        {
            Debug.LogWarning($"Sound type {type} not found!");
            return;
        }

        var checkHurt = GameObject.Find("Sound_Land");
        if (checkHurt != null)
        {
            Destroy(checkHurt);
        }

        var soundObj = new GameObject($"Sound_{type}");
        var audioSrc = soundObj.AddComponent<AudioSource>();

        audioSrc.clip = s.Clip;
        audioSrc.volume = s.Volume;

        audioSrc.Play();

        if (type.ToString().Contains("EndPortal"))
        {
            DontDestroyOnLoad(soundObj);
        }

        Destroy(soundObj, s.Clip.length);
    }

    public void PlayLong(SoundType type, bool action)
    {
        if (!_soundDictionary.TryGetValue(type, out Sound s))
        {
            Debug.LogWarning($"Sound type {type} not found!");
            return;
        }


        if(action) {
            if (_activeLongSounds.ContainsKey(type)) return;
            var soundObj = new GameObject($"Sound_{type}");
            var audioSrc = soundObj.AddComponent<AudioSource>();

            audioSrc.clip = s.Clip;
            audioSrc.volume = s.Volume;

            audioSrc.loop = true;
            audioSrc.Play();

            _activeLongSounds.Add(type, soundObj);
        } else
        {
            if (_activeLongSounds.TryGetValue(type, out GameObject soundObj)) {
                Destroy(soundObj);
                _activeLongSounds.Remove(type);
            }
        }
    }

    public void ChangeMusic(SoundType type)
    {
        if (!_soundDictionary.TryGetValue(type, out Sound track))
        {
            Debug.LogWarning($"Music track {type} not found!");
            return;
        }

        if(_musicSource == null)
        {
            var container = new GameObject("SoundTrackObj");
            _musicSource = container.AddComponent<AudioSource>();
            _musicSource.loop = true;
        }

        _musicSource.clip = track.Clip;
        _musicSource.volume = track.Volume;
        _musicSource.Play();
    }
}