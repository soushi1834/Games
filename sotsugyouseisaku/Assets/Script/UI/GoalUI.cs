using UnityEngine;
using System.Collections;

public class GoalUI : MonoBehaviour {

    private Texture goalTex;
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
            goalTex = Resources.Load("Texture/Goal!!", typeof(Texture)) as Texture;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            if (Application.loadedLevelName == "GamePlay")
            {
                guiTexture.texture = goalTex;
            }
            else
            {
                transform.FindChild("Goal").gameObject.SetActive(true);
            }
        }
    }
}
