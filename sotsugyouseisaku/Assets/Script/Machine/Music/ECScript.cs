using UnityEngine;
using System.Collections;

public class ECScript : MonoBehaviour {
    public float speed = 15.0f;
    public float desTime = 15.0f;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, desTime);
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(0, 0,  10);
    }
}
