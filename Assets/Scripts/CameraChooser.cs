using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChooser : MonoBehaviour {

    public GameObject perspective;
    public GameObject ortho;

	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            perspective.SetActive(true);
            ortho.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ortho.SetActive(true);
            perspective.SetActive(false);
        }
	}
}
