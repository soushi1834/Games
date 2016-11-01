using UnityEngine;
using System.Collections;

public class LectureScene : MonoBehaviour {

    private int number;
    private bool triggerX;
    public AudioClip clip;
    private AudioSource source;

	// Use this for initialization
	void Start () {
        number = 0;
        triggerX = false;
        source = this.transform.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        select();
        end();

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 0)
        {
            triggerX = false;
        }
	}

    void select()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) ||
            (Input.GetAxisRaw("Horizontal") >= 1 && !triggerX))
        {
            number++;
            triggerX = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) ||
            (Input.GetAxisRaw("Horizontal") <= -1 && !triggerX))
        {
            number--;
            triggerX = true;
        }
        number = Mathf.Max(0, (Mathf.Min(number, 1)));
        this.transform.position = new Vector3(number * -1, 0, 0);
    }

    void end()
    {
        if (Input.GetKeyDown(KeyCode.Z) ||
            Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            source.PlayOneShot(clip);
            System.Threading.Thread.Sleep(250); //ミリ秒
            Application.LoadLevel("Title");
        }
    }
}
