using UnityEngine;
using System.Collections;

public class LapCount2P : MonoBehaviour {

    Texture[] texture;

    void Start()
    {
        texture = new Texture[5];
        texture[0] = Resources.Load("Texture/ラップ/1", typeof(Texture)) as Texture;
        texture[1] = Resources.Load("Texture/ラップ/2", typeof(Texture)) as Texture;
        texture[2] = Resources.Load("Texture/ラップ/3", typeof(Texture)) as Texture;
        texture[3] = Resources.Load("Texture/ラップ/4", typeof(Texture)) as Texture;
        texture[4] = Resources.Load("Texture/ラップ/5", typeof(Texture)) as Texture;
    }

    private static int lapCount = 1;
    public static int NowLap
    {
        set
        {
            lapCount = value;
            //instance.gameObject.guiText.text = lapCount.ToString();
        }
    }

    void Update()
    {
        if (Application.loadedLevelName == "GamePlay")
        {
            guiText.text = lapCount.ToString();
        }
        else
        {
            renderer.material.mainTexture = texture[lapCount - 1];
        }
    }
}
