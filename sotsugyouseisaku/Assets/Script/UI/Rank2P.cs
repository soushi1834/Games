using UnityEngine;
using System.Collections;

public class Rank2P : MonoBehaviour
{
    Texture[] texture;
    private static int rank;
    public static int mRank2P
    {
        set { rank = value; }
    }

    // Use this for initialization
    void Start()
    {
        texture = new Texture[7];
        //texture[0] = Resources.Load("Texture/順位数字/Rank small", typeof(Texture)) as Texture;
        texture[0] = Resources.Load("Texture/順位数字/1st", typeof(Texture)) as Texture;
        texture[1] = Resources.Load("Texture/順位数字/2nd", typeof(Texture)) as Texture;
        texture[2] = Resources.Load("Texture/順位数字/3rd", typeof(Texture)) as Texture;
        texture[3] = Resources.Load("Texture/順位数字/4th", typeof(Texture)) as Texture;
        texture[4] = Resources.Load("Texture/順位数字/5th", typeof(Texture)) as Texture;
        texture[5] = Resources.Load("Texture/順位数字/6th", typeof(Texture)) as Texture;
        texture[6] = Resources.Load("Texture/順位数字/7th", typeof(Texture)) as Texture;
        //guiTexture.texture = texture[0];
        renderer.material.mainTexture = texture[0];
    }

    // Update is called once per frame
    void Update()
    {
        //print("Rank:" + rank);

        if (rank <= 0)
        {
            return;
        }

        //guiTexture.texture = texture[rank -1]; //ここが問題
        renderer.material.mainTexture = texture[rank - 1];
    }
}
