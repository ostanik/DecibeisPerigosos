using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScreenController : MonoBehaviour {

	public void StartGame () {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene() {
        float fadeTime = GameObject.Find("Fade_Object").GetComponent<Fade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Intro");
    }

}
