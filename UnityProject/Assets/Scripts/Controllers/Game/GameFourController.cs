using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFourController : GameController, QuestionDelegation, TextTyperDelegation
{
    public ExhibitionController[] tables;
    private ExhibitionController zeca;
    private AudioSource audioSource;

    override public void Setup()
    {
        base.Setup();
        this.questionBox.delegation = GetComponent<GameFourController>();
        zeca = GameObject.Find("ZecaMesa").GetComponent<ExhibitionController>();
        GetComponent<ExhibitionController>().Show(true, 2);
        StartCoroutine(setupDialog());
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.anyKeyDown && canShowNextScreen)
        {
            if (currentScreen == 1)
            {
                canShowNextScreen = false;
                zeca.Hide(true, 1);
                tables[0].Hide(true, 1);
                tables[1].Show(true, 2);
                audioSource.Play();
                StartCoroutine(showNextMsg());
            }

            if (currentScreen == 2)
            {
                canShowNextScreen = false;
                audioSource.Stop();
                questionBox.thisSoundIsSafe = isSafeSound;
                questionBox.setListenAgainAudio(sceneAudio);
                questionBox.GetComponent<ExhibitionController>().Show(true, 2);
            }

            if (currentScreen == 3)
            {
                canShowNextScreen = false;
                dialogBox.showMessage(dialogs[currentDialog]);
            }

            if (currentScreen == 4)
            {
                canShowNextScreen = false;
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
        tables[1].Hide(true, 1);
        zeca.Show(true, 2);
        tables[2].Show(true, 2);
        dialogBox.showMessage(dialogs[currentDialog]);
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
        dialogBox.setupDialogBox(GetComponent<GameFourController>());
        dialogBox.GetComponent<ExhibitionController>().Show(true, 2);
        StartCoroutine(ShowFirstDialog());
    }

    IEnumerator ShowFirstDialog()
    {
        yield return new WaitForSeconds(1);
        dialogBox.showMessage(dialogs[currentDialog]);
    }
}
