using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour {

    public string playerName;
    public int countCorrect;
    public int countError;
    public AnswerData[] responseData;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MenuScreen");
	}

    public void Restart()
    {
        playerName = "";
        countCorrect = 0;
        countError = 0;

        foreach (var response in responseData)
        {
            response.isCorrect = false;
        }
    }
}
