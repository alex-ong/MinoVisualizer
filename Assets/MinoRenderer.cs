using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoRenderer : MonoBehaviour {
    public Material L;
    public Material O;
    public Material S;
    public Material T;
    public Material J;
    public Material I;
    public Material Z;
    public Material Garbage;
    public Material Solid;
    public Material Empty;

    public void SetMinoType(MinoType mt)
    {
        Renderer r = this.GetComponent<Renderer>();
        switch (mt)
        {
            case MinoType.L: r.sharedMaterial = L; break;
            case MinoType.O: r.sharedMaterial = O; break;
            case MinoType.S: r.sharedMaterial = S; break;
            case MinoType.T: r.sharedMaterial = T; break;
            case MinoType.J: r.sharedMaterial = J; break;
            case MinoType.I: r.sharedMaterial = I; break;
            case MinoType.Z: r.sharedMaterial = Z; break;
            case MinoType.Garbage: r.sharedMaterial = Garbage; break;
            case MinoType.Solid: r.sharedMaterial = Solid; break;
            case MinoType.Empty: r.sharedMaterial = Empty; break;
        }    
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
