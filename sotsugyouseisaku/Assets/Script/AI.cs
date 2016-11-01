using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//一応継承
public class AI : Driver {

    private GameObject mMachine;  //操作する機体
    private float digree;    //目標の経路オブジェクトとの角度
    private float mSpeed; //機体の速度
    private Patterns patterns;
    private string machineName;

	// Use this for initialization
	protected override void Start () {
        driverState = DRIVERSTATE.CPU;
        base.Start();

        mMachine = transform.parent.gameObject;
        nowflagnumber = 1;

        patterns = GetComponent<Patterns>();
	}
	
	// Update is called once per frame
	protected override void Update () {
        accelflag = false;
        brakeflag = false;
        driftflag = false;
        useability = false;
        useitem = false;
        turnaxis = 0;

        //通過点の設定
        Vector3 target = rootObjects[nowflagnumber].transform.FindChild("Point").position;
        //通過点までのベクトル
        Vector3 vec = target - mMachine.transform.position;
        //CPUの進行方向のベクトル
        Vector3 playerVec = mMachine.transform.rigidbody.velocity;
        //進行ベクトルと通過点までのベクトルの角度差
        digree = angle_Digree(vec, playerVec);
        //速度
        mSpeed = Mathf.Pow(playerVec.x * playerVec.x + playerVec.z * playerVec.z, 0.5f);

        pattern();

        base.Update();
	}

    void pattern()
    {
        accelflag = patterns.accel(machineName, digree, mSpeed);
        brakeflag = patterns.brake(machineName, digree, mSpeed);
        turnaxis = patterns.turnAxis(machineName, digree, mSpeed);
        driftflag = patterns.drift(machineName, digree, mSpeed);
        if (nextRange() >= 300)
        {
            if(Random.Range(0, 200) == 0)
            {
                useability = true;
            }
            if (Random.Range(0, 200) == 1)
            {
                useitem = true;
            }
        }
        //Debug.Log("AI調整中");

        /*
        if (GameObject.Find("DesignBullet(Clone)") != null)
        {
            GameObject obj = GameObject.Find("DesignBullet(Clone)");
            float distance = Vector2.Distance(
                new Vector2(this.transform.position.x, this.transform.position.z),
                new Vector2(obj.transform.position.x, obj.transform.position.z));
            if (distance <= 200)
            {
                //float dig = angle_Digree(this.transform.position.normalized, obj.transform.position.normalized);
                //if (Mathf.Abs(dig) >= 90)
                {
                    if (dig > 0)
                    {
                        turnaxis = 1;
                    }
                    else
                    {
                        turnaxis = -1;
                    }

                    if (mSpeed < 0)
                    {
                        turnaxis = -turnaxis;
                    }

                    if (mSpeed < 100)
                    {
                        brakeflag = false;
                        accelflag = true;
                    }
                }
            }
        }
         * */
    }

    public void setMachineName(string name)
    {
        machineName = name;
    }

    float angle_Digree(Vector3 v1, Vector3 v2)
    {
        float dot_v12 = dot(v1, v2);    //内積
        float det_v12 = det(v1, v2);    //外積(プラスで右、マイナスで左の判定)
        float v1_length = Mathf.Pow((v1.x * v1.x) + (v1.z * v1.z), 0.5f);
        float v2_length = Mathf.Pow((v2.x * v2.x) + (v2.z * v2.z), 0.5f);

        float cos = dot_v12 / (v1_length * v2_length);
        float sita = Mathf.Acos(cos);
        float digree = sita * 180.0f / Mathf.PI;

        if(det_v12 < 0)
        {
            digree = -digree;
        }

        return digree;
    }

    float dot(Vector3 v1, Vector3 v2)
    {
        return v1.x * v2.x + v1.z * v2.z;
    }

    float det(Vector3 v1, Vector3 v2)
    {
        return v1.x * v2.z - v1.z * v2.x;
    }
}
