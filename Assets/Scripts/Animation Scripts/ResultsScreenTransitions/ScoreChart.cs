using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreChart : MonoBehaviour {

    public float scoreCeiling;

    public List<float> scores;
    public float chartHeight;
    public float chartWidth;

    public GameObject marker;

    private List<Vector2> DrawPoints;

	// Use this for initialization
    //Need to hack around this to make sure it works cleanly from the other scene
	public void DisplayScores (List<float> scores) {
        this.scores = scores;

        DrawPoints = new List<Vector2>() { Vector2.zero };

        for (int i = 0; i < scores.Count; i++)
        {
            float xPos = (i+1) * chartWidth / scores.Count;
            float yPos = scores[i]/scoreCeiling * chartHeight;

            DrawPoints.Add(new Vector2(xPos, yPos));
        }
        StartCoroutine(PlotScore());
	}

    public IEnumerator PlotScore()
    {
        float segmentDrawTime = 1.0f;
        float timer = 0;

        for (int i = 0; i < DrawPoints.Count-1; i++)
        {
            while(timer < segmentDrawTime)
            {
                timer += Time.deltaTime;
                marker.transform.position = Vector3.Lerp(DrawPoints[i], DrawPoints[i+1], timer/segmentDrawTime);
                yield return new WaitForSeconds(0.01f);
            }
            timer = 0;
        }
        yield return null;
    }
}
