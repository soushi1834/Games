using UnityEngine;
using System.Collections;

public class RetireUI2P : MonoBehaviour {

    private Texture retireTex;
    private static bool flag;
    public static bool Flag
    {
        set { flag = value; }
    }

    // Use this for initialization
    void Start()
    {
        flag = false;
        if (Application.loadedLevelName == "GamePlay")
        {
            guiTexture.texture = null;
            retireTex = Resources.Load("Texture/Retire_sample1", typeof(Texture)) as Texture;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            if (Application.loadedLevelName == "GamePlay")
            {
                guiTexture.texture = retireTex;
            }
            else
            {
                transform.FindChild("Retire").gameObject.SetActive(true);
            }
        }
    }
}
