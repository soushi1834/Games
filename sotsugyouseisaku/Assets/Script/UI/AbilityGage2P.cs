using UnityEngine;
using System.Collections;

public class AbilityGage2P : MonoBehaviour
{

    Texture[] texture;
    private static float power;
    public static float Power
    {
        set { power = value; }
    }
    private int mode;
    private static bool abilityLock;
    public static bool AbilityLock
    {
        set { abilityLock = value; }
    }
    private static bool broken;
    public static bool Broken
    {
        set { broken = value; }
    }

    // Use this for initialization
    void Start()
    {
        texture = new Texture[4];
        texture[0] = Resources.Load("Texture/マシンインターフェース/AbilityBlank", typeof(Texture)) as Texture;
        texture[1] = Resources.Load("Texture/マシンインターフェース/AbilityCharge", typeof(Texture)) as Texture;
        texture[2] = Resources.Load("Texture/マシンインターフェース/AbilityHack", typeof(Texture)) as Texture;
        texture[3] = Resources.Load("Texture/マシンインターフェース/AbilityBroken", typeof(Texture)) as Texture;

        power = 0;

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
        guiTexture.texture = texture[mode];
        if (broken)
        {
            mode = 3;
        }
        else if (abilityLock)
        {
            mode = 2;
        }
        else if (power >= 1)
        {
            mode = 1;
        }
        else
        {
            mode = 0;
        }

        if (mode == 0)
        {
            transform.localScale = new Vector3(
                0.41f * power,
                transform.localScale.y,
                transform.localScale.z);

            transform.position = new Vector3(
                (0.095f + 0.105f * power) * transform.parent.localScale.x,
                transform.position.y,
                transform.position.z);
        }
        else
        {
            transform.localScale = new Vector3(
                0.41f,
                transform.localScale.y,
                transform.localScale.z);

            transform.position = new Vector3(
                0.2f * transform.parent.localScale.x,
                transform.position.y,
                transform.position.z);
        }
    }

    void Texture()
    {
        renderer.material.mainTexture = texture[mode];
        if (broken)
        {
            mode = 3;
        }
        else if (abilityLock)
        {
            mode = 2;
        }
        else if (power >= 1)
        {
            mode = 1;
        }
        else
        {
            mode = 0;
        }

        if (mode == 0)
        {
            transform.localScale = new Vector3(
                0.72f * Mathf.Max(power, 0),
                transform.localScale.y,
                transform.localScale.z);

            transform.localPosition = new Vector3(
                494.4f + 1.9f * Mathf.Max(power, 0),
                transform.localPosition.y,
                transform.localPosition.z);
        }
        else
        {
            transform.localScale = new Vector3(
                0.72f,
                transform.localScale.y,
                transform.localScale.z);
            //496.3 495.38
            transform.localPosition = new Vector3(
                494.4f + 1.9f,
                transform.localPosition.y,
                transform.localPosition.z);
        }
    }
}
