using UnityEngine;
using System.Collections;

public class ScreenSize : MonoBehaviour {
    public static Vector2 aspect;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        aspect = new Vector2(Screen.width * 9 / (float)Screen.height, 9);
        //Debug.Log(aspect.x + " : " + aspect.y);
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevelName == "Title")
        {
            Destroy(this.gameObject);
        }
	}
}
