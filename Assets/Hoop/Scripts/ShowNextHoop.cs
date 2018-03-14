using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNextHoop : MonoBehaviour, IReceiveTriggerFromChildren {

	public ShowNextHoop NextHoop; /* The next hoop to activate */
	public bool FirstHoop; /* Is this the first hoop in a sequence */

	void Awake()
	{
		if (!FirstHoop)
		{
			/* Hide hoop */
			this.gameObject.SetActive(false);
		}
	}

	void Start()
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
		SetHoopCollider(true);

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
			/* Make this hoop a collectible */
			Collectible c = this.GetComponent<Collectible>();
			Rigidbody rb = this.GetComponent<Rigidbody>();
			if (c != null && rb != null)
			{
				c.enabled = true;
				rb.useGravity = true;
				SetHoopCollider(false);
				this.enabled = false;
				SetHoopShape(true);

			}
		}
    }

    public void OnChildTriggerExit(Collider col){}

    public void OnChildTriggerStay(Collider col){}

	private void SetHoopCollider(bool toState)
	{
		MeshCollider[] meshColliders = this.GetComponentsInChildren<MeshCollider>();
		foreach (MeshCollider mc in meshColliders)
		{
			if (mc.name == "hoopCollider")
				mc.gameObject.SetActive(toState);
		}
	}

	private void SetHoopShape(bool toState)
	{
		MeshCollider[] meshColliders = this.GetComponentsInChildren<MeshCollider>();
		foreach (MeshCollider mc in meshColliders)
		{
			if (mc.name == "hoopShape")
				mc.convex = true;
		}
	}	
}
