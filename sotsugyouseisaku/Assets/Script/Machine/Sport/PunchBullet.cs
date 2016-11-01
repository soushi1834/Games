using UnityEngine;
using System.Collections;

public class PunchBullet : MonoBehaviour {

    public float desTime;
    private GameObject mMachine;
    private float speed;

    // Use this for initialization
    void Start()
    {
        desTime = 3.0f;
        speed = 500;

        //Destroy(gameObject, desTime);

        if (mMachine.GetComponent<Machine>().speed < 0)
        {
            mMachine.GetComponent<Machine>().speed = 0;
        }
    }

    // Update is called once per fram
    void Update()
    {
        this.transform.position =
            mMachine.transform.position +
            new Vector3(30 * Mathf.Sin(mMachine.transform.eulerAngles.y * Mathf.Deg2Rad), 0, 30 * Mathf.Cos(mMachine.transform.eulerAngles.y * Mathf.Deg2Rad));
        this.transform.rotation = mMachine.transform.rotation;
        Vector3 vec = new Vector3(Mathf.Sin(mMachine.transform.eulerAngles.y * Mathf.Deg2Rad), 0, Mathf.Cos(mMachine.transform.eulerAngles.y * Mathf.Deg2Rad));
        mMachine.rigidbody.velocity += vec * speed;
        if (mMachine.GetComponentInChildren<Driver>().Name == "Player1")
        {
            SpeedUI.SpeedUp(speed);
        }
        if (mMachine.GetComponentInChildren<Driver>().Name == "Player2")
        {
            SpeedUI2P.SpeedUp(speed);
        }
        speed -= 1000 * Time.deltaTime;
        if (speed <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public int getDamage()
    {
        return 200;
    }

    public void setMachine(GameObject obj)
    {
        mMachine = obj;
    }
}
