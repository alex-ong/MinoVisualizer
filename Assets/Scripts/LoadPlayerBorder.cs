using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadPlayerBorder : MonoBehaviour {

    public string assetName;
    public IEnumerator LoadTexture(Material m, string assetName)
    {
        string path = Path.Combine("file:///" + Application.streamingAssetsPath, assetName);
        Debug.Log("loading..." + path);
        WWW www = new WWW(path);
        yield return www;
        m.mainTexture = www.texture;
    }

    public IEnumerator Start()
    {
        yield return StartCoroutine(LoadTextures());
    }

    public IEnumerator LoadTextures()
    {
        Material m = this.gameObject.GetComponent<Renderer>().material;
        yield return StartCoroutine(LoadTexture(m, assetName));
    }
}


