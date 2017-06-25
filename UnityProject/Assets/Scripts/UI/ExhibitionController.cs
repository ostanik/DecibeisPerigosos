using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitionController : MonoBehaviour {
    public bool showOnInit = false;

	// Use this for initialization
	void Start () {
        if (showOnInit)
            Show();
        else
            Hide();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Show(bool animated = false, float transitionTime = 2)
    {
        setup(true, animated, transitionTime);
    }

    public void Hide(bool animated = false, float transitionTime = 2)
    {
        setup(false, animated, transitionTime);
    }

    void setup(bool canShow = true, bool transition = false, float transitionTime = 2)
    {
        var canvasGroup = GetComponentInChildren<CanvasGroup>();
        if (transition)
        {
            StartCoroutine(FadeTransition(canShow, transitionTime));
        }
        else
        {
            canvasGroup.alpha = canShow ? 1 : 0;
        }
        canvasGroup.interactable = canShow;
    }

    IEnumerator FadeTransition(bool canShow, float time)
    {
        var canvasGroup = GetComponentInChildren<CanvasGroup>();
        if (canShow)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime * time;
                yield return null;
            }

        }
        else
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime * time;
                yield return null;
            }
        }
    }
}
