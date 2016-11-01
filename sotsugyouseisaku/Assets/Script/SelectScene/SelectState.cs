using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectState : MonoBehaviour
{
    public enum SELECTSTATE
    {
        SELECT_PLAY_OF_PARTICIPANTS,
        SELECT_MACHINES,
        SELECT_COURSE,
        SELECT_END,
        SELECT_NULL
    };

    public static SELECTSTATE now;
    private int participants;
    private int players;
    private List<string> machines;
    private string course;
    private Data data;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
        now = SELECTSTATE.SELECT_PLAY_OF_PARTICIPANTS;
        this.transform.FindChild("SelectMachine").gameObject.SetActive(true);
        participants = 7;
        data = GameObject.Find("GetPlayers").GetComponent<Data>();
        players = data.Players;
        Destroy(data.gameObject);
        string[] str = new string[participants];
        machines = new List<string>(str);
    }

    void Update()
    {
        if (Application.loadedLevelName == "Title")
        {
            Destroy(this.gameObject);
        }
    }

    public SELECTSTATE nowState()
    {
        return now;
    }

    public void next(SELECTSTATE next)
    {
        switch (next)
        {
                /*
            case SELECTSTATE.SELECT_PLAY_OF_PARTICIPANTS:
                this.transform.FindChild("SelectParticipants").gameObject.SetActive(true);
                now = next;
                break;
                 * */

            case SELECTSTATE.SELECT_MACHINES:
                this.transform.FindChild("SelectMachine").gameObject.SetActive(true);
                now = next;
                break;

            case SELECTSTATE.SELECT_COURSE:
                this.transform.FindChild("SelectCourse").gameObject.SetActive(true);
                now = next;
                break;

            case SELECTSTATE.SELECT_END:
                this.transform.FindChild("SelectEnd").gameObject.SetActive(true);
                now = next;
                break;

            default:
                break;
        }
    }

    public void setParticipants(int participants, int players)
    {
        this.participants = participants;
        this.players = players;
        string[] str = new string[participants];
        machines = new List<string>(str);
    }

    public int getParticipants()
    {
        return participants;
    }

    public int getPlayers()
    {
        return players;
    }

    public void setMachines(string name, int n)
    {
        machines[n] = name;
    }

    public List<string> getMachines()
    {
        return machines;
    }

    public void setCourse(string name)
    {
        course = name;
    }

    public string getCourse()
    {
        return course;
    }

    /*
    void OnGUI()
    {
        //仮表示
        for (int i = 0; i < players; i++)
        {
            GUI.Label(new Rect(Screen.width / 8.0f * (i), Screen.height / 100.0f, Screen.width / 8.0f, Screen.height / 8.0f), "Player" + i.ToString());
        }
        for (int i = players; i < participants; i++)
        {
            GUI.Label(new Rect(Screen.width / 8.0f * (i), Screen.height / 100.0f, Screen.width / 8.0f, Screen.height / 8.0f), "CPU" + i.ToString());
        }

        for (int i = 0; i < machines.Count; i++)
        {
            GUI.Label(new Rect(Screen.width / 8.0f * i, Screen.height / 100.0f * 5, Screen.width / 8.0f, Screen.height / 8.0f), machines[i]);
        }

        GUI.Label(new Rect(0, Screen.height / 100.0f * 10, Screen.width / 8.0f, Screen.height / 8.0f), course);
    }
     * */
}
