using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Driver : MonoBehaviour {

    public enum DRIVERSTATE
    {
        PLAYER,
        CPU
    }

    public Machine machine;
    protected string mName;
    public string Name
    {
        set { mName = value; }
        get { return mName; }
    }

    public DRIVERSTATE driverState;    //ドライバーが人かAIか
    public float turnaxis;     //ハンドル操作
    public bool accelflag, brakeflag;   //加減速フラグ
    public bool backflag;   //バックフラグ
    public bool driftflag;
    public bool startFlag;  //スタートを切ったか
    public bool endFlag;    //ゴールしたか

    protected int rootflags;   //経路オブジェクトの総数
    protected List<GameObject> rootObjects = new List<GameObject>();  //経路オブジェクトのリスト
    public int nowflagnumber;   //目標の経路オブジェクトの番号
    protected int getrootflags;    //通過した経路オブジェクトの数
    protected bool allget;  //一周したか
    protected int rank;
    protected int lap;

    public GameObject item;
    public bool useitem;    //アイテムを使用
    public bool useability;

    private CourseData data;

    private float allTime;
    public float AllTime
    {
        get { return allTime; }
    }

	// Use this for initialization
	protected virtual void Start () {
        if (driverState == DRIVERSTATE.PLAYER)
        {
            transform.parent.tag = "Player";
        }
        else
        {
            transform.parent.tag = "CPU";
        }
        machine = this.transform.parent.GetComponentInChildren<Machine>();
        turnaxis = 0;
        accelflag = false;
        brakeflag = false;
        backflag = false;
        driftflag = false;
        useitem = false;
        useability = false;

        nowflagnumber = 1;
        getrootflags = 0;
        allget = false;

        startFlag = false;
        endFlag = false;
        rank = 1;
        lap = 1;

        item = null;

        data = GameObject.FindWithTag("course").GetComponent<CourseData>();
        setFlags(data.getFlags());
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (Time.timeScale == 0)
            return;

        if (machine.RetireFlag &&
            endFlag)
            {
                machine.input(false, true, false, 0, false, false);
                return;
            }
        if(startFlag)
            machine.input(accelflag, brakeflag, backflag, turnaxis, useability, driftflag);

        machine.Boost = rank - 1;

        //仮アイテム発射プログラム(あとで分離)
        if (useitem)
        {
            if (item != null)
            {
                GameObject obj = Instantiate(item, this.transform.position, this.transform.rotation) as GameObject;
                Item i = obj.GetComponent<Item>();
                Vector3 force;
                force = this.gameObject.transform.forward;
                obj.rigidbody.AddForce(force);
                i.setData(this.transform.parent.gameObject, force);
                if (item.name == "Makibishi")
                {
                    obj = Instantiate(item, this.transform.position, this.transform.rotation) as GameObject;
                    i.transform.position += new Vector3(
                    20 * Mathf.Sin((transform.eulerAngles.y + 90) * Mathf.Deg2Rad),
                    10,
                    20 * Mathf.Cos((transform.eulerAngles.y + 90) * Mathf.Deg2Rad));
                    i = obj.GetComponent<Item>();
                    force = this.gameObject.transform.forward;
                    obj.rigidbody.AddForce(force);
                    i.setData(this.transform.parent.gameObject, force);

                    obj = Instantiate(item, this.transform.position, this.transform.rotation) as GameObject;
                    i.transform.position += new Vector3(
                    20 * Mathf.Sin((transform.eulerAngles.y - 90) * Mathf.Deg2Rad),
                    10,
                    20 * Mathf.Cos((transform.eulerAngles.y - 90) * Mathf.Deg2Rad));
                    i = obj.GetComponent<Item>();
                    force = this.gameObject.transform.forward;
                    obj.rigidbody.AddForce(force);
                    i.setData(this.transform.parent.gameObject, force);
                }
                item = null;
            }
        }

        //リタイア処理
        if (machine.RetireFlag)
        {
            /* プレイヤーはプレイヤークラスで処理
            if (driverState == DRIVERSTATE.PLAYER &&
                endFlag != true)
            {
                RetireUI.Flag = true;
                Ranking.PlayerRetire();
            }
             * */
            if (driverState != DRIVERSTATE.PLAYER)
                endFlag = true;
        }
    }

    public void setFlags(int f)
    {
        rootflags = f;
        for (int i = 0; i < rootflags; i++)
        {
            rootObjects.Add(GameObject.Find("RouteCube" + i));
        }
    }

    public int getFlags()
    {
        return getrootflags;
    }

    public void setStartFrag(bool flag)
    {
        startFlag = flag;
    }

    public void setEndFlag(bool flag)
    {
        endFlag = flag;
    }

    public void getItem(GameObject item)
    {
        if (this.item == null)
        {
            this.item = item;
        }
    }

    public bool setRank(int r)
    {
        if (!endFlag)
        {
                rank = r;
        }

        return endFlag;
    }

    public float nextRange()
    {
        GameObject next = GameObject.Find("RouteCube" + nowflagnumber);
        return Vector3.Distance(next.transform.position, this.transform.position);
    }

    public int getLap()
    {
        return lap;
    }

    public bool nextLap(bool end)
    {
        if (allget)
        {
            if (end)
            {
                if (!endFlag)
                {
                    if (driverState == DRIVERSTATE.PLAYER)
                    {
                        Ranking.Goal();
                    }
                    allTime = Timer.AllTime;
                }
                endFlag = true;
                return true;
            }
            else
            {
                lap++;
            }
            allget = false;
        }

        return false;
    }

    protected void OnTriggerEnter(Collider col)
    {
        if (col.transform.name == "RouteCube" + nowflagnumber ||
            col.transform.name == "RouteCube" + (nowflagnumber + 1))
        {
            getrootflags++;
            nowflagnumber++;
            if (nowflagnumber >= rootflags)
            {
                allget = true;
                nowflagnumber = 0;
            }
            machine.restartPos = this.transform.position;
            machine.restartPos.y += 30;
            machine.restartRot = this.transform.rotation;
        }
    }

    protected void OnTriggerStay(Collider col)
    {
        if (col.transform.name == "RouteCube" + nowflagnumber ||
            col.transform.name == "RouteCube" + (nowflagnumber + 1))
        {
            getrootflags++;
            nowflagnumber++;
            if (nowflagnumber >= rootflags)
            {
                allget = true;
                nowflagnumber = 0;
            }
            machine.restartPos = this.transform.position;
            machine.restartPos.y += 30;
            machine.restartRot = this.transform.rotation;
        }
    }
}
