using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController: MonoBehaviour
{
    public string[] dialogs;

    public bool isSafeSound;
    public DialogBox dialogBox;
    public QuestionBox questionBox;
    public GameScreenController gameController;

    virtual public void Setup()
    {
        dialogBox = FindObjectOfType<DialogBox>();
        dialogBox.setupDialogBox(GetComponent<GameOneController>());
        questionBox = FindObjectOfType<QuestionBox>();
        gameController = FindObjectOfType<GameScreenController>();
    }

}

public class GameTwoController : GameController, QuestionDelegation, TextTyperDelegation
{
    private int currentDialog = 0;
    private int currentScreen = 0;
    private bool canShowNextScreen = false;
    private bool canBusMove = false;
    private GameObject bus;

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
        this.questionBox.delegation = GetComponent<GameTwoController>();
        GetComponent<ExhibitionController>().Show(true, 2);
        StartCoroutine(setupDialog());
        bus = GameObject.Find("Bus");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown && canShowNextScreen)
        {
            if (currentScreen == 1)
            {
                questionBox.thisSoundIsSafe = isSafeSound;
                questionBox.GetComponent<ExhibitionController>().Show(true, 2);
                canShowNextScreen = false;
            }

            if (currentScreen == 2)
            {
                dialogBox.GetComponent<ExhibitionController>().Hide(true, 2);
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
