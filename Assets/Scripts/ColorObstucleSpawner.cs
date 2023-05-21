using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObstucleSpawner : MonoBehaviour
{
    public GameObject colorObstucles;
    [SerializeField] Material[] blockMats;
    [SerializeField] string[] tagNames = { "Red", "Green", "Blue", "Yellow"};

    int spawnPos = -6;
    bool triggered = false;
    RowSpawner _spawner;


    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            int randomBlock = Random.Range(0, blockMats.Length);
            GameObject _block = Instantiate(colorObstucles, new Vector3(spawnPos + (i * 3), transform.position.y, 0f), Quaternion.identity,transform);
            _block.GetComponent<MeshRenderer>().material = blockMats[randomBlock];
            _block.tag = tagNames[randomBlock];
            _block.layer = LayerMask.NameToLayer(tagNames[randomBlock]);
        }

        _spawner = gameObject.GetComponentInParent<RowSpawner>();
        if (_spawner != null)
        {
            if (_spawner.uilimited)
                triggered = false;
            else
                triggered = true;
        }
        else
            triggered = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!triggered)
        {
            if (other.CompareTag("Player"))
            {              
                _spawner.SpawnRow();
                GameManager.instance.AddScore();
                triggered = true;
            }
        }

    }

}
