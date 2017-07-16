using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FeedDelegation
{
    void didFinishHide();
    void didFinishShow();
}

public class Feedback : MonoBehaviour {

    private CanvasGroup canvas;
    private ExhibitionController exhibition;
    public FeedDelegation delegation;
    private bool dispatchOnce = true;

    // Use this for initialization
    void Start () {
        exhibition = GetComponent<ExhibitionController>();
        canvas = GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown && canvas.alpha == 1)
        {
            hide();
        }

        canvas.blocksRaycasts = canvas.alpha == 1;

        if (canvas.alpha == 1 && delegation != null && !dispatchOnce)
        {
            delegation.didFinishShow();
            dispatchOnce = true;
        }

        if (canvas.alpha == 0 && delegation != null && !dispatchOnce)
        {
            delegation.didFinishHide();
            dispatchOnce = true;
        }
	}

    public void show()
    {
        exhibition.Show(true, 2);
        dispatchOnce = false;
    }

    public void hide()
    {
        exhibition.Hide(true, 2);
        dispatchOnce = false;
    }
}
