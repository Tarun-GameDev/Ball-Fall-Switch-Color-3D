using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowSpawner : MonoBehaviour
{
    [SerializeField] GameObject rowPrefab;
    [SerializeField] List<GameObject> allRows;
    [SerializeField] float newSpawnYPos = 5f;
    [SerializeField] int noOfRowsSpawned = 0;
    public bool uilimited = false;

    private void Start()
    {
        if(uilimited)
            SpawnRow();
    }

    public void SpawnRow()
    {
        GameObject obj = Instantiate(rowPrefab, new Vector3(0f,-(newSpawnYPos * noOfRowsSpawned), 0f), Quaternion.identity,transform);
        allRows.Add(obj);
        noOfRowsSpawned++;

        if (noOfRowsSpawned >= 7)
            DestroyRow();
    }

    void DestroyRow()
    {
        Destroy(allRows[0]);
        allRows.RemoveAt(0);
    }
}
