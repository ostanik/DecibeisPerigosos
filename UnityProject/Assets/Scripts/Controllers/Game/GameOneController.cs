using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOneController : GameController, TextTyperDelegation, QuestionDelegation
{   

    public ExhibitionController[] screens;


	// Use this for initialization
	void Start () {
        base.Setup();
        dialogBox.setupDialogBox(GetComponent<GameOneController>());
        questionBox.delegation = GetComponent<GameOneController>();
	}

	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown && canShowNextScreen)
        {
            if (currentScreen == 1) {
                questionBox.thisSoundIsSafe = isSafeSound;
                questionBox.setListenAgainAudio(sceneAudio);
                questionBox.GetComponent<ExhibitionController>().Show(true, 2);
                canShowNextScreen = false;
            }

            if (currentScreen == 2) {
                FindObjectOfType<GameScreenController>().loadNextGame();
                currentScreen++;
                canShowNextScreen = false;
            }
        }

        if (Input.anyKeyDown && currentScreen == 0)
        {
            showNextScreen();
            gameController = FindObjectOfType<GameScreenController>();
            GetComponent<AudioSource>().Pause();
            gameController.backgroundAudio.clip = backgroundAudio;
            gameController.backgroundAudio.volume = 0.5f;
            gameController.backgroundAudio.Play();
        }
    }

    public void showNextScreen()
    {
        if (currentScreen + 1 < screens.Length)
        {
            currentScreen++;
            screens[currentScreen].Show(true, 2);
        }

        switch(currentScreen)
        {
            case 1:
                StartCoroutine(ShowFirstMessage(2));            
                break;
        }
    }

    public void didFinishAnimation()
    {
        canShowNextScreen = true;
        if (currentDialog + 1 < dialogs.Length)
        {
            currentDialog++;
        }
    }

    IEnumerator ShowFirstMessage(float time)
    {
        yield return new WaitForSeconds(time);
        dialogBox.GetComponent<ExhibitionController>().Show(true, 2);
        dialogBox.showMessage(dialogs[currentDialog]);
    }

    public void didFinishAnswer()
    {
        dialogBox.showMessage(dialogs[currentDialog]);
        questionBox.GetComponent<ExhibitionController>().Hide(true, 2);
        currentScreen++;
    }
}
