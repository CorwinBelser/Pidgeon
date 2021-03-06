﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Collectible : MonoBehaviour {

    public static string COLLECTIBLE_TAG = "Collectible";

    public float FOLLOW_VELOCITY = 25f;
    public float MAX_VELOCITY = 50f;
    public float MAX_FORCE = 100f;
    public float GAIN = 25f;
    public float PICKUP_COOLDOWN = 5f;
    private Vector3 _spawnPosition;
    private float _respawnMagnitude = 1000f;
    public float RESPAWN_HEIGHT = 5f;
    private Transform _target;
    private float _timeDropped = -5f;
    private Rigidbody _rigidbody;

    private Vector3 _dropTarget;
    private float _dropDelay;

    public AudioClip COLLECTED_MUSIC;
    public AudioClip COLLISION_SOUND;
    private AudioSource _audioSource;
    private float _musicPauseTime;
	
	public Transform GetTarget(){
		return _target;
	}

    void Start()
    {
        _spawnPosition = this.transform.position;
        _rigidbody = this.GetComponent<Rigidbody>();
        _audioSource = this.GetComponent<AudioSource>();
        _audioSource.Stop();
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
        else if (this.transform.position.y < -1000f)
        {
            /* Respawn the object, as it is likely out of bounds */
            this.transform.position = new Vector3(_spawnPosition.x, _spawnPosition.y + RESPAWN_HEIGHT, _spawnPosition.z);
            /* Clear out any existing forces */
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        this.StartCoroutine("PlayCollisionSoundEffect");
    }

    private IEnumerator PlayCollisionSoundEffect()
    {
        if (_audioSource.clip == COLLECTED_MUSIC)
            _musicPauseTime = _audioSource.time;

        /* Play the collision sound effect */
        _audioSource.Stop();
        _audioSource.PlayOneShot(COLLISION_SOUND);

        yield return new WaitForSeconds(COLLISION_SOUND.length);

        _audioSource.Stop();
        _audioSource.clip = COLLECTED_MUSIC;
        _audioSource.time = _musicPauseTime;
        if (_target != null)
            _audioSource.Play();
    }

    public bool Pickup(Transform target)
    {
        if (Time.time - _timeDropped > PICKUP_COOLDOWN)
        {
            Debug.Log("<color=blue>    (Collectible): " + this.name + " now following " + target.name + "</color>");
            _target = target;

            /* Play this collectible's music, if any */
            _audioSource.clip = COLLECTED_MUSIC;
            _audioSource.Play();

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

        /* Stop any music being played */
        _audioSource.Stop();

        _timeDropped = Time.time;
        _target = null;
    }
}
