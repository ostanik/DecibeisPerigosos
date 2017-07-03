using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroController : MonoBehaviour, TextTyperDelegation
{

    public string[] texts;
    public ExhibitionController dialog;
    public TextTyper dialogText;
    public InputField playerName;
    public GameObject start;
    public ExhibitionController next;
    public int currentDialog = 0;

	// Use this for initialization
	void Start () {
        dialogText.delegation = GetComponent<IntroController>();
        StartCoroutine(ShowDialogAfter(1));
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return)) {
            dialogText.Finish();
        }

    }

    IEnumerator ShowDialogAfter(float time)
    {
        yield return new WaitForSeconds(time);
        dialog.Show(true, 2);
        StartCoroutine(ShowMessageAfter(0.5f));
    }

    IEnumerator ShowMessageAfter(float time)
    {
        yield return new WaitForSeconds(time);
        dialogText.setText(texts[currentDialog]);
        dialogText.StartTextAnimation();
    }

    public void StartGame()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        float fadeTime = GameObject.Find("Fade_Object").GetComponent<Fade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Game");
    }

    public void nextDialog()
    {
        playerName.GetComponent<ExhibitionController>().Hide();
        next.Hide();
        currentDialog++;
        dialogText.setText("");
        StartCoroutine(ShowMessageAfter(0.5f));
    }

    public void didFinishAnimation()
    {
        switch (currentDialog)
        {
            case 0:
                playerName.GetComponent<ExhibitionController>().Show(true, 5);
                next.Show(true, 2);
                break;
            case 1:
                next.Show(true, 2);
                break;
            case 2:
                start.GetComponent<ExhibitionController>().Show(true, 2);
                break;
        }
    }
}