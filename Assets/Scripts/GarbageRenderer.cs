using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageRenderer : MonoBehaviour {
    public GameObject tick;
    // Use this for initialization
    List<GameObject> ticks = new List<GameObject>();
	void Start () {
        for (int i = 0; i < 21; i++)
        {
            GameObject go = GameObject.Instantiate(tick);
            ticks.Add(go);
            //go.SetActive(false);
            go.transform.SetParent(this.transform);
            Vector3 pos = Vector3.zero;
            pos.y = i;
            go.transform.localPosition = pos;
        }
	}
    public void SetGarbage(int garbage)
    {
        for (int i = 0; i < ticks.Count; i++)
        {
            if (i < garbage)
            {
                ticks[i].SetActive(true);
            }
            else
            {
                ticks[i].SetActive(false);
            }
        }
    }
	
}
