using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreenController: MonoBehaviour {

    private DialogBox dialogBox;
    private QuestionBox questionBox;
    private int currentGame = 0;
    public GameObject[] gameObjects;

    // Use this for initialization
    void Start () {
        dialogBox = FindObjectOfType<DialogBox>();
        questionBox = FindObjectOfType<QuestionBox>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void loadNextGame()
    {
        gameObjects[currentGame].GetComponent<ExhibitionController>().Hide(true, 2);
        dialogBox.GetComponent<ExhibitionController>().Hide();
        questionBox.GetComponent<ExhibitionController>().Hide();
        currentGame++;
        StartCoroutine(DestroyPreviewsGame());
    }

    IEnumerator DestroyPreviewsGame()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObjects[currentGame - 1]);
    }
}

public interface GameController
{
    void Setup();
}
