using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonGrab : MonoBehaviour {

    public static string ANIMATOR_BOOL_GRABBING = "isGrabbing";

    private List<Transform> _heldObjects;
    private Animator _animator;

    void Start()
    {
        _heldObjects = new List<Transform>();
        _animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        /* If the player presses Fire2, drop every item */
        if (Input.GetButtonDown("Fire2") && _heldObjects.Count != 0)
        {
            for (int i = 0; i < _heldObjects.Count; i++)
            {
                //Debug.Log("<color=blue>(Pigeon): Dropping " + _heldObjects[i].name + " </color>");
                _heldObjects[i].GetComponent<Collectible>().Drop(_heldObjects[0].position, 0.5f * i);
            }
            _heldObjects.Clear();
            if (_animator != null)
                _animator.SetBool(ANIMATOR_BOOL_GRABBING, false);
        }
    }
    
    void OnTriggerEnter(Collider coll)
    {
        //Debug.Log("<color=green>(Pigeon): Trying to pickup " + coll.name + "</color>");
        /* Check if the thing below is a collectible */
        if (coll.tag == Collectible.COLLECTIBLE_TAG && !_heldObjects.Contains(coll.transform))
        {
            //Debug.Log("<color=green>    Tag matched! Picking up...</Color>");
            /* Tell the collectible to follow the last grabbed collectible */
            bool succeeded = false;
            /*if (_heldObjects.Count != 0)
                succeeded = coll.gameObject.GetComponent<Collectible>().Pickup(_heldObjects[_heldObjects.Count - 1]);
            else*/
                succeeded = coll.gameObject.GetComponent<Collectible>().Pickup(this.transform);
            
            /* Add the collectible to the list of held items */
                if (succeeded)
                {
                    _heldObjects.Add(coll.transform);
                    if (_animator != null)
                        _animator.SetBool(ANIMATOR_BOOL_GRABBING, true);
                }
        }
        else
        {
            //Debug.Log("<Color=green>    Tag didn't match or already held</color>");
        }
    }
}
