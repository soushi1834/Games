using UnityEngine;
using System.Collections;

public class EngineGage : MonoBehaviour {
    Texture[] texture;
    private static float hp;
    public static float HP
    {
        set { hp = value; }
    }

    // Use this for initialization
    void Start()
    {
        texture = new Texture[2];
        texture[0] = Resources.Load("Texture/マシンインターフェース/PartsHP", typeof(Texture)) as Texture;
        texture[1] = Resources.Load("Texture/マシンインターフェース/PartsDamage", typeof(Texture)) as Texture;

        hp = 1;

        if (Application.loadedLevelName == "GamePlay")
        {
            guiTexture.texture = texture[0];
        }
        else
        {
            renderer.material.mainTexture = texture[0];
        }
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
        if (hp > 1 / 3f)
        {
            guiTexture.texture = texture[0];
        }
        else
        {
            guiTexture.texture = texture[1];
        }

        transform.localScale = new Vector3(
                0.05f + 0.3f * Mathf.Max(hp, 0),
                0.16f,
                transform.localScale.z);

        transform.position = new Vector3(
            (0.05f + 0.08f * Mathf.Max(hp, 0)) * transform.parent.localScale.x,
            transform.position.y,
            transform.position.z);
    }

    void Texture()
    {
        if (hp > 1 / 3f)
        {
            renderer.material.mainTexture = texture[0];
        }
        else
        {
            renderer.material.mainTexture = texture[1];
        }

        transform.localScale = new Vector3(
                0.47f * Mathf.Max(hp, 0),
                transform.localScale.y,
                transform.localScale.z);

        transform.localPosition = new Vector3(
                493.95f + 1.12f * Mathf.Max(hp, 0),
                transform.localPosition.y,
                transform.localPosition.z);
    }
}
