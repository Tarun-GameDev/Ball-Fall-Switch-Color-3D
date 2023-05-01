using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObstucleSpawner : MonoBehaviour
{
    public GameObject[] colorObstucles;

    int spawnPos = -6;
    bool triggered = false;
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(colorObstucles[Random.Range(0, colorObstucles.Length)], new Vector3(spawnPos + (i * 3), transform.position.y, 0f), Quaternion.identity,transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!triggered)
        {
            if (other.CompareTag("Player"))
            {
                gameObject.GetComponentInParent<RowSpawner>().SpawnRow();
                GameManager.instance.AddScore();
                triggered = true;
            }
        }

    }

}
