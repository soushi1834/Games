using UnityEngine;
using System.Collections;

public class TiTleSelect : MonoBehaviour {

    static string[] SelectMode =
    {
        "1P",
        "2P",
        "Lecture",
        "Credit"
    };

    private Data data;
    private int column;
    private bool trigger;

    public AudioClip clip;
    private AudioSource source;

	// Use this for initialization
	void Start () {
        data = GameObject.Find("GetPlayers").GetComponent<Data>();
        column = 0;
        trigger = false;

        source = GameObject.Find("BGM").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        select();

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) <= 0.2f)
        {
            trigger = false;
        }

        if (Input.GetKeyDown(KeyCode.X) ||
            Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            source.PlayOneShot(clip);
            string mode = SelectMode[column];
            System.Threading.Thread.Sleep(250); //ミリ秒
            if (mode == "1P")
            {
                data.Players = 1;
                Application.LoadLevel("Select");
            }
            if (mode == "2P")
            {
                data.Players = 2;
                Application.LoadLevel("Select2P");
            }
            if (mode == "Lecture")
            {
                Application.LoadLevel("Lecture");
            }
            if (mode == "Credit")
            {
                Application.LoadLevel("Credit");
            }
        }
	}

    void select()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) ||
            (Input.GetAxisRaw("Vertical1P") <= -1 && !trigger))
        {
            column--;
            trigger = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) ||
            (Input.GetAxisRaw("Vertical1P") >= 1 && !trigger))
        {
            column++;
            trigger = true;
        }
        column = Mathf.Max(0, (Mathf.Min(column, 3)));
        transform.position = new Vector3(0.55f, 0.265f - 0.068f * column, 1);
    }
}
