using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManagerMenu : MonoBehaviour
{
    public static AudioManagerMenu Instance;
	[HideInInspector] private AudioSource audioMusic;
	public GameObject state;
	private void OnLevelWasLoaded(int level)
	{
		if (SceneManager.GetActiveScene().name == "MainGame")
		{
			Destroy(gameObject);
		}
	}
	private void Awake()
	{
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
	private void Start()
	{
		audioMusic = GetComponent<AudioSource>();
		PlayMenuMusic();
	}
	public void PlayMenuMusic()
	{
		audioMusic.Play();
	}
}

