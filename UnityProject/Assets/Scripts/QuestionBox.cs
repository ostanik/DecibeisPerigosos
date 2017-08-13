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
    private AudioSource audioSource;
    public Feedback rightFeed;
    public Feedback wrongFeed;
    public QuestionDelegation delegation;
    public int currentGame = -1;
    private bool corrected = false;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        rightFeed.delegation = GetComponent<QuestionBox>();
        wrongFeed.delegation = GetComponent<QuestionBox>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setListenAgainAudio(AudioClip clip)
    {
        audioSource.clip = clip;
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
        var gamecontroller = FindObjectOfType<GameScreenController>();

        if (!audioSource.isPlaying) {
            audioSource.Play();
            gamecontroller.backgroundAudio.Pause();
        }
        else
        {
            gamecontroller.backgroundAudio.Play();
            audioSource.Stop();
        }
    }

    void correct(bool safePressed)
    {
        if (!corrected)
        {
            corrected = true;
            GetComponent<CanvasGroup>().blocksRaycasts = false;

            var dataController = FindObjectOfType<DataController>();

            if (audioSource.isPlaying)
            {
                FindObjectOfType<GameScreenController>().backgroundAudio.Play();
                audioSource.Stop();
            }

            if ((thisSoundIsSafe && safePressed) || (!thisSoundIsSafe && !safePressed))
            {
                rightFeed.show();
                rightFeed.GetComponent<AudioSource>().Play();
                if (dataController != null)
                {
                    dataController.countCorrect++;
                    dataController.responseData[currentGame].isCorrect = true;
                }
            }
            else
            {
                if (dataController != null)
                {
                    dataController.countError++;
                    dataController.responseData[currentGame].isCorrect = false;
                }
                wrongFeed.GetComponent<AudioSource>().Play();
                wrongFeed.show();
            }
        }
    }

    public void didFinishHide()
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (delegation != null) {
            delegation.didFinishAnswer();
        }
        corrected = false;
    }

    public void didFinishShow() { }
    
}
