using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy = 5f;
    void Start()
    {
        Destroy(gameObject, _timeToDestroy);
    }
}
