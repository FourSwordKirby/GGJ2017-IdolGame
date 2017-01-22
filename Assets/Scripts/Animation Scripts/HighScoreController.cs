using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScoreController : MonoBehaviour {

    public HighScoreInstance ScoreInstance;
    public List<HighScoreInstance> highScores;


	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Display()
    {
        //ANIMATE

        this.gameObject.SetActive(true);

        string filePath = Application.streamingAssetsPath + "/HighScores.txt";
        string contents = File.ReadAllText(filePath);

        List<KeyValuePair<string, float>> entries = new List<KeyValuePair<string, float>>();
        foreach(string entry in contents.Split('\n'))
        {
            if(entry != "")
            {
                string name = entry.Split(' ')[0];
                float score = float.Parse(entry.Split(' ')[1]);
                entries.Add(new KeyValuePair<string, float>(name, score));
            }
        }

        entries.Sort((x, y) => (int)Mathf.Sign(y.Value - x.Value));

        for(int i = 0; i < Mathf.Min(entries.Count, 5); i++)
        {
            HighScoreInstance highScore = Instantiate(ScoreInstance);
            highScore.transform.parent = this.transform;
            highScore.nameText.text = entries[i].Key;
            highScore.scoreText.text = entries[i].Value.ToString();
            highScore.transform.position = this.transform.position + (Vector3.down * i * 100.0f);

            highScores.Add(highScore);
        }

        yield return null;
    }

    public IEnumerator Dismiss()
    {
        //ANIMATE

        this.gameObject.SetActive(false);
        foreach(HighScoreInstance entry in highScores)
        {
            Destroy(entry);
        }
        highScores = new List<HighScoreInstance>();
        yield return null;
    }
}
