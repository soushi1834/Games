using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

    public int Lap; //ラップ数の保持
    public int endLap;  //規定周回数

    private Rect lapLabelRect = new Rect(10, 10, 400, 300);
    private GUIStyle lapLabelStyle;

	// Use this for initialization
	void Start () {
        lapLabelStyle = new GUIStyle();
        lapLabelStyle.fontSize = 30;
        lapLabelStyle.normal.textColor = Color.white;

        Lap = 0;
	}

    public void setLap(int l)
    {
        endLap = l;
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void Destroy(string p)
    {
        throw new System.NotImplementedException();
    }

     void OnGUI()
    {
        GUI.Label(lapLabelRect, "Lap:" + Mathf.Max(Mathf.Min(Lap, endLap), 1), lapLabelStyle);
    }
}