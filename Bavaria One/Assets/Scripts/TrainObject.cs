using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro.EditorUtilities;
using System.Security.Policy;

public class TrainObject : MonoBehaviour
{
    Renderer train, packed;

    public void Awake()
    {
        train = transform.Find("train").GetComponentInChildren<Renderer>();
        packed = transform.Find("train_packed").GetComponentInChildren<Renderer>();
    }

    public void SetPosition(Vector2 vec)
    {
        transform.position = new Vector3(vec.x, 0f, vec.y);
    }

    public void Unload()
    {
        if (train != null && packed != null)
        {
            train.enabled = true;
            packed.enabled = false;
        }
    }

    public void PackUp()
    {
        if (train != null && packed != null)
        {
            train.enabled = false;
            packed.enabled = true;
        }
    }

    public void Hide()
    {
        if (train != null && packed != null)
        {
            train.enabled = false;
            packed.enabled = false;
        }
    }

    public void MoveTo(Vector2 vec, float frac)
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(vec.x, 0f, vec.y), frac);
        transform.LookAt(new Vector3(vec.x, 0.0f, vec.y));
    }
}
