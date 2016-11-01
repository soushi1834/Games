using UnityEngine;
using System.Collections;

public class SelectCourse : MonoBehaviour
{
    static string[] courseName = 
    {
        //"SportsCircuit",
        "Circuit1",
        //"第2コース 決定"
        "Circuit2",
        "Circuit3",
        "Circuit4",
        "Circuit5"
    };

    private SelectState state;
    private int number;

    public AudioClip clip;
    private AudioSource source;

    private bool triggerX;

	// Use this for initialization
	void Start () {
        state = this.transform.parent.GetComponent<SelectState>();
        this.gameObject.SetActive(false);
        number = 0;

        source = this.transform.parent.GetComponent<AudioSource>();
        triggerX = false;
    }
	
	// Update is called once per frame
    void Update()
    {
        end();
        select();

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 0)
        {
            triggerX = false;
        }
    }

    void end()
    {
        if (Input.GetKeyDown(KeyCode.X) ||
            Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            source.PlayOneShot(clip);

            state.setCourse(courseName[number]);
            state.next(SelectState.SELECTSTATE.SELECT_END);
            this.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Z) ||
            Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            source.PlayOneShot(clip);

            state.setMachines(null, state.getPlayers() - 1);
            state.next(SelectState.SELECTSTATE.SELECT_MACHINES);
            this.gameObject.SetActive(false);
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
        number = Mathf.Max(0, (Mathf.Min(number, courseName.Length - 1)));
        this.transform.position = new Vector3(number * -1, 0, 0);
    }
}
