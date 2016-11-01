using UnityEngine;
using System.Collections;

public class Patterns : MonoBehaviour {
    /*
    {
        {"Creater", "Design"},
        {"Music", "IT"},
        {"Technology","Medical"},
        {"Sports", "Random"}
    }
     * */

    private string machineName;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool accel(string name, float digree, float speed)
    {
        //switchでごまかす
        switch (name)
        {
            case "Creater":
                if (Mathf.Abs(digree) <= 20 || speed < 200)
                {
                    return true;
                }
                break;

            case "Design":
                if (Mathf.Abs(digree) <= 30 || speed < 200)
                {
                    return true;
                }
                break;

            case "Music":
                if (Mathf.Abs(digree) <= 30 || speed < 200)
                {
                    return true;
                }
                break;

            case "IT":
                if (Mathf.Abs(digree) <= 20 || speed < 200)
                {
                    return true;
                }
                break;

            case "Technology":
                if (Mathf.Abs(digree) <= 10 || speed < 200)
                {
                    return true;
                }
                break;

            case "Medical":
                if (Mathf.Abs(digree) <= 20 || speed < 200)
                {
                    return true;
                }
                break;

            case "Sports":
                if (Mathf.Abs(digree) <= 20 || speed < 200)
                {
                    return true;
                }
                break;
        }

        return false;
    }

    public bool brake(string name, float digree, float speed)
    {
        switch (name)
        {
            case "Creater":
                if (Mathf.Abs(digree) >= 70 && speed >= 300)
                {
                    return true;
                }
                break;

            case "Design":
                if (Mathf.Abs(digree) >= 70 && speed >= 300)
                {
                    return true;
                }
                break;

            case "Music":
                if (Mathf.Abs(digree) >= 80 && speed >= 300)
                {
                    return true;
                }
                break;

            case "IT":
                if (Mathf.Abs(digree) >= 60 && speed >= 300)
                {
                    return true;
                }
                break;

            case "Technology":
                if (Mathf.Abs(digree) >= 55 && speed >= 150)
                {
                    return true;
                }
                break;

            case "Medical":
                if (Mathf.Abs(digree) >= 60 && speed >= 300)
                {
                    return true;
                }
                break;

            case "Sports":
                if (Mathf.Abs(digree) >= 65 && speed >= 300)
                {
                    return true;
                }
                break;
        }

        return false;
    }

    public bool drift(string name, float digree, float speed)
    {
        switch (name)
        {
            case "Creater":
                if (Mathf.Abs(digree) >= 25 && speed >= 300)
                {
                    return true;
                }
                break;

            case "Design":
                if (Mathf.Abs(digree) >= 35 && speed >= 300)
                {
                    return true;
                }
                break;

            case "Music":
                if (Mathf.Abs(digree) >= 45 && speed >= 300)
                {
                    return true;
                }
                break;

            case "IT":
                if (Mathf.Abs(digree) >= 25 && speed >= 300)
                {
                    return true;
                }
                break;

            case "Technology":
                if (Mathf.Abs(digree) >= 15 && speed >= 200)
                {
                    return true;
                }
                break;

            case "Medical":
                if (Mathf.Abs(digree) >= 25 && speed >= 300)
                {
                    return true;
                }
                break;

            case "Sports":
                if (Mathf.Abs(digree) >= 20 && speed >= 300)
                {
                    return true;
                }
                break;
        }

        return false;
    }

    public float turnAxis(string name, float digree, float speed)
    {
        if (speed < 0)
            digree = -digree;

        switch (name)
        {
            case "Creater":
                if (digree <= -7)
                {
                    return -1.0f;
                }
                else if (digree >= 7)
                {
                    return 1.0f;
                }
                break;

            case "Design":
                if (digree <= -7)
                {
                    return -1.0f;
                }
                else if (digree >= 7)
                {
                    return 1.0f;
                }
                break;

            case "Music":
                if (digree <= -7)
                {
                    return -1.0f;
                }
                else if (digree >= 7)
                {
                    return 1.0f;
                }
                break;

            case "IT":
                if (digree <= -7)
                {
                    return -1.0f;
                }
                else if (digree >= 7)
                {
                    return 1.0f;
                }
                break;

            case "Technology":
                if (digree <= -0.3f)
                {
                    return -1.0f;
                }
                else if (digree >= 0.3f)
                {
                    return 1.0f;
                }
                break;

            case "Medical":
                if (digree <= -7)
                {
                    return -1.0f;
                }
                else if (digree >= 7)
                {
                    return 1.0f;
                }
                break;

            case "Sports":
                if (digree <= -7)
                {
                    return -1.0f;
                }
                else if (digree >= 7)
                {
                    return 1.0f;
                }
                break;
        }

        return 0;
    }
}
