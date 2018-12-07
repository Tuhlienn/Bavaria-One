using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMovement : MonoBehaviour {

    public Image TitleImage;
    public float scaleRate = -0.01f;
    public float minScale = 0.2f;
    public Transform[] ScrollingObjects;
    public float secondsTillScrolling = 2.7f;
    public float scrollSpeed;

    private float timeSinceStart = 0;
	
	// Update is called once per frame
	void Update () 
    {
        timeSinceStart += Time.deltaTime;

        //Scale Title
        if (TitleImage.transform.localScale.x < minScale)
        {
            scaleRate = 0.0f;
            TitleImage.gameObject.SetActive(false);
        }
        else 
        {
            TitleImage.transform.localScale += Vector3.one * scaleRate * Time.deltaTime;
        }

        if(timeSinceStart >= secondsTillScrolling) 
        {
            foreach(Transform transformObject in ScrollingObjects) 
            {
                Vector3 pos = transformObject.position;
                Vector3 localVectorUp = transformObject.TransformDirection(0, 1, 0);
                pos += localVectorUp * scrollSpeed * Time.deltaTime;
                transformObject.position = pos;
            }
        }
    }
}
