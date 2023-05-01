using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikes : MonoBehaviour
{

    [SerializeField] Collider col;

    private void Start()
    {
        if (col == null)
            col = GetComponent<Collider>();
    }
    public void ActiveCollider()
    {
        col.enabled = true;
    }

    public void DeactiveCollider()
    {
        col.enabled = false;
    }
}
