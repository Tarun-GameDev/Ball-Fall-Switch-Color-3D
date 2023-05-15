using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] Vector3 rotateAxis;
    [SerializeField] float rotationSpeed = 30f;

    void Update()
    {
        transform.Rotate(rotateAxis * rotationSpeed * Time.deltaTime);
    }
}
