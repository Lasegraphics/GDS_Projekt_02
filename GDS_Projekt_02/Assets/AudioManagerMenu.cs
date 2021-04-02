using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManagerMenu : MonoBehaviour
{

	AudioSource audio;

	public static AudioManagerMenu Instance = null;
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
		 audio = GetComponent<AudioSource>();
		PlayMenuMusic();

	}
    public void PlayMenuMusic()
    {
		audio.Play();

	}

}

