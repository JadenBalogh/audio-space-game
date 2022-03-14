using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static AudioSystem AudioSystem { get; private set; }
    public static ScoreSystem ScoreSystem { get; private set; }

    [SerializeField] private CanvasGroup endScreen;

    private bool isGameOver = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        AudioSystem = GetComponent<AudioSystem>();
        ScoreSystem = GetComponent<ScoreSystem>();

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public static void EndGame() => instance.IEndGame();
    private void IEndGame()
    {
        isGameOver = true;
        Time.timeScale = 0;
        // AudioSystem.Stop();
        endScreen.alpha = 1f;
        endScreen.blocksRaycasts = true;
        endScreen.interactable = true;
    }
}
