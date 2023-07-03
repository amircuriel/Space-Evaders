using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    #region Singleton Implementation
    public static SessionManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public bool isGameRunning = false;
    public TMP_Text fps;
    public TMP_Text Stopwatch;
    public Transform PauseScreen;
    public Transform GameOverScreen;
    public RectTransform ScrollingBackground;
    public float backgroundScrollSpeed = 5f;
    public AudioSource PointUpSound;
    private float currentTime;

    void Start()
    {
        StartCoroutine(StartFPSCounter());
        Stopwatch.text = "00:00";
        currentTime = 0f;
        isGameRunning = true;
    }

    public void PauseGame()
    {
        if (isGameRunning)
        {
            isGameRunning = false;
            PauseScreen.gameObject.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if (!isGameRunning && !GameOverScreen.gameObject.activeSelf)
        {
            isGameRunning = true;
            PauseScreen.gameObject.SetActive(false);
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        isGameRunning = false;
        GameOverScreen.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameRunning)
        {
            ScrollingBackground.offsetMin = new Vector2(ScrollingBackground.offsetMin.x, ScrollingBackground.offsetMin.y - backgroundScrollSpeed);
            currentTime += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            Stopwatch.text = time.ToString(@"mm\:ss");

        }
    }

    private float count;

    private IEnumerator StartFPSCounter()
    {
        GUI.depth = 2;
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnGUI()
    {
        fps.text = Mathf.Round(count).ToString();

        //if (!isGameRunning && !PauseScreen.gameObject.activeSelf)
        //{
        //    PauseGame();
        //}
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            PauseGame();
        }
    }

    public void PointUp()
    {
        PointUpSound.Play();
    }
}
