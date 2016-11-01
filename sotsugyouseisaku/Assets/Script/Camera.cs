using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    private Transform target;
    private Quaternion[] rotation;

	// Use this for initialization
	void Start () {
        transform.Rotate(new Vector3(0, 180, 0));
        rotation = new Quaternion[6];
        for (int i = 0; i < 6; i++)
        {
            rotation[i] = this.transform.rotation;
        }
	}

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameObject.FindWithTag("Player") == null)
            return;

        target = GameObject.FindWithTag("Player").transform;
        Vector3 cameraPos = new Vector3(
            (target.position.x + -50 * Mathf.Sin(target.transform.eulerAngles.y * Mathf.Deg2Rad)),
            (target.position.y + 20),
            (target.position.z + -50 * Mathf.Cos(target.transform.eulerAngles.y * Mathf.Deg2Rad)));
        //transform.position = Vector3.Lerp(transform.position, cameraPos, Time.deltaTime * 60);
        transform.position = cameraPos;
        //transform.rotation = rotation[0];
        transform.rotation = target.rotation;
        transform.Rotate(new Vector3(0, 00, 0));
        /*
        for (int i = 0; i < rotation.Length - 1; i++)
        {
            rotation[i] = rotation[i + 1];
        }
        rotation[rotation.Length - 1] = target.rotation;
         * */
	}
}
