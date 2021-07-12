using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 10;

    void Update()
    {
        transform.Rotate(transform.up * Time.deltaTime * _rotationSpeed);
    }
}
