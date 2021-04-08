using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManagerMenu : MonoBehaviour
{
	[HideInInspector] private AudioSource audioMusic;
	
	private void OnLevelWasLoaded(int level)
	{
		if (SceneManager.GetActiveScene().name == "MainGame")
		{
			Destroy(gameObject);
		}
	}
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
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

