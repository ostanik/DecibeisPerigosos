using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScreenController: MonoBehaviour {

    private DialogBox dialogBox;
    private QuestionBox questionBox;
    private int currentGame = 0;
    public GameObject[] gameObjects;
    public AudioSource backgroundAudio;

    // Use this for initialization
    void Start () {
        dialogBox = FindObjectOfType<DialogBox>();
        questionBox = FindObjectOfType<QuestionBox>();
        backgroundAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadNextGame()
    {
        dialogBox.clearText();
        currentGame++;

        if (currentGame == gameObjects.Length)
        {
            StartCoroutine(ShowEndGame());
        }
        else
        {
            gameObjects[currentGame].GetComponent<ExhibitionController>().Hide(true, 2);
            dialogBox.GetComponent<ExhibitionController>().Hide();
            questionBox.GetComponent<ExhibitionController>().Hide();

            StartCoroutine(DestroyPreviewsGame());
            StartCoroutine(ShowNextGame());
        }
    }

    IEnumerator ShowEndGame()
    {
        float fadeTime = GameObject.Find("Fade_Object").GetComponent<Fade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("EndGame");
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