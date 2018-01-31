using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Collectible : MonoBehaviour {

    public static string COLLECTIBLE_TAG = "Collectible";

    public float FOLLOW_VELOCITY = 2.5f;
    public float MAX_VELOCITY = 15f;
    public float MAX_FORCE = 40f;
    public float GAIN = 5f;
    public float PICKUP_COOLDOWN = 5f;
    private Vector3 _spawnPosition;
    private float _respawnMagnitude = 1000f;
    public float RESPAWN_HEIGHT = 5f;
    private Transform _target;
    private float _timeDropped = -5f;
    private Rigidbody _rigidbody;

    private Vector3 _dropTarget;
    private float _dropDelay;

    void Start()
    {
        _spawnPosition = this.transform.position;
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (_target != null)
        {
            /* Taken from https://answers.unity.com/answers/195790/view.html */
            Vector3 distance = _target.position - this.transform.position;
            Vector3 targetVelocity = Vector3.ClampMagnitude(FOLLOW_VELOCITY * distance, MAX_VELOCITY);
            Vector3 error = targetVelocity - _rigidbody.velocity;
            Vector3 force = Vector3.ClampMagnitude(GAIN * error, MAX_FORCE);
            
            /* Apply the force */
            _rigidbody.AddForce(force);
        }
        else if (this.transform.position.sqrMagnitude > _respawnMagnitude)
        {
            /* Respawn the object, as it is likely out of bounds */
            this.transform.position = new Vector3(_spawnPosition.x, _spawnPosition.y + RESPAWN_HEIGHT, _spawnPosition.z);
            /* Clear out any existing forces */
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }

    public bool Pickup(Transform target)
    {
        if (Time.time - _timeDropped > PICKUP_COOLDOWN)
        {
            Debug.Log("<color=blue>    (Collectible): " + this.name + " now following " + target.name + "</color>");
            _target = target;
            return true;
        }
        Debug.Log("<color=green>    (Collectible): Collectible is on cooldown and can't be picked up</color");
        return false;
    }

    public void Drop(Vector3 dropTarget, float dropDelay)
    {
        _dropTarget = dropTarget;
        _dropDelay = dropDelay;
        this.StartCoroutine("MoveAndDrop");
    }

    IEnumerator MoveAndDrop()
    {
        Debug.Log("<color=blue>(Collectible " + this.name + "): Waiting for " + _dropDelay + " seconds to drop</color>");
        /* wait before dropping */
        yield return new WaitForSeconds(_dropDelay);

        /* Teleport the object to the drop point */
        this.transform.position = _dropTarget;
        /* Clear out any existing forces */
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _timeDropped = Time.time;
        _target = null;
    }
}
