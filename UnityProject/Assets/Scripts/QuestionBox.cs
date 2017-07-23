using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface QuestionDelegation
{
    void didFinishAnswer();
}

public class QuestionBox : MonoBehaviour, FeedDelegation {

    public bool thisSoundIsSafe = false;
    public Feedback rightFeed;
    public Feedback wrongFeed;
    public QuestionDelegation delegation;
    public int currentGame = -1;

    // Use this for initialization
    void Start () {
        rightFeed.delegation = GetComponent<QuestionBox>();
        wrongFeed.delegation = GetComponent<QuestionBox>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void safeButtonPress()
    {
        correct(true);
    }

    public void dangerButtonPress()
    {
        correct(false);
    }

    public void listenButtonPress()
    {

    }

    void correct(bool safePressed)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        var dataController = FindObjectOfType<DataController>();

        if ((thisSoundIsSafe && safePressed) || (!thisSoundIsSafe && !safePressed))
        {
            rightFeed.show();
            if (dataController != null)
            {
                dataController.countCorrect++;
                dataController.responseData[currentGame].isCorrect = true;
            }
        } else
        {
            if (dataController != null)
            {
                dataController.countError++;
                dataController.responseData[currentGame].isCorrect = false;
            }
            wrongFeed.show();
        }
    }

    public void didFinishHide()
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (delegation != null) {
            delegation.didFinishAnswer();
        }
    }

    public void didFinishShow() { }
    
}
