using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFiveController : GameController, QuestionDelegation, TextTyperDelegation
{

    override public void Setup()
    {
        base.Setup();
        this.questionBox.delegation = GetComponent<GameFiveController>();
        GetComponent<ExhibitionController>().Show(true, 2);
        StartCoroutine(setupDialog());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && canShowNextScreen)
        {
            if (currentScreen < 3)
            {
                canShowNextScreen = false;
                dialogBox.showMessage(dialogs[currentDialog]);
            }

            if (currentScreen == 3)
            {
                canShowNextScreen = false;
                questionBox.thisSoundIsSafe = isSafeSound;
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
