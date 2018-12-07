using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

	public AudioSource MusicSource;
    public AudioClip Music;

	void Start () {
		MusicSource.clip = Music;
        MusicSource.Play();
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
			skipIntro();
        }
	}

	public void skipIntro() 
	{
		SceneManager.LoadScene(1, LoadSceneMode.Single);
	}
}
