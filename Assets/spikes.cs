using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikes : MonoBehaviour
{

    [SerializeField] Collider col;
    [SerializeField] AudioSource spikesAudio;

    private void Start()
    {
        if (col == null)
            col = GetComponent<Collider>();
    }
    public void ActiveCollider()
    {
        col.enabled = true;
        if (spikesAudio != null)
            spikesAudio.Play();
    }

    public void DeactiveCollider()
    {
        col.enabled = false;
    }
}
