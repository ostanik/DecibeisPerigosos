using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFourController : GameController, QuestionDelegation, TextTyperDelegation
{

    private int currentDialog = 0;
    private int currentScreen = 0;
    private bool canShowNextScreen = false;
    public ExhibitionController[] tables;
    private ExhibitionController zeca;

    override public void Setup()
    {
        base.Setup();
        this.questionBox.delegation = GetComponent<GameFourController>();
        zeca = GameObject.Find("ZecaMesa").GetComponent<ExhibitionController>();
        GetComponent<ExhibitionController>().Show(true, 2);
        StartCoroutine(setupDialog());
    }

    // Update is called once per frame
    void Update () {
        if (Input.anyKeyDown && canShowNextScreen)
        {
            if (currentScreen == 1)
            {
                zeca.Hide(true, 1);
                tables[0].Hide(true, 1);
                tables[1].Show(true, 2);
                StartCoroutine(showNextMsg());
            }

            if (currentScreen == 2)
            {
                questionBox.thisSoundIsSafe = isSafeSound;
                questionBox.GetComponent<ExhibitionController>().Show(true, 2);
                canShowNextScreen = false;
            }

            if (currentScreen == 3)
            {
                dialogBox.showMessage(dialogs[currentDialog]);
                canShowNextScreen = false;
            }

            if (currentScreen == 4)
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
