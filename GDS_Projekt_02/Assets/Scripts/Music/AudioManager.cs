using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Sound[] sounds;
    [SerializeField] private Slider volumeSlider;

    void Awake()
    {
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.valume;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = s.mixerGroup;
            s.source.loop = s.loop;
        }
        volumeSlider.value = PlayerPrefs.GetFloat("volumeMain");
    }

    private void Start()
    {
      mainMixer.SetFloat("Volume", PlayerPrefs.GetFloat("volumeMain"));
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Nie ma takiego dźwięku jak: " + name);
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Nie ma takiego dźwięku jak: " + name);
            return;
        }
        s.source.Stop();
    }
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("volumeMain", volume);
    }
}
