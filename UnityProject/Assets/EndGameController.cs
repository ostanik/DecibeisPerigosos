using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour, TextTyperDelegation
{

    public TextTyper[] texts;
    public ExhibitionController textBox;
    public ScoreView[] scores;
    public ExhibitionController btnRestart;
    public ExhibitionController btnSair;
    private bool showedScore = false;

    // Use this for initialization
    void Start () {
        texts[0].setText("Esse dia com você foi demais! Olha quantos decibels perigosos nós encontramos juntos:");
        texts[0].delegation = GetComponent<EndGameController>();

        var dataController = FindObjectOfType<DataController>();
        if (dataController != null && scores.Length == dataController.responseData.Length)
        {
            for(int i = 0; i < dataController.responseData.Length; i++)
            {
                scores[i].SetAnswer(dataController.responseData[i].isCorrect);
            }
        }

        StartCoroutine(ShowBox());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RestartGame()
    {
        var dataController = FindObjectOfType<DataController>();
        if (dataController != null)
        {
            dataController.Restart();
        }
        StartCoroutine(ChangeScene());
    }

    public void CloseButtom()
    {
        Application.Quit();
    }

    public void didFinishAnimation()
    {
        if (!showedScore)
        {
            var time = 0.5f;
            for (int i = 0; i < scores.Length; i++)
            {
                StartCoroutine(ShowScoreView(scores[i], time));
                time += 0.5f;
            }

            StartCoroutine(ShowLastText(time + 1));
            texts[1].delegation = GetComponent<EndGameController>();
            showedScore = true;
        }
        else
        {
            btnRestart.Show(true, 1);
            btnSair.Show(true, 1);
        }

    }

    IEnumerator ChangeScene()
    {
        float fadeTime = GameObject.Find("Fade_Object").GetComponent<Fade>().BeginFade(1);
        var dataController = FindObjectOfType<DataController>();
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("MenuScreen");
    }

    IEnumerator ShowLastText(float time)
    {
        yield return new WaitForSeconds(time);
        texts[1].setText("Espero que você também comece a caçar os decibels perigosos por aí e ajudar os seus amigos!\n\nE aí, quer jogar de novo ?");
        texts[1].StartTextAnimation();
    }

    IEnumerator ShowBox()
    {
        yield return new WaitForSeconds(1);
        textBox.Show(true, 2);
        texts[0].StartTextAnimation();
    }

    IEnumerator ShowScoreView(ScoreView scoreView, float time)
    {
        yield return new WaitForSeconds(time);
        scoreView.exhibition.Show(true, 0.5f);
    }
}
