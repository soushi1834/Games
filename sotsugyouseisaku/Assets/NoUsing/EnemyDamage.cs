using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour {

    public GameObject Sparks;

    void OnTriggerEnter(Collider other)
    {
        string othersTag = other.gameObject.tag;

        if (othersTag == "CPU" || othersTag == "Player")
        {
            Sparks.SetActive(true);
        }

        if (othersTag == "CPU" || othersTag == "CPU")
        {
            Sparks.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        string othersTag = other.gameObject.tag;

        if (othersTag == "CPU" || othersTag == "Player")
        {
            Sparks.SetActive(false);
        }

        if (othersTag == "CPU" || othersTag == "CPU")
        {
            Sparks.SetActive(false);
        }
    }
}
