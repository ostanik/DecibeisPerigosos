using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTwoController : GameController, QuestionDelegation, TextTyperDelegation
{
    private bool canBusMove = false;
    private GameObject bus;
    private AudioSource audioSource;

    public void didFinishAnimation()
    {
        currentScreen++;
        currentDialog++;
        canShowNextScreen = true;
    }

    public void didFinishAnswer()
    {
        questionBox.GetComponent<ExhibitionController>().Hide();
        dialogBox.showMessage(dialogs[currentDialog]);
    }

    override public void Setup()
    {
        base.Setup();
        this.dialogBox.setupDialogBox(GetComponent<GameTwoController>());
        this.questionBox.delegation = GetComponent<GameTwoController>();
        GetComponent<ExhibitionController>().Show(true, 2);
        StartCoroutine(setupDialog());
        bus = GameObject.Find("Bus");
        gameController.backgroundAudio.Pause();
        audioSource.Play();
    }

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && canShowNextScreen)
        {
            if (currentScreen == 1)
            {
                canShowNextScreen = false;
                questionBox.thisSoundIsSafe = isSafeSound;
                questionBox.GetComponent<ExhibitionController>().Show(true, 2);
                questionBox.setListenAgainAudio(sceneAudio);
                audioSource.Pause();
                gameController.backgroundAudio.Play();
            }

            if (currentScreen == 2)
            {
                canShowNextScreen = false;
                dialogBox.GetComponent<ExhibitionController>().Hide(true, 2);
                gameController.backgroundAudio.Pause();
                audioSource.Play();
                canBusMove = true;
                StartCoroutine(nextGame());
            }
        }

        if (canBusMove && bus != null)
        {
            var pos = bus.transform.position;
            bus.transform.position = new Vector3(pos.x + 20, pos.y, pos.z);
        }
	}

    IEnumerator nextGame()
    {
        yield return new WaitForSeconds(3);
        audioSource.Stop();
        gameController.backgroundAudio.Play();
        gameController.loadNextGame();
    }

    IEnumerator setupDialog()
    {
        yield return new WaitForSeconds(2);
        dialogBox.setupDialogBox(GetComponent<GameTwoController>());
        dialogBox.GetComponent<ExhibitionController>().Show(true, 2);
        StartCoroutine(ShowFirstDialog());
    }

    IEnumerator ShowFirstDialog()
    {
        yield return new WaitForSeconds(1);
        dialogBox.showMessage(dialogs[currentDialog]);
    }
}
