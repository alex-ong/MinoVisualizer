using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTextureStartup : MonoBehaviour {

    public GameObject parent;

    // Use this for initialization
    public IEnumerator Start()
    {
        yield return StartCoroutine(LoadTextures());
        Debug.Log("enabling parent");
        parent.SetActive(true);
    }

    public MinoRenderer mr;

    public IEnumerator LoadTexture(Material m, string assetName)
    {
        string path = Application.streamingAssetsPath +"/"+ assetName;
        Debug.Log("loading..." + path);
        WWW www = new WWW(path);
        yield return www;
        m.mainTexture = www.texture;
    }
    public IEnumerator LoadTextures()
    {
        yield return StartCoroutine(LoadTexture(mr.Empty, "empty.png"));
        yield return StartCoroutine(LoadTexture(mr.Garbage, "Garbage.png"));
        yield return StartCoroutine(LoadTexture(mr.Solid, "solidGarbage.png"));
        yield return StartCoroutine(LoadTexture(mr.Color, "block.png"));
        Debug.Log("Finished loading textures");
    }
}
