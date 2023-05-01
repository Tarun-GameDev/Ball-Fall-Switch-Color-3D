using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playingMenu;
    [SerializeField] GameObject deadMenu;
    [SerializeField] TextMeshProUGUI scoreText;

    public void DeadMenu()
    {
        playingMenu.SetActive(false);
        deadMenu.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ScoreText(int _score)
    {
        scoreText.text = _score.ToString("00");
    }
     
}
