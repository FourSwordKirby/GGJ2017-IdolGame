using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public Text ScoreDisplay;
    public Text RankDisplay;

    public GameObject scoreExplode;
    public GameObject explodeParticles;

    float score;
    float targetScore;

    public AudioClip successSfx;

    bool exploded;
    
    public void SetScore(float s)
    {
        this.targetScore = s;
    }

    //This class used to display the score on the final results screen, do so here
    public void Update()
    {
        ScoreDisplay.text = "Score: " + (int)score;
        float prevScore = score;
        score = Mathf.Lerp(score, targetScore, Time.deltaTime);

        if (targetScore - score < 1 && !exploded)
        {
            AudioSource.PlayClipAtPoint(successSfx, Camera.main.transform.position);

            exploded = true;
            StartCoroutine(Explode(10));
            StartCoroutine(Explode2(25));
            StartCoroutine(DisplayRank());
        }

        if (score > 20000 && prevScore <= 20000)
        {
            StartCoroutine(Explode(2));
            StartCoroutine(Explode2(10));
        }
        else if (score > 18000 && prevScore <= 18000)
        {
            StartCoroutine(Explode(2));
            StartCoroutine(Explode2(10));
        }
        else if (score > 14000 && prevScore <= 14000)
        {
            StartCoroutine(Explode(2));
            StartCoroutine(Explode2(10));
        }
        else if (score > 8000 && prevScore <= 8000)
        {
            StartCoroutine(Explode(2));
            StartCoroutine(Explode2(10));
        }
        else if (score > 2000 && prevScore <= 2000)
        {
            StartCoroutine(Explode(2));
            StartCoroutine(Explode2(10));
        }
    }

    private IEnumerator Explode(int intensity)
    {
        for (int i = 0; i < intensity; i++)
        {
            GameObject o = (GameObject)Instantiate(scoreExplode);
            o.GetComponent<Text>().text = "Score: " + (int)score;
            o.transform.parent = transform;
            RectTransform r = o.GetComponent<RectTransform>();
            r.localPosition = scoreExplode.GetComponent<RectTransform>().localPosition;
            o.SetActive(true);
            Destroy(o, 2);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator Explode2(int intensity)
    {
        for (int i = 0; i < intensity; i++)
        {
            Instantiate(explodeParticles, new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(10f, 20f)), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator DisplayRank()
    {
        float flashTime = 0.1f;
        if (score > 20000)
            RankDisplay.text = "<size=100><b>SSS</b></size> UGOI!!";
        else if (score > 18000)
            RankDisplay.text = "<size=100><b>SS</b></size> UGOI!";
        else if (score > 14000)
            RankDisplay.text = "<size=100><b>S</b></size> UGOI";
        else if (score > 8000)
            RankDisplay.text = "<size=100><b>A</b></size>> nime";
        else if (score > 2000)
            RankDisplay.text = "<size=100><b>B</b></size> -b-baka";

        float timer = 0.0f;
        while (timer  < 3*flashTime)
        {
            timer += Time.deltaTime;
            if(timer < flashTime)
                RankDisplay.color = Color.Lerp(Color.white - Color.black, Color.white, timer / flashTime);
            else 
                RankDisplay.color = Color.Lerp(Color.white, Color.black, (timer-flashTime) / (2 * flashTime));
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }
}
