using UnityEngine;
using System.Collections;

public class CourseData : MonoBehaviour {

    private int lap = 3;
    private AI[] ais;

	// Use this for initialization
	void Start () {
	}

    public void setData(string course)
    {
        switch (course)
        {
            case "Circuit1":
                lap = 3;
                break;

            case "Circuit2":
            case "Circuit3":
            case "Circuit4":
                lap = 2;
                break;

            case "Circuit5":
                lap = 3;
                break;
        }
        EndLapData.EndLap = lap;
    }

    public int getFlags()
    {
        return GetComponentInChildren<RootObjectScript>().getFlags();
    }

    public int getEndLap()
    {
        return lap;
    }

    // Update is called once per frame
    void Update()
    {
	
	}
}
