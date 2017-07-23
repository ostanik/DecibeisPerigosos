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

	// Update is called once per frame
	void Update () {
		
	}
}
