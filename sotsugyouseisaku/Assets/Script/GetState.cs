using UnityEngine;
using System.Collections;

public class GetState : MonoBehaviour {

    Vector3[] vec = {
                        new Vector3(-90,0,80),
                        new Vector3(-60,0,120),
                        new Vector3(-30,0,160),
                        new Vector3(0,0,200),
                        new Vector3(30,0,240),
                        new Vector3(60,0,280),
                        new Vector3(90,0,320)
                    };

    SelectState state;
    GameObject[] gameobj;
    Ranking ranking;

    void Awake()
    {

    }

	// Use this for initialization
    void Start()
    {
        for (int i = 0; i < vec.Length; i++)
        {
            int n = Random.Range(0, vec.Length);
            Vector3 v = vec[i];
            vec[i] = vec[n];
            vec[n] = v;
        }

        state = GameObject.Find("Select").GetComponent<SelectState>();
        Destroy(GameObject.Find("Select"));
        gameobj = new GameObject[state.getParticipants()];

        GameObject dataobj = GameObject.Find("Course");
        dataobj.GetComponent<CourseData>().setData(state.getCourse());
        GameObject obj = (GameObject)Instantiate(Resources.Load<GameObject>(state.getCourse()));
        obj.transform.parent = dataobj.transform;

        for (int i = 0; i < state.getParticipants(); i++)
        {
            gameobj[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Machines/" + state.getMachines()[i] + "CollegeModel"), vec[i], new Quaternion(0, 180, 0, 0));
            gameobj[i].transform.parent = this.gameObject.transform;
            gameobj[i].name = state.getMachines()[i];
        }

        if (state.getPlayers() == 1)
        {
            obj = (GameObject)Instantiate(Resources.Load<GameObject>("Player"), new Vector3(-90, 0, 100), new Quaternion(0, 180, 0, 0));
            obj.transform.position = gameobj[0].transform.position;
            obj.transform.parent = gameobj[0].transform;
            Driver p = obj.GetComponent<Driver>();
            p.Name = "Player1";
        }
        else if (state.getPlayers() == 2)
        {
            obj = (GameObject)Instantiate(Resources.Load<GameObject>("Player1"), new Vector3(-90, 0, 100), new Quaternion(0, 180, 0, 0));
            obj.transform.position = gameobj[0].transform.position;
            obj.transform.parent = gameobj[0].transform;
            Driver p = obj.GetComponent<Driver>();
            p.Name = "Player1";
            obj = (GameObject)Instantiate(Resources.Load<GameObject>("Player2"), new Vector3(-90, 0, 100), new Quaternion(0, 180, 0, 0));
            obj.transform.position = gameobj[1].transform.position;
            obj.transform.parent = gameobj[1].transform;
            p = obj.GetComponent<Driver>();
            p.Name = "Player2";
        }
        for (int i = state.getPlayers(); i < state.getParticipants(); i++)
        {
            obj = (GameObject)Instantiate(Resources.Load<GameObject>("CPU"), new Vector3(30 * i - 90, 0, 40 * i + 100), new Quaternion(0, 180, 0, 0));
            obj.transform.position = gameobj[i].transform.position;
            obj.transform.parent = gameobj[i].transform;
            AI ai = obj.GetComponent<AI>();
            ai.setMachineName(state.getMachines()[i]);
            ai.Name = "CPU" + i;
        }

        PlayCourseBGM bgm = GameObject.Find("BGM").GetComponent<PlayCourseBGM>();
        bgm.SetBGM(state.getCourse());
    }

    // Update is called once per frame
    void Update()
    {
	}
}
