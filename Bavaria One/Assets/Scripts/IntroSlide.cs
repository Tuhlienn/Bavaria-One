using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IntroSlide : MonoBehaviour {

    public Image introText;
    float moveRate = 1.0f;

    // Use this for initialization
    void Start () {
        introText = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {

        if (introText.transform.position.y > 350) {
          moveRate = 0.0f;
        introText.color = new Vector4(0, 0, 0, 0);
        }
        introText.transform.localPosition += new Vector3(0, moveRate, moveRate);
    }
}
