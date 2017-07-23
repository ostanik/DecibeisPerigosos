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
    void Update()
    {

    }

    public void loadNextGame()
    {
        gameObjects[currentGame].GetComponent<ExhibitionController>().Hide(true, 2);
        dialogBox.GetComponent<ExhibitionController>().Hide();
        questionBox.GetComponent<ExhibitionController>().Hide();
        dialogBox.clearText();
        currentGame++;
        if (currentGame == gameObjects.Length)
        {

        } else {
            StartCoroutine(DestroyPreviewsGame());
            StartCoroutine(ShowNextGame());
        }
    }

    IEnumerator ShowNextGame()
    {
        yield return new WaitForSeconds(1);
        gameObjects[currentGame].GetComponent<GameController>().Setup();
    }

    IEnumerator DestroyPreviewsGame()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObjects[currentGame - 1]);
    }
}