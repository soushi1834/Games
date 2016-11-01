using UnityEngine;
using System.Collections;

public class GoGamePlay : MonoBehaviour {

    private SelectState state;
    private int number;

    public AudioClip clip;
    private AudioSource source;

    private Data data;

	// Use this for initialization
	void Start () {
        state = this.transform.parent.GetComponent<SelectState>();
        this.gameObject.SetActive(false);

        source = this.transform.parent.GetComponent<AudioSource>();

        data = GameObject.Find("GetPlayers").GetComponent<Data>();
    }
	
	// Update is called once per frame
	void Update () {
        if (data.Players == 1)
        {
            if (Input.GetKeyDown(KeyCode.X) ||
            Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                source.PlayOneShot(clip);
                System.Threading.Thread.Sleep(250); //ミリ秒
                Application.LoadLevel("GamePlay");
                this.gameObject.SetActive(false);
            }
        }
        if (data.Players == 2)
        {
            if (Input.GetKeyDown(KeyCode.X) ||
            Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                source.PlayOneShot(clip);
                System.Threading.Thread.Sleep(250); //ミリ秒
                Application.LoadLevel("GamePlay2P");
                this.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) ||
            Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            source.PlayOneShot(clip);

            state.next(SelectState.SELECTSTATE.SELECT_COURSE);
            this.gameObject.SetActive(false);
        }
	}
}
