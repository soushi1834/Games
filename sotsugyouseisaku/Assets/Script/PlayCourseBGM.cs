using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayCourseBGM : MonoBehaviour {

    private string playBgmName;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetBGM(string name)
    {
        playBgmName = name;
        switch (name)
        {
            case "Circuit1":
                this.transform.FindChild("CourseBGM").FindChild("Course1").gameObject.SetActive(true);
                break;

            case "Circuit2":
                this.transform.FindChild("CourseBGM").FindChild("Course2").gameObject.SetActive(true);
                break;

            case "Circuit3":
                this.transform.FindChild("CourseBGM").FindChild("Course3").gameObject.SetActive(true);
                break;

            case "Circuit4":
                this.transform.FindChild("CourseBGM").FindChild("Course4").gameObject.SetActive(true);
                break;

            case "Circuit5":
                this.transform.FindChild("CourseBGM").FindChild("Course5").gameObject.SetActive(true);
                break;
        }
    }

    public void GoalBGM()
    {
        this.transform.FindChild("CourseBGM").gameObject.SetActive(false);
        this.transform.FindChild("Goal").gameObject.SetActive(true);
    }
}
