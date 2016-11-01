using UnityEngine;
using System.Collections;

public class SelectParticipants : MonoBehaviour {

    private SelectState state;
    private int participants;
    private int players;

	// Use this for initialization
	void Start () {
        state = this.transform.parent.GetComponent<SelectState>();
        participants = 1;
        players = 1;
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
    void Update()
    {
        end();
        select();
        drawNumber();
    }

    void end()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            state.setParticipants(participants, players);
            state.next(SelectState.SELECTSTATE.SELECT_MACHINES);
            this.gameObject.SetActive(false);
        }
    }

    void select()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            participants++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            participants--;
        }
        participants = Mathf.Max(1, (Mathf.Min(participants, 2)));
    }

    void drawNumber()
    {
        this.gameObject.renderer.material.mainTextureOffset = new Vector2(participants / 10.0f, 0);
    }
}
