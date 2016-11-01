using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Machine : MonoBehaviour {

    public enum COLLIDESTATE
    {
        NORMAL, //通常
        OFFROAD,    //舗装されていない路面
        ONWALL  //壁に衝突している
    }

    private Dictionary<string, float> machineState = new Dictionary<string,float>(); //マシンのステータス
    public Dictionary<string, float> MachineState
    {
        get { return machineState; }
    }
    public Dictionary<string, float> nowState = new Dictionary<string, float>();
    public COLLIDESTATE state;  //走っている路面の状態
    public float accel; //加速度
    public float rotate;    //回転角度(フレーム毎)
    public float speed;    //速度(m/s)
    public float kmhspeed;  //速度(km/h)
    public float friction;  //摩擦係数

    private float turnaxis;     //ハンドル操作
    private bool accelflag, brakeflag;   //加減速フラグ
    private bool backflag;   //バックフラグ
    protected bool useability;  //アビリティ使用フラグ
    private bool driftflag; //ドリフトフラグ

    private bool abilityLock;   //アビリティ使用制限
    public bool AbilityLock
    {
        get { return abilityLock; }
    }
    private float lockedTime;

    Vector3 vec;    //衝突用ベクトル

    public bool breakdown;  //破壊判定
    public Dictionary<string, float> breakParts;    //破壊箇所
    private const int REPAIRED = -1;
    //private float recoveryTime; //復帰時間

    private bool BarrierFlag; //バリア使用フラグ
    public bool Barrier
    {
        get { return BarrierFlag; }
        set { BarrierFlag = value; }
    }

    private int boost; //ブースト
    public int Boost
    {
        set { boost = value; }
    }

    private float spinTime; //回転時間

    private bool retireFlag;    //リタイアしたか
    public bool RetireFlag
    {
        get { return retireFlag; }
    }

    public Vector3 restartPos;
    public Quaternion restartRot;

    public float speedDownTime;

    private int collisionCount;

    public float dashspeed;

    //追加
    public AudioSource[] source;
    public AudioClip crashSE;
    public AudioClip bodycrashSE;
    public AudioClip sripSE;

	// Use this for initialization
	protected virtual void Start () {
        MachineState.Clear();
        //仮のステータス
        MachineState.Add("Armoring", 3);
        MachineState.Add("Accelerate", 3);
        MachineState.Add("Speed", 3);
        MachineState.Add("Grip", 3);
        MachineState.Add("Body", 30);
        MachineState.Add("Engine", 30);
        MachineState.Add("Wheel", 30);
        MachineState.Add("AbilityUnit", 20);
        nowState = new Dictionary<string,float>(MachineState);

        accel = 0;
        speed = 0;
        friction = 0;
        state = COLLIDESTATE.NORMAL;
        rotate = 0;

        turnaxis = 0;
        accelflag = false;
        brakeflag = false;
        backflag = false;
        driftflag = false;
        useability = false;
        abilityLock = false;
        lockedTime = 0;

        breakdown = false;
        breakParts = new Dictionary<string,float>();
        breakParts.Add("Body", REPAIRED);
        breakParts.Add("Engine", REPAIRED);
        breakParts.Add("Wheel", REPAIRED);
        breakParts.Add("AbilityUnit", REPAIRED);
        //recoveryTime = 0;

        restartPos = Vector3.zero;
        speedDownTime = 0;
        collisionCount = 0;
        dashspeed = 0;

        //追加
        source = transform.GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        //摩擦係数、加速度、回転角度の初期化
        friction = 0.001f;
        if (state == COLLIDESTATE.OFFROAD)
        {
            friction += 0.002f * nowState["Accelerate"];
        }
        if (driftflag)
        {
            accelflag = false;
            friction += 0.004f;
        }
        accel = 0;
        rotate = 0;

        brake();
        accelerator();
        turn();
        recover();
        Spining();
        SpeedDown();

        //速度の加減算
        speed += accel * Time.deltaTime * 60;
        speed -= speed * friction * Time.deltaTime * 60;
        //一定の速度以下でアクセルが操作されていない場合、速度を0にする
        if (Mathf.Abs(speed) < 1)
        {
            if (!accelflag)
                speed = 0;
        }

        //回転、速度を反映
        transform.Rotate(0, rotate * Time.deltaTime * 60, 0);
        dashspeed -= 250 * Time.deltaTime;
        if (dashspeed < 0)
        {
            dashspeed = 0;
        }
        rigidbody.velocity += new Vector3(Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad) * (speed + dashspeed), rigidbody.velocity.y, Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad) * (speed + dashspeed)) - rigidbody.velocity;
        Drift();        
        rigidbody.velocity += vec * Time.deltaTime * 60;
        vec = vec * 9 / 10;
        kmhspeed = (
            rigidbody.velocity.x * Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad) +
            rigidbody.velocity.z * Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad)) / 3.6f;
            //speed * 1000 / 3600.0f;
        
        //復帰
        if (transform.position.y <= -1000)
        {
            transform.rigidbody.velocity = Vector3.zero;
            transform.position = restartPos;
            transform.rotation = restartRot;
            speed = 0;
        }

        //アビリティロック解除
        if (abilityLock)
        {
            lockedTime += Time.deltaTime;
            if (lockedTime >= 10 - boost / 2.0f)
            {
                abilityLock = false;
                lockedTime = 0;
            }
        }

        collisionCount = 0;
	}

    void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            //壁に当たった時、跳ね返る
            case "wall":
                //speed = -speed * 0.4f;
                speed = speed * 0.4f;
                foreach (var contact in col.contacts)
                {
                    Vector3 reflect = Vector3.Reflect(transform.rigidbody.velocity, contact.normal) / col.contacts.Length;
                    reflect.y = 0;
                    vec += reflect;
                }
                break;

            case "plane":
            case "offroad":
                transform.rigidbody.velocity = new Vector3(
                    transform.rigidbody.velocity.x,
                    0,
                    transform.rigidbody.velocity.z);
                break;

            case "Player":
            case "CPU":
            case "DesignBullet":
            case "SportBullet":
                Vector3 v = this.transform.position - col.transform.position;
                v.y = 0;
                vec += v * 8;
                break;
        }

        switch (col.gameObject.tag)
        {
            case "wall":
                state = COLLIDESTATE.ONWALL;
                break;

            case "plane":
                state = COLLIDESTATE.NORMAL;
                break;
        }
    }

    float det(Vector3 v1, Vector3 v2)
    {
        return v1.x * v2.z - v1.z * v2.x;
    }

    void OnCollisionStay(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "wall":
                state = COLLIDESTATE.ONWALL;
                transform.rigidbody.velocity = new Vector3(
                    transform.rigidbody.velocity.x,
                    0,
                    transform.rigidbody.velocity.z);
                break;

            case "plane":
                state = COLLIDESTATE.NORMAL;
                transform.rigidbody.velocity = new Vector3(
                    transform.rigidbody.velocity.x,
                    0,
                    transform.rigidbody.velocity.z);
                collisionCount++;
                break;

            case "offroad":
                transform.rigidbody.velocity = new Vector3(
                    transform.rigidbody.velocity.x,
                    0,
                    transform.rigidbody.velocity.z);
                break;
        }

        if (collisionCount == 0)
        {
            state = COLLIDESTATE.OFFROAD;
        }
    }

    /*
    void OnCollisionExit(Collision col)
    {
        switch (col.gameObject.tag)
        {
                
            case "plane":
                restart =
                    transform.position +
                    new Vector3(0, 50, 0);
                //state = COLLIDESTATE.OFFROAD;
                break;
            
                
            case "wall":
                state = COLLIDESTATE.OFFROAD;
                break;
                 
        }
    }
     * */

    //ステータスを反映
    public void setMachineState(float[] machineState)
    {
        MachineState["Armoring"] = machineState[0];
        MachineState["Accelerate"] = machineState[1];
        MachineState["Speed"] = machineState[2];
        MachineState["Grip"] = machineState[3];
        MachineState["Body"] = machineState[4];
        MachineState["Engine"] = machineState[5];
        MachineState["Wheel"] = machineState[6];
        MachineState["AbilityUnit"] = machineState[7];
        nowState = new Dictionary<string,float>(MachineState);
    }

    //入力を反映
    public void input(bool accel, bool brake, bool back, float axis, bool useability, bool drift)
    {
        if (Time.timeScale == 0)
            return;

        if (spinTime <= 0)
        {
            accelflag = accel;
            brakeflag = brake;
            backflag = back;
            turnaxis = axis;
            driftflag = drift;
        }
        else
        {
            accelflag = false;
            brakeflag = false;
            backflag = false;
            turnaxis = 0;
            driftflag = false;
        }
        if (abilityLock ||
            machineState["AbilityUnit"] <= 0)
        {
            this.useability = false;
        }
        else
        {
            this.useability = useability;
        }
    }

    void brake()
    {
        if (brakeflag)
        {
            accel = 0;
            friction += 0.02f;
        }
    }

    void accelerator()
    {
        if (!brakeflag)
        {
            if(backflag)
            {
                accel = -0.6f * nowState["Accelerate"];
                speed = Mathf.Max(speed, -nowState["Speed"] * 25 - 50);
            }
            else if (accelflag)
            {
                accel = 0.7f + 0.1f * nowState["Accelerate"] * nowState["Accelerate"] + 0.3f * boost;
                speed = Mathf.Min(speed, nowState["Speed"] * 40 + 500 + 12 * boost * boost);
            }
        }
    }

    void turn()
    {
        if (Mathf.Abs(speed) < 30)
        {
            rotate = turnaxis * speed / 30.0f * (nowState["Grip"] * 0.2f + 0.5f);
        }
        else
        {
            rotate = turnaxis * (nowState["Grip"] * 0.2f + 0.5f + 0.05f * boost);
        }
        friction += Mathf.Abs(turnaxis) / 1000.0f * Time.deltaTime * 60;
    }

    public bool Damage(string str, float dam)
    {
        string s = str;
        if (str == "WheelRight" ||
            str == "WheelLeft")
        {
            s = "Wheel";
        }

        if (BarrierFlag == true)
        {
            return false;
        }

        nowState[s] -= dam;
        if (nowState[s] <= 0)
        {
            if (breakParts[s] == REPAIRED)
            {
                //recoveryTime = 30;
                breakdown = true;
                breakParts[s] = 30;
                switch (s)
                {
                    case "Engine":
                        nowState["Speed"] = MachineState["Speed"] - 2;
                        break;

                    case "Wheel":
                        nowState["Accelerate"] = MachineState["Accelerate"] - 2;
                        break;

                        /*
                    case "AbilityUnit":
                        //アビリティ使用禁止
                        abilityLock = true;
                        break;
                         * */
                }
                //追加
                audio.PlayOneShot(crashSE);
            }
            else
            {
                nowState["Body"] -= dam;
                if (nowState["Body"] <= 0)
                {
                    breakParts["Body"] = 30;
                    //追加
                    audio.PlayOneShot(bodycrashSE);
                    //リタイア処理
                    retireFlag = true;
                }
            }
        }
        return nowState[s] <= 0;
    }

    void recover()
    {
        if (breakdown)
        {
            //recoveryTime -= Time.deltaTime;
            List<string> keys = new List<string>(breakParts.Keys);
            int count = 0;
            foreach (string key in keys)
            {
                if (breakParts[key] == REPAIRED)
                {
                    count++;
                }
                else
                {
                    breakParts[key] -= Time.deltaTime;

                    if (breakParts[key] <= 0)
                    {
                        breakParts[key] = REPAIRED;
                        nowState[key] = MachineState[key];
                        count++;
                        switch (key)
                        {
                            case "Engine":
                                nowState["Speed"] += 2;
                                break;

                            case "Wheel":
                                nowState["Accelerate"] += 2;
                                break;
                        }
                        
                        if (breakParts.Count - count <= 0)
                        {
                            breakdown = false;
                        }
                    }
                }
            }
        }
    }

    public void spin()
    {
        if (!BarrierFlag)
        {
            spinTime = 1.5f;
            //追加
            audio.PlayOneShot(sripSE);
        }
    }

    private void Spining()
    {
        if (spinTime <= 0)
        {
            spinTime = 0;
            this.transform.FindChild("Model").rotation = new Quaternion(0, 0, 0, 0);
            return;
        }

        this.transform.FindChild("Model").Rotate(0, 360 * Time.deltaTime * 2, 0);
        speed -= speed / 1.5f * Time.deltaTime;
        spinTime -= Time.deltaTime;
    }

    public void Abilitylock(bool aLock)
    {
        if (machineState["AbilityUnit"] > 0)
        {
            abilityLock = aLock;
        }
    }

    void SpeedDown()
    {
        if (speedDownTime > 0)
        {
            speedDownTime -= Time.deltaTime;
            speed = Mathf.Min(speed, (Mathf.Max(speed - speed / 2.0f * Time.deltaTime, nowState["Speed"] * 10 + 350 + 5 * boost)));
        }
    }

    void Drift()
    {
        if (driftflag)
        {
            if (turnaxis > 0)
            {
                transform.Rotate(0, rotate * Time.deltaTime * 60 / 1.5f, 0);
                rigidbody.velocity -= new Vector3(Mathf.Sin((transform.eulerAngles.y + 90) * Mathf.Deg2Rad) * speed / 4.0f, 0, Mathf.Cos((transform.eulerAngles.y + 90) * Mathf.Deg2Rad) * speed / 4.0f);
            }

            if (turnaxis < 0)
            {
                transform.Rotate(0, rotate * Time.deltaTime * 60 / 1.5f, 0);
                rigidbody.velocity += new Vector3(Mathf.Sin((transform.eulerAngles.y + 90) * Mathf.Deg2Rad) * speed / 4.0f, 0, Mathf.Cos((transform.eulerAngles.y + 90) * Mathf.Deg2Rad) * speed / 4.0f);
            }
        }
    }
}
