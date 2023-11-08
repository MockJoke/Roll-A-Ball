using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int TotalCoinCount = 0;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameWonScreen;
    [SerializeField] private GameObject inputControlsScreen;

    private void Awake()
    {
        TotalCoinCount = FindObjectsOfType<Collectible>().Length;
    }

    public void ToggleGameOverScreen(bool status)
    {
        gameOverScreen.SetActive(status);    
    }
    
    public void ToggleGameWonScreen(bool status)
    {
        gameWonScreen.SetActive(status);    
    }
    
    public void ToggleInputControlsScreen(bool status)
    {
        inputControlsScreen.SetActive(status);    
    }
    
    public void DisplayScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }
    
    public void Restart()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
