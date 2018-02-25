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
        if (r.material != null)
        {
            Destroy(r.material);
            r.material = null;
        }

        switch (mt.type)
        {
            case MinoType.Color:
                r.material = Color;
                r.material.color = mt.c;
                break;
            case MinoType.Garbage: r.material = Garbage; break;
            case MinoType.Solid: r.material = Solid; break;
            case MinoType.Empty: r.material = Empty; break;
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
            pos.z = 1.0f;
            this.transform.localScale = scale;
            this.transform.localPosition = pos;
        }
        else
        {
            this.transform.localScale = Vector3.one;
            Vector3 pos = this.transform.localPosition;
            pos.z = 0.0f;
            this.transform.localPosition = pos;
        }
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
