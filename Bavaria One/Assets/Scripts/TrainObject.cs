using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Policy;

public class TrainObject : MonoBehaviour
{
    Transform train;
    Transform packed;

    Vector3 target;

    void Awake()
    {
        train = transform.Find("train");
        packed = transform.Find("train_packed");
        target = transform.position;
    }

    void Update()
    {
        float frac = GameManager.Instance.DTime / (1.0f / GameManager.Instance.Speed);
        transform.position = Vector3.Lerp(transform.position, target, frac);
        transform.LookAt(target);
    }

    public void SetTarget(Vector2 vec)
    {
        target = new Vector3(vec.x, 0.0f, vec.y);
    }

    public void Unload()
    {
        train.gameObject.SetActive(true);
        packed.gameObject.SetActive(false);
    }

    public void PackUp()
    {
        train.gameObject.SetActive(false);
        packed.gameObject.SetActive(true);
    }

    public void Hide()
    {
        train.gameObject.SetActive(false);
        packed.gameObject.SetActive(false);
    }
}
