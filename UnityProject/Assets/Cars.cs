using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cars : MonoBehaviour {
    private Vector3 initialPosition;
    public bool rightDirection;

    // Use this for initialization
    void Start () {
        initialPosition = gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        var pos = gameObject.transform.position;
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(pos);
        bool onScreen = screenPoint.x > -100 && screenPoint.x < 150;
        if (onScreen == false)
        {
            gameObject.transform.position = initialPosition;
            pos = initialPosition;
        }
        //print(screenPoint.x);
        
        var x = rightDirection ? pos.x + 20 : pos.x - 20;
        gameObject.transform.position = new Vector3(x, pos.y, pos.z);

    }

    void OnBecameInvisible()
    {
       
    }
}
