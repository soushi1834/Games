using UnityEngine;
using System.Collections;

public class Player : Driver {

    //private Texture texture;
    private Ability ability;

	// Use this for initialization
	protected override void Start () {
        /*用途不明
        texture = Resources.Load("ブーストエネルギー") as Texture2D;
        texture = Resources.Load("バスターアイコン") as Texture2D;
        texture = Resources.Load("マキビシ") as Texture2D;
        texture = Resources.Load("リフレクション") as Texture2D;
        texture = Resources.Load("リペアBOX") as Texture2D;
         * */

        driverState = DRIVERSTATE.PLAYER;
        base.Start();
        ability = machine.gameObject.GetComponent<Ability>();
	}
	
	// Update is called once per frame
	protected override void Update () {
        accelflag = false;
        brakeflag = false;
        backflag = false;
        turnaxis = 0;
        useitem = false;
        driftflag = false;
        useability = false;

        brake();
        accelerator();
        turn();
        useItem();
        useAbility();

        //ドリフト
        if (Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.Joystick1Button5))
        {
            driftflag = true;
        }

        base.Update();

        setGUIData();
	}

    void brake()
    {
        if (Input.GetKey(KeyCode.Joystick1Button3))
        {
            brakeflag = true;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            brakeflag = true;
        }
    }

    void accelerator()
    {
        if (Input.GetKey(KeyCode.Joystick1Button2))
        {
            accelflag = true;
        }
        if (Input.GetKey(KeyCode.Joystick1Button0))
        {
            backflag = true;
        }

        if (Input.GetKey(KeyCode.X))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //バックフラグを立てる
                backflag = true;
            }
            accelflag = true;
        }
    }

    void turn()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal1P")) >= 0.1f)
        {
            turnaxis = Input.GetAxisRaw("Horizontal1P");
        }
        /*
        Debug.Log("キーボード操作");
        turnaxis = Input.GetAxisRaw("Horizontal");
         * */
    }

    void useItem()
    {
        if (Input.GetKey(KeyCode.Joystick1Button1))
        {
            useitem = true;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            useitem = true;
        }
    }

    void useAbility()
    {
        if (Input.GetKey(KeyCode.Joystick1Button7))
        {
            useability = true;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            useability = true;
        }
    }

    /*
    void OnGUI()
    {
        GUIStyle LabelStyle = new GUIStyle();
        LabelStyle.fontSize = 50;
        LabelStyle.normal.textColor = new Color(200, 200, 200, 255);
        GUI.Label(new Rect(Screen.width * 9 / 10, Screen.height * 9 / 10, 1000, 1000), rank.ToString() + "位", LabelStyle);
        if(item != null)
            GUI.Label(new Rect(Screen.width / 10, Screen.height * 9 / 10, 1000, 1000), item.name, LabelStyle);
    }
     * */

    void setGUIData()
    {
        LapCount.NowLap = lap;

        AbilityGage.Power = Mathf.Min(ability.NowTime / ability.WaitTime, 1);
        AbilityGage.AbilityLock = machine.AbilityLock;
        AbilityGage.Broken = (machine.breakParts["AbilityUnit"] >= 0);

        AUGage.HP = machine.nowState["AbilityUnit"] / machine.MachineState["AbilityUnit"];
        EngineGage.HP = machine.nowState["Engine"] / machine.MachineState["Engine"];
        WheelGage.HP = machine.nowState["Wheel"] / machine.MachineState["Wheel"];
        BodyGage.HP = machine.nowState["Body"] / machine.MachineState["Body"];
        Rank.mRank = rank;

        if (item != null)
        {
            ItemTexture.ItemName = item.name;
        }
        else
        {
            ItemTexture.ItemName = null;
        }

        if (!endFlag)
            TimeUI.Time = Timer.AllTime;
        else
            TimeUI.Time = AllTime;

        SpeedUI.Speed = machine.kmhspeed;

        if (endFlag)
        {
            if (!machine.RetireFlag)
            {
                GoalUI.Flag = true;
            }
        }

        if (machine.RetireFlag)
        {
            if (endFlag != true)
            {
                RetireUI.Flag = true;
                Ranking.PlayerRetire();
            }
            endFlag = true;
        }
    }
}
