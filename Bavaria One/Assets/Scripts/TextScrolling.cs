using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextScrolling : MonoBehaviour {

    public float scrollSpeed;

    void Start()
    {
        StartCoroutine(MyCoroutine());
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);

        }
        
       

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

    }
}
