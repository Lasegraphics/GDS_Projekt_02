using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManagerMenu : MonoBehaviour
{
	[HideInInspector] private AudioSource audioMusic;
	public GameObject state;
	private void OnLevelWasLoaded(int level)
	{
		if (SceneManager.GetActiveScene().name == "MainGame")
		{
			Destroy(gameObject);
		}

		if (FindObjectsOfType<AudioManagerMenu>().Length>1)
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

