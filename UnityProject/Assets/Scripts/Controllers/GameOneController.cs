using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOneController : MonoBehaviour, TextTyperDelegation
{

    public ExhibitionController[] screens;
    public DialogBox dialogBox;
    public string[] dialogs;
    private int currentScreen = 0;
    private int currentDialog = 0;
    private bool canShowNextMessage = false;

	// Use this for initialization
	void Start () {
        dialogBox.setupDialogBox(GetComponent<GameOneController>());
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return) && canShowNextMessage)
        {
            dialogBox.showMessage(dialogs[currentDialog]);
        }

        if (Input.GetKeyDown(KeyCode.Return) && currentScreen == 0)
        {
            showNextScreen();
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
        canShowNextMessage = true;
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

}
