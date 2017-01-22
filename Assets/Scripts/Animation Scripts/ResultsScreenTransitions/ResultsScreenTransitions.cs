using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsScreenTransitions : MonoBehaviour {

    //Temporarily Hardcoded
    public static float score = 120;
    public static List<float> accumulatedScores = new List<float>() { 5.0f, 15.0f, 30.0f, 50.0f, 100.0f, 120.0f};

    public AudioClip buttonClick;

    public ScoreController scoreController;
    public ScoreChart scoreChart;
    public NameController nameController;
    public HighScoreController highScoreController;

    public ScreenFader fader;
    public bool titleScreenSelected;

    // Use this for initialization
    void Start () {
        scoreController.SetScore(score);
        scoreChart.DisplayScores(accumulatedScores);
        StartCoroutine(nameController.Display());
        StartCoroutine(fader.FadeIn());
    }

    public void BackToTitle()
    {
        AudioSource.PlayClipAtPoint(buttonClick, Camera.main.transform.position);
        if (!fader.fading)
        {
            StartCoroutine(fader.FadeOut());
            titleScreenSelected = true;
        }
    }

    public void SubmitHighScore()
    {
        string name = nameController.GetName();
        float score = ResultsScreenTransitions.score;

        if(name != "")
        {
            string filePath = Application.streamingAssetsPath + "/HighScores.txt";
            string contents = File.ReadAllText(filePath);
            contents += name + " " + score.ToString() + "\n";
            File.WriteAllText(filePath, contents);
            Debug.Log("wrote to log");
        }

        //Remove the submit button etc.
        StartCoroutine(nameController.Dismiss());
        StartCoroutine(highScoreController.Display());

        AudioSource.PlayClipAtPoint(buttonClick, Camera.main.transform.position);
    }

    void Update()
    {
        if (titleScreenSelected)
        {
            if (!fader.fading)
                SceneManager.LoadScene(0);
        }
    }
}
