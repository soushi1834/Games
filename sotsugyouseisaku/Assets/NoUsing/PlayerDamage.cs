using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour {

    public GameObject Sparks;
    bool activeSpark;
    float time;

    void Start()
    {
        Sparks = Instantiate((GameObject)Resources.Load("Sparks"), this.transform.position, this.transform.rotation) as GameObject;
        Sparks.transform.parent = this.transform;
        Sparks.SetActive(false);

        activeSpark = false;
        time = 0;
    }

    void Update()
    {
        if (activeSpark && time < 0.2f)
        {
            time += Time.deltaTime;
        }
        else
        {
            Sparks.SetActive(false);
            activeSpark = false;
            time = 0;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        string othersTag = other.gameObject.tag;

        if (othersTag == "Player" || othersTag == "CPU")
        {
            Sparks.SetActive(true);
            activeSpark = true;
        }
    }

    /*
    void OnTriggerExit(Collider other)
    {
        string othersTag = other.gameObject.tag;

        if (othersTag == "Player" || othersTag == "CPU")
        {
            Sparks.SetActive(false);
            activeSpark = false;
        }
    }
     * */
}
