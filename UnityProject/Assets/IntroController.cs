using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour {

    public string[] texts;
    public ExhibitionController dialog;
    public TextTyper dialogText;
    public InputField playerName;
    public int currentDialog = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(ShowDialogAfter(1));
	}
	
	// Update is called once per frame
	void Update () {
		if (dialogText.isFinished)
        {
            if (currentDialog == 0) {
                playerName.GetComponent<ExhibitionController>().Show(true, 5);
                currentDialog++;
            }

            if (currentDialog == 1 && Input.GetKeyDown(KeyCode.Return))
            {
                playerName.GetComponent<ExhibitionController>().Hide();
                dialogText.setText("");
                StartCoroutine(ShowMessageAfter(0.5f, true));
            }

            if (currentDialog ==2 && Input.GetKey(KeyCode.Return))
            {
                dialogText.setText("");
                StartCoroutine(ShowMessageAfter(0.5f));
            }
        }
	}

    IEnumerator ShowDialogAfter(float time)
    {
        yield return new WaitForSeconds(time);
        dialog.Show(true, 2);
        StartCoroutine(ShowMessageAfter(0.5f));
    }

    IEnumerator ShowMessageAfter(float time, bool incrementDialog = false)
    {
        yield return new WaitForSeconds(time);
        dialogText.setText(texts[currentDialog]);
        dialogText.StartTextAnimation();
        if (incrementDialog)
        {
            currentDialog++;
        }
    }
}
