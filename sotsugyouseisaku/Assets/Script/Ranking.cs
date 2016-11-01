using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ranking : MonoBehaviour {

    private List<Driver> driversList;
    private Dictionary<int, string> driversListResult;
    private Dictionary<string, string> driversMachine;
    private Dictionary<string, float> driversTime;
    private Dictionary<int, string> driverRetired;
    private int playerCount;
    private static int goalPlayerCount;
    private static int retirePlayerCount;

    private bool end;
    private float time;
    private int goalRank;

	// Use this for initialization
	void Start () {
        driversList = new List<Driver>(GetComponentsInChildren<Driver>());
        driversListResult = new Dictionary<int, string>();
        driversMachine = new Dictionary<string, string>();
        driversTime = new Dictionary<string, float>();
        driverRetired = new Dictionary<int, string>();
        playerCount = 0;
        goalPlayerCount = 0;
        retirePlayerCount = 0;
        end = false;
        time = 0;
        goalRank = 1;
    }
	
	// Update is called once per frame
    void LateUpdate()
    {
        if (end)
        {
            time += Time.deltaTime;
            if (goalPlayerCount == 0)
            {
                if (time >= 5)
                {
                    for (int i = 0; i < driversList.Count; i++)
                    {
                        setResult(driversList[i], i + 1);
                    }
                    ResultScript r = GameObject.Find("ResultData").GetComponent<ResultScript>();
                    r.setRanking(driversListResult);
                    r.driversData(driversMachine, driversTime);
                    Application.LoadLevel("Result");
                }
            }
            if (time >= 15)
            {
                for (int i = 0; i < driversList.Count; i++)
                {
                    setResult(driversList[i], i + 1);
                }
                ResultScript r = GameObject.Find("ResultData").GetComponent<ResultScript>();
                r.setRanking(driversListResult);
                r.driversData(driversMachine, driversTime);
                Application.LoadLevel("Result");
            }
            return;
        }

        if(driversList.Count == 0)
            driversList = new List<Driver>(this.GetComponentsInChildren<Driver>());
        
        sort();

        /*
        if (driversListResult.Count >= driversList.Count)
        {
            end = true;
        }
        else    */
        {
            int count = 0;
            foreach (var d in GetComponentsInChildren<Driver>())
            {
                if (d.driverState == Driver.DRIVERSTATE.PLAYER)
                {
                    count++;
                }
            }
            playerCount = count;
            if (playerCount != 0)
            {
                if (retirePlayerCount + goalPlayerCount >= playerCount)
                {
                    Debug.Log(retirePlayerCount + "," + goalPlayerCount + "," + count);
                    end = true;
                }
            }
        }
        //Debug.Log(driversListResult.Count + "/" + driversList.Count);
	}

    void sort()
    {
        for (int i = 0; i < driversList.Count - 1; i++)
        {
            for (int j = 0; j < driversList.Count - i - 1; j++)
            {
                if (driversList[j].getFlags() < driversList[j + 1].getFlags())
                {
                    Driver d = driversList[j];
                    driversList[j] = driversList[j + 1];
                    driversList[j + 1] = d;
                }
                if (driversList[j].getFlags() == driversList[j + 1].getFlags())
                {
                    if (driversList[j].nextRange() > driversList[j + 1].nextRange())
                    {
                        Driver d = driversList[j];
                        driversList[j] = driversList[j + 1];
                        driversList[j + 1] = d;
                    }
                }
            }
        }

        for (int i = 0; i < driversList.Count; i++)
        {
            if (driversList[i].endFlag)
            {
                setResult(driversList[i], goalRank);
            }
            else
            {
                driversList[i].setRank(i + 1);
            }
        }
    }

    private void setResult(Driver driver, int rank)
    {
        if (driver.machine.RetireFlag)
        {
            for (int i = driversList.Count; i >= 1; i--)
            {
                if (driversListResult.ContainsKey(i) == false &&
                    driversListResult.ContainsValue(driver.Name) == false)
                {
                    driversListResult.Add(i, driver.Name);
                    driversMachine.Add(driver.Name, driver.transform.parent.name);
                    driversTime.Add(driver.Name, driver.AllTime);
                    driversList.Remove(driver);
                    driverRetired.Add(i, driver.Name);
                    break;
                }
            }
        }
        else if (driversListResult.ContainsValue(driver.Name) == false)
        {
            /*
            if (driversListResult.ContainsKey(rank) == false)
            {
                driversListResult.Add(rank, driver.Name);
                driversMachine.Add(driver.Name, driver.transform.parent.name);
                driversTime.Add(driver.Name, driver.AllTime);
                goalRank++;
            }
            */
            driversMachine.Add(driver.Name, driver.transform.parent.name);
            driversTime.Add(driver.Name, driver.AllTime);
            List<KeyValuePair<string, float>> list = new List<KeyValuePair<string, float>>(driversTime);
            list.Sort(timesort);
            driversListResult.Clear();
            int r = 1;
            foreach (KeyValuePair<string, float> kvp in list)
            {
                if (kvp.Value != 0)
                {
                    driversListResult.Add(r, kvp.Key);
                    r++;
                }
            }
            foreach (KeyValuePair<string, float> kvp in list)
            {
                if (end && !(driverRetired.ContainsValue(kvp.Key) || driversListResult.ContainsValue(kvp.Key)))
                {
                    driversListResult.Add(r, kvp.Key);
                    r++;
                }
            }
            for (int i = 7; i > 7 - driverRetired.Count; i--)
            {
                driversListResult.Add(i, driverRetired[i]);
            }
            goalRank++;
        }
    }

    int timesort(KeyValuePair<string, float> pair1, KeyValuePair<string, float> pair2)
    {
        return (int)((pair1.Value - pair2.Value) * 100);
    }

    public static void PlayerRetire()
    {
        retirePlayerCount++;
        //Debug.Log(retirePlayerCount);
    }

    public static void Goal()
    {
        goalPlayerCount++;
    }
}
