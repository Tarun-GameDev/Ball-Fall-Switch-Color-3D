using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblePowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ActivateInvisiblePowerUp();
        }
    }

    void ActivateInvisiblePowerUp()
    {
        GameManager.instance.ball.InvisiblePowerUp();
        gameObject.SetActive(false);
    }
}
