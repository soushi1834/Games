using UnityEngine;
using System.Collections;

public class ItemTxture2P : MonoBehaviour {
    Texture[] texture;
    private static string itemName;
    public static string ItemName
    {
        set { itemName = value; }
    }

    // Use this for initialization
    void Start()
    {
        texture = new Texture[7];
        texture[0] = Resources.Load("Texture/マシンインターフェース/OSアップデーター2", typeof(Texture)) as Texture;
        texture[1] = Resources.Load("Texture/マシンインターフェース/バスターアイコン", typeof(Texture)) as Texture;
        texture[2] = Resources.Load("Texture/マシンインターフェース/ブーストエネルギー", typeof(Texture)) as Texture;
        texture[3] = Resources.Load("Texture/マシンインターフェース/マキビシ", typeof(Texture)) as Texture;
        texture[4] = Resources.Load("Texture/マシンインターフェース/ロック", typeof(Texture)) as Texture;
        texture[5] = Resources.Load("Texture/マシンインターフェース/リペアBOX", typeof(Texture)) as Texture;
        texture[6] = Resources.Load("Texture/マシンインターフェース/リフレクション", typeof(Texture)) as Texture;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName == "GamePlay")
        {
            GUI();
        }
        else
        {
            Texture();
        }
    }

    void GUI()
    {
        if (itemName == null)
        {
            guiTexture.texture = null;
            return;
        }
        if (itemName == "Dash")
        {
            guiTexture.texture = texture[2];
        }

        if (itemName == "Beam")
        {
            guiTexture.texture = texture[1];
        }

        if (itemName == "Makibishi")
        {
            guiTexture.texture = texture[3];
        }

        if (itemName == "StateUP")
        {
            guiTexture.texture = texture[0];
        }

        if (itemName == "Barrier")
        {
            guiTexture.texture = texture[6];
        }

        if (itemName == "AbilityHack")
        {
            guiTexture.texture = texture[4];
        }

        if (itemName == "RepairItem")
        {
            guiTexture.texture = texture[5];
        }
    }

    void Texture()
    {
        if (itemName == null)
        {
            renderer.material.mainTexture = null;
            return;
        }
        if (itemName == "Dash")
        {
            renderer.material.mainTexture = texture[2];
        }

        if (itemName == "Beam")
        {
            renderer.material.mainTexture = texture[1];
        }

        if (itemName == "Makibishi")
        {
            renderer.material.mainTexture = texture[3];
        }

        if (itemName == "StateUP")
        {
            renderer.material.mainTexture = texture[0];
        }

        if (itemName == "Barrier")
        {
            renderer.material.mainTexture = texture[6];
        }

        if (itemName == "AbilityHack")
        {
            renderer.material.mainTexture = texture[4];
        }

        if (itemName == "RepairItem")
        {
            renderer.material.mainTexture = texture[5];
        }
    }
}
