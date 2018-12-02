using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextScrolling : MonoBehaviour {

    public float scrollSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        StartCoroutine(MyCoroutine());
       

    }

    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(27);
        Vector3 pos = transform.position;
        if (transform.position.y < 250.0f)
        {
            Vector3 localVectorUp = transform.TransformDirection(0, 1, 0);
            pos += localVectorUp * scrollSpeed * Time.deltaTime;
            transform.position = pos;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(0, LoadSceneMode.Single);
      
            }
        }

    }
}
