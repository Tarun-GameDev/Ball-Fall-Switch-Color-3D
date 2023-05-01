using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.uiManager.LevelComplete();
            GameManager.instance.ball.levelCompleted = true;
            GameManager.instance.ball.DisablePowerUp();
        }
    }
}
