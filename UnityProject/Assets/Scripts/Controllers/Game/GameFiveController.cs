using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFiveController : GameController, QuestionDelegation, TextTyperDelegation
{
    private AudioSource audioSource;

    override public void Setup()
    {
        base.Setup();
        this.questionBox.delegation = GetComponent<GameFiveController>();
        GetComponent<ExhibitionController>().Show(true, 2);
        StartCoroutine(setupDialog());
        gameController.backgroundAudio.Pause();
        audioSource.Play();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && canShowNextScreen)
        {
            if (currentScreen < 3)
            {
                canShowNextScreen = false;
                dialogBox.showMessage(dialogs[currentDialog]);
            }

            if (currentScreen == 3)
            {
                canShowNextScreen = false;
                audioSource.Stop();
                gameController.backgroundAudio.Play();
                questionBox.thisSoundIsSafe = isSafeSound;
                questionBox.setListenAgainAudio(sceneAudio);
                questionBox.GetComponent<ExhibitionController>().Show(true, 2);
            }

            if (currentScreen == 4)
            {
                canShowNextScreen = false;
                dialogBox.showMessage(dialogs[currentDialog]);
            }

            if (currentScreen == 5)
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
        dialogBox.showMessage(dialogs[currentDialog]);
        questionBox.GetComponent<ExhibitionController>().Hide();
        gameController.backgroundAudio.Pause();
        audioSource.Play();
    }

    IEnumerator showNextMsg()
    {
        yield return new WaitForSeconds(1);
        dialogBox.showMessage(dialogs[currentDialog]);
    }

    IEnumerator setupDialog()
    {
        yield return new WaitForSeconds(2);
        dialogBox.setupDialogBox(GetComponent<GameFiveController>());
        dialogBox.GetComponent<ExhibitionController>().Show(true, 2);
        StartCoroutine(ShowFirstDialog());
    }

    IEnumerator ShowFirstDialog()
    {
        yield return new WaitForSeconds(1);
        dialogBox.showMessage(dialogs[currentDialog]);
    }
}
