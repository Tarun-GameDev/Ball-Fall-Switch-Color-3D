using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMat : MonoBehaviour
{
    [SerializeField] MeshRenderer[] objs;

    public void changeMaterial(Material _mat)
    {
        foreach (MeshRenderer mat in objs)
        {
            mat.material = _mat;
        }
    }
}
