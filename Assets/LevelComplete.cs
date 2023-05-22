using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] ParticleSystem CompletedPartEff;
    [SerializeField] AudioSource levelCompletedAudio;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager _gameManager = GameManager.instance;

            PlayerPrefs.SetInt("LevelUnlocked", SceneManager.GetActiveScene().buildIndex + 1);

            _gameManager.uiManager.LevelComplete();
            _gameManager.ball.LevelCompleted();
            _gameManager.ball.DisablePowerUp();
            if (CompletedPartEff != null)
                CompletedPartEff.Play();
            if (levelCompletedAudio != null)
                levelCompletedAudio.Play();
        }
    }
}
