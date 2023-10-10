using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int TotalCoinCount = 0;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameWonScreen;

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
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
