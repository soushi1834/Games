using UnityEngine;
using System.Collections;

public class SelectMachines : MonoBehaviour {

    static string[,] machineName =
    {
        {"Creater", "Design"},
        {"Music", "IT"},
        {"Technology","Medical"},
        {"Sports", "Random"}
    };

    private SelectState state;
    private int column, row;
    private int participants;
    private int players;
    private int participantNum;

    private Texture[] textures;

    public AudioClip clip;
    private AudioSource source;
    private bool triggerX;
    private bool triggerY;

	// Use this for initialization
	void Start () {
        state = this.transform.parent.GetComponent<SelectState>();
        column = 0;
        row = 0;
        participantNum = 0;
        //this.gameObject.SetActive(false);

        textures = new Texture[2];
        textures[0] = Resources.Load("Texture/selectcursor1p", typeof(Texture)) as Texture;
        textures[1] = Resources.Load("Texture/selectcursor2p", typeof(Texture)) as Texture;
        GameObject.Find("SelectCursor").guiTexture.texture = textures[0];

        source = this.transform.parent.GetComponent<AudioSource>();
        triggerX = false;
        triggerY = false;
    }
	
	// Update is called once per frame
	void Update () {
        participants = state.getParticipants();
        players = state.getPlayers();

        GameObject.Find("SelectCursor").guiTexture.texture = textures[Mathf.Max(0, Mathf.Min(participantNum, 1))];
        decide();
        select();

        if ((Mathf.Abs(Input.GetAxisRaw("Horizontal1P")) <= 0.2f && participantNum == 0) ||
            (Mathf.Abs(Input.GetAxisRaw("Horizontal2P")) <= 0.2f && participantNum == 1))
        {
            triggerX = false;
        }
        if ((Mathf.Abs(Input.GetAxisRaw("Vertical1P")) <= 0.2f && participantNum == 0) ||
            (Mathf.Abs(Input.GetAxisRaw("Vertical2P")) <= 0.2f && participantNum == 1))
        {
            triggerY = false;
        }
	}

    void decide()
    {
        if (Input.GetKeyDown(KeyCode.X) ||
            (Input.GetKeyDown(KeyCode.Joystick1Button2) && participantNum == 0) ||
            (Input.GetKeyDown(KeyCode.Joystick2Button2) && participantNum == 1))
        {
            source.PlayOneShot(clip);

            string name = machineName[column, row];
            while (name == "Random")
            {
                name = machineName[Random.Range(0, 4), Random.Range(0, 2)];
            }
            state.setMachines(name, participantNum);

            participantNum++;
            if (participantNum >= players)
            {
                for (int i = 0; i < participants - players; i++)
                {
                    do
                    {
                        name = machineName[Random.Range(0, 4), Random.Range(0, 2)];
                        state.setMachines(name, participantNum);
                    }
                    while (name == "Random");
                    participantNum++;
                }
                participantNum = players - 1;
                state.next(SelectState.SELECTSTATE.SELECT_COURSE);
                this.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) ||
            Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            source.PlayOneShot(clip);

            participantNum--;
            if (participantNum < 0)
            {
                participantNum = 0;
                System.Threading.Thread.Sleep(250); //ミリ秒
                Application.LoadLevel("Title");
                Destroy(this.gameObject);
            }
            else
            {
                state.setMachines(null, participantNum);
            }
        }
    }

    void select()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) ||
            (Input.GetAxisRaw("Horizontal1P") >= 1 && participantNum == 0 && !triggerY) || 
            (Input.GetAxisRaw("Horizontal2P") >= 1 && participantNum == 1 && !triggerY))
        {
            row = 1;
            triggerX = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) ||
            (Input.GetAxisRaw("Horizontal1P") <= -1 && participantNum == 0 && !triggerY) ||
            (Input.GetAxisRaw("Horizontal2P") <= -1 && participantNum == 1 && !triggerY))
        {
            row = 0;
            triggerX = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) ||
            (Input.GetAxisRaw("Vertical1P") <= -1 && participantNum == 0 && !triggerY) ||
            (Input.GetAxisRaw("Vertical2P") <= -1 && participantNum == 1 && !triggerY))
        {
            column--;
            triggerY = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) ||
            (Input.GetAxisRaw("Vertical1P") >= 1 && participantNum == 0 && !triggerY) ||
            (Input.GetAxisRaw("Vertical2P") >= 1 && participantNum == 1 && !triggerY))
        {
            column++;
            triggerY = true;
        }
        column = Mathf.Max(0, (Mathf.Min(column, 3)));
        this.transform.FindChild("SelectCursor").position = new Vector3(0.25f + 0.5f * row, 0.775f - 0.22f * column, 0);
    }
}
