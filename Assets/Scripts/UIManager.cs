using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Text Settings")]
    public Text scoreText;
    public Text movesText;

    public GameObject gameOverPanel;
    
    void Start()
    {
        UpdateScoreText();
        UpdateMovesText();
    }
    private void Update()
    {
        if (GameManager.instance.gameOver)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void ResetButton()
    {
        print("reset");
        SceneManager.LoadScene(0);
    }

    public void UpdateScoreText()
    {
        scoreText.text = GameManager.instance.score.ToString();
    }
    public void UpdateMovesText()
    {
        movesText.text = GameManager.instance.moves.ToString();
    }
}
