using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoRenderer : MonoBehaviour {
    public Material Color;    
    public Material Garbage;
    public Material Solid;
    public Material Empty;

    public void SetMinoColor(MinoColor mt)
    {
        Renderer r = this.GetComponent<Renderer>();
        switch (mt.type)
        {
            case MinoType.Color:
                r.material = Color;
                r.material.color = mt.c;
                break;
            case MinoType.Garbage: r.sharedMaterial = Garbage; break;
            case MinoType.Solid: r.sharedMaterial = Solid; break;
            case MinoType.Empty: r.sharedMaterial = Empty; break;
        }    
        
        //Set the mino width. We can get sick 3d effect with empty field.
        SetWidth(mt.type);
    }

    private void SetWidth(MinoType t)
    {
        if (t == MinoType.Empty)
        {
            Vector3 scale = this.transform.localScale;
            scale.z = 0.01f;

            Vector3 pos = this.transform.localPosition;
            pos.z = -0.5f;
        }
        else
        {
            this.transform.localScale = Vector3.one;
            Vector3 pos = this.transform.localPosition;
            pos.z = 0.0f;
        }
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
