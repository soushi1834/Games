using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    GameObject drivers;
    Texture texture;

	// Use this for initialization
	void Start () {
        drivers = GameObject.Find("Drivers");
        texture = (Texture)Resources.Load("Texture/PAUSE");
        guiTexture.texture = null;
	}
	
	// Update is called once per frame
	void Update () {
        pause();

        if (Input.GetKeyDown(KeyCode.B) ||
            Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            if (Time.timeScale == 0)
            {
                Destroy(GameObject.Find("ResultData"));
                Time.timeScale = 1;
                Application.LoadLevel("Title");
            }
        }
	}

    void pause()
    {
        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.JoystickButton8))
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
                guiTexture.texture = texture;
            }
            else
            {
                Time.timeScale = 1;
                guiTexture.texture = null;
            }
        }
    }
}
