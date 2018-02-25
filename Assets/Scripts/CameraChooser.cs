using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChooser : MonoBehaviour
{

    public GameObject perspective;
    public GameObject ortho;

    // Use this for initialization
    void Start()
    {

    }

    bool ThreeD = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ThreeD = !ThreeD;
            if (ThreeD)
            {
                perspective.SetActive(true);
                ortho.SetActive(false);
            }
            else
            {
                ortho.SetActive(true);
                perspective.SetActive(false);
            }
        }


    }

}
