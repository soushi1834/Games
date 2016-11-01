using UnityEngine;
using System.Collections;

public class Data: MonoBehaviour {

    private int players;
    public int Players
    {
        set { players = value; }
        get { return players; }
    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevelName == "Lecture" ||
            Application.loadedLevelName == "Credit")
        {
            Destroy(this.gameObject);
        }
	}
}
