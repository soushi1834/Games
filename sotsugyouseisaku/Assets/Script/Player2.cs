using UnityEngine;
using System.Collections;

public class Player2 : Driver
{
    private Ability ability;

    // Use this for initialization
    protected override void Start()
    {
        driverState = DRIVERSTATE.PLAYER;
        base.Start();
        ability = machine.gameObject.GetComponent<Ability>();
    }

    // Update is called once per frame
    protected override void Update()
    {
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
            Input.GetKey(KeyCode.Joystick2Button5))
        {
            driftflag = true;
        }

        base.Update();

        setGUIData();
    }

    void brake()
    {
        if (Input.GetKey(KeyCode.Joystick2Button3))
        {
            brakeflag = true;
        }

        if (Input.GetKey(KeyCode.U))
        {
            brakeflag = true;
        }
    }

    void accelerator()
    {
        if (Input.GetKey(KeyCode.Joystick2Button2))
        {
            accelflag = true;
        }
        if (Input.GetKey(KeyCode.Joystick2Button0))
        {
            backflag = true;
        }

        if (Input.GetKey(KeyCode.I))
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
        if (Mathf.Abs(Input.GetAxis("Horizontal2P")) >= 0.1f)
        {
            turnaxis = Input.GetAxisRaw("Horizontal2P");
        }
    }

    void useItem()
    {
        if (Input.GetKey(KeyCode.Joystick2Button1))
        {
            useitem = true;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            useitem = true;
        }
    }

    void useAbility()
    {
        if (Input.GetKey(KeyCode.Joystick2Button7))
        {
            useability = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
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
        if (item != null)
            GUI.Label(new Rect(Screen.width / 10, Screen.height * 9 / 10, 1000, 1000), item.name, LabelStyle);
    }
     * */

    void setGUIData()
    {
        LapCount2P.NowLap = lap;

        AbilityGage2P.Power = Mathf.Min(ability.NowTime / ability.WaitTime, 1);
        AbilityGage2P.AbilityLock = machine.AbilityLock;
        AbilityGage2P.Broken = (machine.breakParts["AbilityUnit"] != -1); //REPAIRED = -1;

        AUGage2P.HP = machine.nowState["AbilityUnit"] / machine.MachineState["AbilityUnit"];
        EngineGage2P.HP = machine.nowState["Engine"] / machine.MachineState["Engine"];
        WheelGage2P.HP = machine.nowState["Wheel"] / machine.MachineState["Wheel"];
        BodyGage2P.HP = machine.nowState["Body"] / machine.MachineState["Body"];
        Rank2P.mRank2P = rank;

        if (item != null)
        {
            ItemTxture2P.ItemName = item.name;
        }
        else
        {
            ItemTxture2P.ItemName = null;
        }

        if (!endFlag)
            TimeUI2P.Time = Timer.AllTime;
        else
            TimeUI2P.Time = AllTime;

        SpeedUI2P.Speed = machine.kmhspeed;

        if (endFlag)
        {
            if (!machine.RetireFlag)
            {
                GoalUI2P.Flag = true;
            }
        }

        if (machine.RetireFlag)
        {
            if (endFlag != true)
            {
                RetireUI2P.Flag = true;
                Ranking.PlayerRetire();
            }
            endFlag = true;
        }
    }
}
