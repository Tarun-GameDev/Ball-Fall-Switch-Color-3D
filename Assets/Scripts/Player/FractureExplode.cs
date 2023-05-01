using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureExplode : MonoBehaviour
{
    [Header("Assignables:")]
    [SerializeField] Vector3 explosionOffset;
    [Header("Forces To Explode:")]
    [SerializeField] float minForce;
    [SerializeField] float maxForce;
    [SerializeField] float radius;
    float destroytime = 6f;

    public void Start()
    {
        Explode();
    }

    public void Explode()
    {
        #region Exploding himself

        foreach (Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            }
            Destroy(t.gameObject, destroytime);
        }
        #endregion
    }
}
