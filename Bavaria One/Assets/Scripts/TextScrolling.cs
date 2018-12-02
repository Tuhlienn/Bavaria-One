using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScrolling : MonoBehaviour {

    public float scrollSpeed = 20;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        StartCoroutine(MyCoroutine());

        Vector3 pos = transform.position;
        if (transform.position.y < 250.0f)
        {
            Vector3 localVectorUp = transform.TransformDirection(0, 1, 0);
            pos += localVectorUp * scrollSpeed * Time.deltaTime;
            transform.position = pos;
        }
        else
        {
            //change Scene?
        }
    }

    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(27);
    }
}
