using UnityEngine;
using System.Collections;

public class CountDownTimer : MonoBehaviour {

    private float time_;
    //private GUIStyle style;
    //public GUIStyleState GUIstate;
    private bool isdraw = false;

    private Timer timer;
    private bool call;

    private GameObject drawObject;
    private Texture[] textures;

    // Use this for initialization
    void Start()
    {
        time_ = 5;
        //style = new GUIStyle();
        //style.fontSize = 100;

        timer = this.GetComponent<Timer>();
        call = false;

        drawObject = GameObject.Find("CountDown");
        textures = new Texture[5];
        textures[0] = Resources.Load("Texture/その他/スタートシグナル/cd0", typeof(Texture)) as Texture;
        textures[1] = Resources.Load("Texture/その他/スタートシグナル/sg1", typeof(Texture)) as Texture;
        textures[2] = Resources.Load("Texture/その他/スタートシグナル/sg2", typeof(Texture)) as Texture;
        textures[3] = Resources.Load("Texture/その他/スタートシグナル/sg3", typeof(Texture)) as Texture;
        textures[4] = Resources.Load("Texture/その他/スタートシグナル/sg4", typeof(Texture)) as Texture;
    }

    // Update is called once per frame
    void Update()
    {

        time_ -= Time.deltaTime;

        draw();

        if (!call && time_ <= 1)
        {
            isdraw = true;
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < obj.Length; i++)
            {
                Driver driver = obj[i].GetComponentInChildren<Driver>();
                driver.setStartFrag(true);
            }
            obj = GameObject.FindGameObjectsWithTag("CPU");
            for (int i = 0; i < obj.Length; i++)
            {
                Driver driver = obj[i].GetComponentInChildren<Driver>();
                driver.setStartFrag(true);
            }
            timer.startTime();
            call = true;
        }
    }

    void draw()
    {
        //カウントダウン
        if (!isdraw)
        {
            drawObject.guiTexture.texture = textures[Mathf.Max(Mathf.Min((int)time_, textures.Length - 1), 0)];
        }
        //GO!の表示
        if (isdraw && time_ > 0)
        {
            drawObject.guiTexture.texture = textures[0];
        }
        if (time_ <= 0)
        {
            drawObject.SetActive(false);
        }
    }

    /*
    void OnGUI()
    {
        //カウントダウン
        if (!isdraw)
        {
            Rect rect = new Rect(Screen.width / 2 - 40, Screen.height / 2 - 50, 100, 30);
            style.fontStyle = FontStyle.Bold;
            GUIstate.textColor = new Color(255, 255, 0, 255);
            style.normal = GUIstate;
            GUI.Label(rect, "" + (int)time_, style);
        }
        //GO!の表示
        if (isdraw && time_ > 0)
        {
            Rect rect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 100, 30);
            GUI.Label(rect, "GO!", style);
        }
    }
    */
}
