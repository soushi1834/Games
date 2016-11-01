using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultScript : MonoBehaviour {

    private Ranking ranking;
    private Dictionary<int, string> drivernames;
    private Dictionary<string, string> driversMachine;
    private Dictionary<string, float> driversTime;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName == "Result")
        {
            if (Input.GetKeyDown(KeyCode.X) ||
                Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                Application.LoadLevel("Title");
                Destroy(this.gameObject);
            }
        }
	}

    public void setRanking(Dictionary<int, string> driversListResult)
    {
        drivernames = new Dictionary<int, string>(driversListResult);
    }

    public void driversData(Dictionary<string, string> driversMachine, Dictionary<string, float> driversTime)
    {
        this.driversMachine = new Dictionary<string, string>(driversMachine);
        this.driversTime = new Dictionary<string, float>(driversTime);
    }

    void OnGUI()
    {

        GUIStyle LabelStyle = new GUIStyle();
        LabelStyle.fontSize = 32 * Screen.width / 800;
        LabelStyle.normal.textColor = new Color(255, 255, 255, 255);
        

        if (Application.loadedLevelName == "Result")
        {
            
            //GUI.Label(new Rect(Screen.width / 3, Screen.height / 10, 1000, 1000), Application.loadedLevelName, LabelStyle);
            foreach(var key in drivernames.Keys)
            {
                GUI.Label(new Rect(Screen.width / 10, Screen.height * (25 + key * 8) / 100, 1000, 1000), key.ToString(), LabelStyle);
                GUI.Label(new Rect(Screen.width / 10 * 1.8f, Screen.height * (25 + key * 8) / 100, 1000, 1000), drivernames[key].ToString(), LabelStyle);
                GUI.Label(new Rect(Screen.width / 10 * 3.6f, Screen.height * (25 + key * 8) / 100, 1000, 1000), driversMachine[drivernames[key]], LabelStyle);
                if (driversTime[drivernames[key]] != 0)
                {
                    GUI.Label(new Rect(Screen.width / 10 * 6.4f, Screen.height * (25 + key * 8) / 100, 1000, 1000),
                         ((int)driversTime[drivernames[key]] / 60).ToString() + ":" + ((int)driversTime[drivernames[key]] % 60).ToString("00") + ":" + ((int)((driversTime[drivernames[key]] - (int)driversTime[drivernames[key]]) * 100)).ToString("00"), LabelStyle);
                }
                else
                {
                    GUI.Label(new Rect(Screen.width / 10 * 6.4f, Screen.height * (25 + key * 8) / 100, 1000, 1000), "---:---:---", LabelStyle);
                }
            }
        }
    }
}
