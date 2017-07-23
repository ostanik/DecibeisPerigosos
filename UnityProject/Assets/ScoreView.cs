using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour {

    public bool isCorrect = false;
    public int currentGame;
    public Text correctText;
    public ExhibitionController exhibition;

	// Use this for initialization
	void Start () {
        exhibition = GetComponent<ExhibitionController>();
	}

    public void SetAnswer(bool correct)
    {
        isCorrect = correct;
        if (isCorrect)
        {
            correctText.text = "Correto";
        }
        else
        {
            correctText.text = "Errado";
        }
    }
}
