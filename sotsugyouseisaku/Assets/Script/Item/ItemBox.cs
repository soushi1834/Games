using UnityEngine;
using System.Collections;

public class ItemBox : MonoBehaviour {

    private GameObject obj;
    private float waittime;
    //追加
    private AudioSource source;
    public AudioClip itemSE;

    static string[] str =
        {
            "Beam",
            "Beam",
            "Beam",
            "Beam",
            "Beam",
            "Beam",
            "Makibishi",
            "Makibishi",
            "Makibishi",
            "Makibishi",
            "Makibishi",
            "Makibishi",
            "Makibishi",
            "Dash",
            "Dash",
            "Dash",
            "Dash",
            "Dash",
            "Barrier",
            "Barrier",
            "StateUP",
            "StateUP",
            "RepairItem",
            "AbilityHack"
        };

    // Use this for initialization
    void Start()
    {
        waittime = 0;
        //追加
        source = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waittime > 0)
        {
            waittime -= Time.deltaTime;
        }
        else
        {
            this.transform.FindChild("mat1").gameObject.SetActive(true);
            this.transform.FindChild("mat2").gameObject.SetActive(true);
            this.GetComponent<SphereCollider>().enabled = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" ||
            col.gameObject.tag == "CPU")
        {
            Driver d = col.gameObject.GetComponentInChildren<Driver>();
            string s = str[Random.Range(0, str.Length)];
            obj = (GameObject)Resources.Load(s);
            d.getItem(obj);
            this.transform.FindChild("mat1").gameObject.SetActive(false);
            this.transform.FindChild("mat2").gameObject.SetActive(false);
            this.GetComponent<SphereCollider>().enabled = false;
            waittime = 2;
            //追加
            source.PlayOneShot(itemSE);
        }
    }
}
