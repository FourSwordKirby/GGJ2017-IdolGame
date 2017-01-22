using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenTransitions : MonoBehaviour {

    public StartMenuController startMenuController;
    public HighScoreController highScoreController;

    public AudioClip buttonClick;

    public ScreenFader fader;
    public bool gameStarted;

    void Start()
    {
        StartCoroutine(fader.FadeIn());
    }

    public void StartGame()
    {
        AudioSource.PlayClipAtPoint(buttonClick, Camera.main.transform.position);

        if (!fader.fading)
        {
            StartCoroutine(fader.FadeOut());
            gameStarted = true;
        }
    }

    public void DisplayHighScores()
    {
        StartCoroutine(startMenuController.Dismiss());
        StartCoroutine(highScoreController.Display());

        AudioSource.PlayClipAtPoint(buttonClick, Camera.main.transform.position);
    }

    public void HideHighScores()
    {
        StartCoroutine(startMenuController.Display());
        StartCoroutine(highScoreController.Dismiss());

        AudioSource.PlayClipAtPoint(buttonClick, Camera.main.transform.position);
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
