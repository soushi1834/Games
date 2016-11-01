using UnityEngine;
using System.Collections;

public class Quit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape) ||
            (Input.GetKey(KeyCode.JoystickButton8) && Input.GetKey(KeyCode.JoystickButton9)))
        {
            Application.Quit();
        }

        if (GameObject.Find("Quit") != this.gameObject)
        {
            Destroy(this.gameObject);
        }
	}
}
