using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    private bool StartTime; //タイムスタートフラグ
    private bool EndTime; //タイムエンドフラグ

    public static float[] LapTime;  //ラップタイム
    private int endLap;
    public int nowLap;

    public static float AllTime = 0;
    
    public float StopTime = 0;

    private Rect timeLabelRect = new Rect(850, 10, 400, 300);
    private GUIStyle timeLabelStyle;
    private GUIStyle laptimeLabelStyle;
    private GUIStyle lapLabelStyle;

    private Rect lapLabelRect = new Rect(10, 10, 400, 300);

    CourseData data;
    PlayCourseBGM bgm;

    public static float getAllTime()
    {
        return AllTime;
    }
    public static float getLapTime(int lap)
    {
        return LapTime[lap];
    }

	// Use this for initialization
	void Start () {
        data = GameObject.FindWithTag("course").GetComponent<CourseData>();
        bgm = GameObject.Find("BGM").GetComponent<PlayCourseBGM>();

        this.StartTime = false;
        this.EndTime = false;

        AllTime = 0;

        endLap = data.getEndLap();
        LapTime = new float[endLap];
        nowLap = 0;

        timeLabelStyle = new GUIStyle();
        timeLabelStyle.fontSize = 45;
        timeLabelStyle.normal.textColor = new Color(200, 200, 200, 255);

        lapLabelStyle = new GUIStyle();
        lapLabelStyle.fontSize = 50;
        lapLabelStyle.normal.textColor = new Color(200, 200, 200, 255);

        laptimeLabelStyle = new GUIStyle();
        laptimeLabelStyle.fontSize = 25;
        laptimeLabelStyle.normal.textColor = new Color(200, 200, 200, 255);
	}
	
	// Update is called once per frame
	void Update () {
        if (StartTime == true)
        {
            AllTime += Time.deltaTime;
            if (nowLap >= 1)
                LapTime[nowLap - 1] += Time.deltaTime;
        }

        if (EndTime == true)
        {
            StopTime += Time.deltaTime;
        }
	}

    //タイムスタート&ラップ1スタート
    public void startTime()
    {
        StartTime = true;
        nowLap = 1;
    }

    //トリガー処理
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "CPU")
        {
            Driver driver = other.GetComponentInChildren<Driver>();
            if (driver.getLap() >= endLap)
            {
                if (driver.nextLap(true))
                {
                    if (other.tag == "Player")
                    {
                        bgm.GoalBGM();
                    }
                }
            }
            else
            {
                driver.nextLap(false);
                if (driver.transform.parent.tag == "Player")
                {
                    nowLap = driver.getLap();
                }
            }
        }
    }

    /*
    void OnGUI()
    {
        GUI.Label(lapLabelRect, "Lap:" + Mathf.Max(Mathf.Min(nowLap, endLap), 1), lapLabelStyle);

        GUI.Label(timeLabelRect, "Timer:\n" + AllTime.ToString("f3"), timeLabelStyle);
        for (int i = 0; i < endLap; i++)
        {
            GUI.Label(new Rect(850, 100 + i * 60, 400, 300), "Lap[" + (i + 1) + "] :\n" + LapTime[i].ToString("f3"), laptimeLabelStyle);
        }
    }
     * */
}
