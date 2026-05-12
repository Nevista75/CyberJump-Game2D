using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundType
    {
        Jump,
        Hurt,
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

        var soundObj = new GameObject($"Sound_{type}");
        var audioSrc = soundObj.AddComponent<AudioSource>();

        audioSrc.clip = s.Clip;
        audioSrc.volume = s.Volume;

        audioSrc.Play();

        Destroy(soundObj, s.Clip.length);
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
        _musicSource.Play();
    }
}
