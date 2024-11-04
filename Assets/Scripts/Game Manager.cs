using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private TMP_Text scoreText;
    private int score;

    private void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnEnable()
    {
        BirdController.OnBirdGameOver += HandleGameOver;
        BirdController.OnScoreIncreased += IncreaseScore;
    }

    private void OnDisable()
    {
        BirdController.OnBirdGameOver -= HandleGameOver;
        BirdController.OnScoreIncreased -= IncreaseScore;
    }

    public void HandleGameOver()
    {
        SceneManager.LoadScene("Game Over");
        Debug.Log("Juego Terminado");
    }
    public void IncreaseScore(int points) 
    {
        score += points;
        scoreText.text = "Score: " + score; 
    }
    public int GetScore() 
    {
        return score;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}

