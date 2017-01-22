using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenTransitions : MonoBehaviour {

    public StartMenuController startMenuController;
    public HighScoreController highScoreController;

    public ScreenFader fader;
    public bool gameStarted;

    void Start()
    {
        StartCoroutine(fader.FadeIn());
    }

    public void StartGame()
    {
        if(!fader.fading)
        {
            StartCoroutine(fader.FadeOut());
            gameStarted = true;
        }
    }

    public void DisplayHighScores()
    {
        StartCoroutine(startMenuController.Dismiss());
        StartCoroutine(highScoreController.Display());
    }

    public void HideHighScores()
    {
        StartCoroutine(startMenuController.Display());
        StartCoroutine(highScoreController.Dismiss());
    }

    void Update()
    {
        if(gameStarted)
        {
            if (!fader.fading)
                SceneManager.LoadScene(1);
        }
    }
}
