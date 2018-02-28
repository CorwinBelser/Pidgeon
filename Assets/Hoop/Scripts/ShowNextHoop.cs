using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNextHoop : MonoBehaviour, IReceiveTriggerFromChildren {

	public ShowNextHoop NextHoop; /* The next hoop to activate */
	public bool FirstHoop; /* Is this the first hoop in a sequence */

	void Awake()
	{
		if (FirstHoop)
		{
			ShowHoop();
		}
	}

	public void ShowHoop()
	{
		/* Make this hoop full alpha */
		Renderer rend = this.GetComponentInChildren<Renderer>();
		Color col = rend.material.color;
		col.a = 1f;
		rend.material.color = col;

		/* Make the next hoop appear at half-alpha */
		if (NextHoop != null)
		{
			NextHoop.gameObject.SetActive(true);
			Renderer nextRend = NextHoop.GetComponentInChildren<Renderer>();
			Color nextCol = nextRend.material.color;
			nextCol.a = .1f;
			nextRend.material.color = nextCol;
		}

	}

    public void OnChildTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && NextHoop != null)
		{
			NextHoop.ShowHoop();
			/* Hide this hoop */
			this.gameObject.SetActive(false);
		}
    }

    public void OnChildTriggerExit(Collider col){}

    public void OnChildTriggerStay(Collider col){}
}
