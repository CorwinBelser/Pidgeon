using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonGrab : MonoBehaviour {
    private static string COLLECTIBLE_TAG = "Collectible";
    private List<Transform> _heldObjects;

    void Start()
    {
        _heldObjects = new List<Transform>();
    }

    void Update()
    {
        /* If the player presses Fire2, drop every item */
        if (Input.GetButtonDown("Fire2") && _heldObjects.Count != 0)
        {
            for (int i = _heldObjects.Count - 1; i >= 0; i--)
            {
                //Debug.Log("<color=blue>(Pigeon): Dropping " + _heldObjects[i].name + " </color>");
                _heldObjects[i].GetComponent<Collectible>().Drop();
                _heldObjects.RemoveAt(i);
            }
        }
    }
    
    void OnTriggerEnter(Collider coll)
    {
        //Debug.Log("<color=green>(Pigeon): Trying to pickup " + coll.name + "</color>");
        /* Check if the thing below is a collectible */
        if (coll.tag == COLLECTIBLE_TAG && !_heldObjects.Contains(coll.transform))
        {
            //Debug.Log("<color=green>    Tag matched! Picking up...</Color>");
            /* Tell the collectible to follow the last grabbed collectible */
            bool succeeded = false;
            if (_heldObjects.Count != 0)
                succeeded = coll.gameObject.GetComponent<Collectible>().Pickup(_heldObjects[_heldObjects.Count - 1]);
            else
                succeeded = coll.gameObject.GetComponent<Collectible>().Pickup(this.transform);
            
            /* Add the collectible to the list of held items */
            if (succeeded)
                _heldObjects.Add(coll.transform);
        }
        else
        {
            //Debug.Log("<Color=green>    Tag didn't match or already held</color>");
        }
    }
}
