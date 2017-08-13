using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameThreeController : GameController, QuestionDelegation, TextTyperDelegation
{
    private ExhibitionController zeca;
    private ExhibitionController cellphone;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    override public void Setup()
    {
        base.Setup();
        this.questionBox.delegation = GetComponent<GameThreeController>();
        zeca = GameObject.Find("ZecaCellphone").GetComponent<ExhibitionController>();
        cellphone = GameObject.Find("Cellphone").GetComponent<ExhibitionController>();
        GetComponent<ExhibitionController>().Show(true, 2);
        StartCoroutine(setupDialog());
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && canShowNextScreen)
        {
            if (currentScreen == 1)
            {
                zeca.Hide(true, 1);
                cellphone.Show(true, 2);
                StartCoroutine(showNextMsg());
                canShowNextScreen = false;
                audioSource.clip = sceneAudio;
                audioSource.Play();
                gameController.backgroundAudio.Pause();
            }

            if (currentScreen == 2)
            {
                questionBox.thisSoundIsSafe = isSafeSound;
                audioSource.Stop();
                questionBox.GetComponent<ExhibitionController>().Show(true, 2);
                questionBox.setListenAgainAudio(sceneAudio);
                canShowNextScreen = false;
                audioSource.Stop();
                gameController.backgroundAudio.Play();
            }

            if (currentScreen == 3)
            {
                gameController.loadNextGame();
            }
        }
    }

    public void didFinishAnimation()
    {
        currentScreen++;
        currentDialog++;
        canShowNextScreen = true;
    }

    public void didFinishAnswer()
    {
        zeca.Show(true, 2);
        cellphone.Hide(true, 1);
        StartCoroutine(showNextMsg());
        questionBox.GetComponent<ExhibitionController>().Hide();
    }

    IEnumerator showNextMsg()
    {
        yield return new WaitForSeconds(1);
        dialogBox.showMessage(dialogs[currentDialog]);
    }

    IEnumerator setupDialog()
    {
        yield return new WaitForSeconds(2);
        dialogBox.setupDialogBox(GetComponent<GameThreeController>());
        dialogBox.GetComponent<ExhibitionController>().Show(true, 2);
        StartCoroutine(ShowFirstDialog());
    }

    IEnumerator ShowFirstDialog()
    {
        yield return new WaitForSeconds(1);
        dialogBox.showMessage(dialogs[currentDialog]);
    }

}
