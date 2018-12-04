using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMusic : MonoBehaviour {
    public AudioSource MusicSource;
    public AudioClip Music;
    // Use this for initialization
    void Start () {
        MusicSource.clip = Music;
        MusicSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
