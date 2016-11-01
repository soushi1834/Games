using UnityEngine;
using System.Collections;

public class TimeUI : MonoBehaviour {

    private static float time;
    public static float Time
    {
        set { time = value; }
    }

    private Texture[] textures;
    private GameObject[] timeobj;

	// Use this for initialization
	void Start () {
        time = 0;
        if (Application.loadedLevelName != "GamePlay")
        {
            textures = new Texture[10];
            textures[0] = Resources.Load("Texture/タイムその他/0", typeof(Texture)) as Texture;
            textures[1] = Resources.Load("Texture/タイムその他/1", typeof(Texture)) as Texture;
            textures[2] = Resources.Load("Texture/タイムその他/2", typeof(Texture)) as Texture;
            textures[3] = Resources.Load("Texture/タイムその他/3", typeof(Texture)) as Texture;
            textures[4] = Resources.Load("Texture/タイムその他/4", typeof(Texture)) as Texture;
            textures[5] = Resources.Load("Texture/タイムその他/5", typeof(Texture)) as Texture;
            textures[6] = Resources.Load("Texture/タイムその他/6", typeof(Texture)) as Texture;
            textures[7] = Resources.Load("Texture/タイムその他/7", typeof(Texture)) as Texture;
            textures[8] = Resources.Load("Texture/タイムその他/8", typeof(Texture)) as Texture;
            textures[9] = Resources.Load("Texture/タイムその他/9", typeof(Texture)) as Texture;

            timeobj = new GameObject[6];
            timeobj[0] = transform.FindChild("X000").gameObject;
            timeobj[1] = transform.FindChild("X00").gameObject;
            timeobj[2] = transform.FindChild("X0").gameObject;
            timeobj[3] = transform.FindChild("X").gameObject;
            timeobj[4] = transform.FindChild("0.X").gameObject;
            timeobj[5] = transform.FindChild("0.0X").gameObject;
        }
        
        if (Application.loadedLevelName == "GamePlay")
        {
            guiText.text = (time % 60).ToString() + "_" + ((int)time).ToString() + "_" + ((int)(time - (int)time) * 100).ToString("00");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevelName == "GamePlay")
        {
            guiText.text = ((int)time / 60).ToString() + "_" + ((int)time % 60).ToString("00") + "_" + ((int)((time - (int)time) * 100)).ToString("00");
        }
        else
        {
            timeobj[0].renderer.material.mainTexture = textures[(int)(time / 600) % 6];
            timeobj[1].renderer.material.mainTexture = textures[(int)(time / 60) % 10];
            timeobj[2].renderer.material.mainTexture = textures[(int)(time / 10) % 6];
            timeobj[3].renderer.material.mainTexture = textures[(int)time % 10];
            timeobj[4].renderer.material.mainTexture = textures[(int)((time - (int)time) * 10)];
            timeobj[5].renderer.material.mainTexture = textures[(int)((time - (int)time) * 100) % 10];
        }
	}
}
