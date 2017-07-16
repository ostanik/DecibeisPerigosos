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

        if ((thisSoundIsSafe && safePressed) || (!thisSoundIsSafe && !safePressed))
        {
            rightFeed.show();
        } else
        {
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

    public void didFinishShow()
    {
        print("Show");
    }
}
