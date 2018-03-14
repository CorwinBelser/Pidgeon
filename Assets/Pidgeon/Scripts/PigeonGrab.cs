using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonGrab : MonoBehaviour {

    public static string ANIMATOR_BOOL_GRABBING = "isGrabbing";
    public LayerMask CollectibleLayer;
    public float GrabRadius;

    private Transform _heldObject;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        /* If the player presses Fire2, drop every item */
        if (Input.GetButtonDown("Fire2") && _heldObject != null)
        {
            _heldObject.GetComponent<Collectible>().Drop(_heldObject.position, 0f);
            _heldObject = null;
            if (_animator != null)
                _animator.SetBool(ANIMATOR_BOOL_GRABBING, false);
        }
        else if (Input.GetButtonDown("Fire2") && _heldObject == null)
        {
            Collider[] cols = Physics.OverlapSphere(this.transform.position, GrabRadius, CollectibleLayer);
            if (cols.Length != 0)
            {
                Debug.Log("Found " + cols.Length + " objects near the feet");
                /* Only pickup the first thing */
                foreach (Collider col in cols)
                {
                    if (col.tag == Collectible.COLLECTIBLE_TAG)
                    {
                        if (col.gameObject.GetComponent<Collectible>().Pickup(this.transform))
                            _heldObject = col.transform;
                    }
                }
            }
        }
    }
}
