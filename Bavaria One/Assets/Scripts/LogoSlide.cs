using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LogoSlide : MonoBehaviour {

    public Image image;
    public AudioClip IntroMusic;
    float scaleRate = -0.01f;
    float minScale= 0.8f;

    // Use this for initialization
    void Awake() {
        SoundManager.Instance.PlayMusic(IntroMusic);
    }

	void Start () {
        image = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {

        if (image.transform.localScale.x < minScale)
        {
            scaleRate = 0.0f;
            image.color = new Vector4(0, 0, 0, 0);
        }
        image.transform.localScale += Vector3.one * scaleRate;
    }   
}
