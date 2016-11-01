using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour {

    public enum COLLIDESTATE
    {
        NORMAL,
        OFFROAD,
        ONWALL
    }

    public enum MACHINESTATE
    {
        ARMORING    = 3,
        ACCELERATE  = 3,
        SPEED       = 3,
        GRIP        = 3
    }
    public Dictionary<string, int> MachineState = new Dictionary<string, int>(); //マシンのステータス

    public COLLIDESTATE state;
    public float accel;
    public float rotate;
    private float speed;
    public float kmhspeed;
    public float friction;

    //public bool left, right;
    public float turnaxis; 
    public bool accelflag, brakeflag;

	// Use this for initialization
	void Start () {
        MachineState.Add("Armoring", 3);
        MachineState.Add("Accelerate", 3);
        MachineState.Add("Speed", 3);
        MachineState.Add("Grip", 3);

        accel = 0;
        speed = 0;
        friction = 0;
        state = COLLIDESTATE.NORMAL;
        rotate = 0;

        turnaxis = 0;
        accelflag = false;
        brakeflag = false;
	}
	
	// Update is called once per frame
	void Update () {
        friction = 0.0002f;
        if(state == COLLIDESTATE.OFFROAD)
        {
            friction += 0.002f * MachineState["Accelerate"];
        }
        accel = 0;
        rotate = 0;

        //Brake();
        //Accelerator();
        //Handle();
        brake();
        accelerator();
        turn();

        speed += accel;
        speed -= speed * friction;
        if (speed < 1 && speed > -1)
        {
            if (!accelflag)
                speed = 0;
        }

        transform.Rotate(0, rotate, 0);
        rigidbody.velocity = new Vector3(Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad) * speed, rigidbody.velocity.y - 1, Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad) * speed);

        kmhspeed = speed * 1000 / 3600.0f;
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            accel = 0;
            friction += 0.02f;
        }
    }

    void Accelerator()
    {
        if (Input.GetKey(KeyCode.X))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                accel = -0.02f * MachineState["Accelerate"];
            }
            else
            {
                accel = 0.05f * MachineState["Accelerate"] * MachineState["Accelerate"];
                speed = Mathf.Min(speed, MachineState["Speed"] * 50 + 150);
            }
        }
    }

    void Handle()
    {
        if (speed < 10)
        {
            rotate = Input.GetAxisRaw("Horizontal") * speed / 10.0f * (int)MACHINESTATE.GRIP / 3.0f;
        }
        else
        {
            rotate = Input.GetAxisRaw("Horizontal") * (int)MACHINESTATE.GRIP / 3.0f;
        }
        friction += Mathf.Abs(Input.GetAxisRaw("Horizontal")) / 1000.0f;
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "wall")
            speed = -speed * 0.2f;
    }

    void OnCollisionStay(Collision col)
    {
        switch(col.gameObject.tag)
        {
            case "wall":
                state = COLLIDESTATE.ONWALL;
                break;

            case "plane":
                state = COLLIDESTATE.NORMAL;
                break;

            case "offroad":
                state = COLLIDESTATE.OFFROAD;
                break;
        }
    }

    void brakeFlag(bool flag)
    {
        brakeflag = flag;
    }

    void brake()
    {
        if (brakeflag)
        {
            accel = 0;
            friction += 0.02f;
        }
    }

    void accelFlag(bool flag)
    {
        accelflag = flag;
    }

    void accelerator()
    {
        if (accelflag)
        {
            accel = 0.05f * MachineState["Accelerate"] * MachineState["Accelerate"];
            speed = Mathf.Min(speed, MachineState["Speed"] * 50 + 150);
        }
    }

    void turnAxis(float axis)
    {
        turnaxis = axis;
    }

    void turn()
    {

        if (speed < 10)
        {
            rotate = turnaxis * speed / 10.0f * MachineState["Grip"] / 3.0f;
        }
        else
        {
            rotate = turnaxis * MachineState["Grip"] / 3.0f;
        }
        friction += Mathf.Abs(turnaxis) / 1000.0f;
    }
}
