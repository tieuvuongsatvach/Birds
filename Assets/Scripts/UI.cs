using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public GameObject Panel;


    public void SetScoreText(string txt)
    {
        if (scoreText)
            scoreText.text = txt;
    }

    public void ShowPanel(bool isShow)
    {
        if (Panel)
            Panel.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void LevelMenu()
    {
        SceneManager.LoadScene("LevelMenu");
    }
}
