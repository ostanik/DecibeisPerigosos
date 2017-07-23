﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameThreeController : GameController, QuestionDelegation, TextTyperDelegation
{

    private int currentDialog = 0;
    private int currentScreen = 0;
    private bool canShowNextScreen = false;
    private ExhibitionController zeca;
    private ExhibitionController cellphone;

    // Use this for initialization
    void Start () {
		
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
        if (Input.anyKeyDown && canShowNextScreen)
        {
            if (currentScreen == 1)
            {
                zeca.Hide(true, 1);
                cellphone.Show(true, 2);
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
