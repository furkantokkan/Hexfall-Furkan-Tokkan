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
    
    void Start()
    {
        UpdateScoreText();
        UpdateMovesText();
    }

   public void ResetButton()
    {
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
