using UnityEngine;
using System.Collections;

public class SpeedUI2P : MonoBehaviour {

    private static float speed;
    public static float Speed
    {
        set { speed = value; }
    }

    private static float dash;

    private Texture[] textures;
    private GameObject[] speedobj;

    // Use this for initialization
    void Start()
    {
        speed = 0;
        dash = 0;

        if (Application.loadedLevelName != "GamePlay")
        {
            textures = new Texture[10];
            textures[0] = Resources.Load("Texture/スピード数字/スピード0", typeof(Texture)) as Texture;
            textures[1] = Resources.Load("Texture/スピード数字/スピード1", typeof(Texture)) as Texture;
            textures[2] = Resources.Load("Texture/スピード数字/スピード2", typeof(Texture)) as Texture;
            textures[3] = Resources.Load("Texture/スピード数字/スピード3", typeof(Texture)) as Texture;
            textures[4] = Resources.Load("Texture/スピード数字/スピード4", typeof(Texture)) as Texture;
            textures[5] = Resources.Load("Texture/スピード数字/スピード5", typeof(Texture)) as Texture;
            textures[6] = Resources.Load("Texture/スピード数字/スピード6", typeof(Texture)) as Texture;
            textures[7] = Resources.Load("Texture/スピード数字/スピード7", typeof(Texture)) as Texture;
            textures[8] = Resources.Load("Texture/スピード数字/スピード8", typeof(Texture)) as Texture;
            textures[9] = Resources.Load("Texture/スピード数字/スピード9", typeof(Texture)) as Texture;

            speedobj = new GameObject[3];
            speedobj[0] = transform.FindChild("X00").gameObject;
            speedobj[1] = transform.FindChild("X0").gameObject;
            speedobj[2] = transform.FindChild("X").gameObject;
        }

        if (Application.loadedLevelName == "GamePlay")
        {
            guiText.text = ((int)speed).ToString("000");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName == "GamePlay")
        {
            guiText.text = ((int)speed + dash).ToString("000");
        }
        else
        {
            speedobj[0].renderer.material.mainTexture = textures[Mathf.Abs((int)((speed + dash) / 100) % 10)];
            speedobj[1].renderer.material.mainTexture = textures[Mathf.Abs((int)((speed + dash) / 10) % 10)];
            speedobj[2].renderer.material.mainTexture = textures[Mathf.Abs((int)((speed + dash)) % 10)];
        }
        speed = 0;
        dash = 0;
    }

    public static void SpeedUp(float speed)
    {
        dash += speed / 3.6f;
    }
}
