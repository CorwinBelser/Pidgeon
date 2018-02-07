using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PigeonAudio : MonoBehaviour {

    public AudioClip LOOP_AUDIO;
    public AudioClip COLLISION_AUDIO;
    private AudioSource _audioSource;
    private float _musicPauseTime;

	// Use this for initialization
	void Start () {
        _audioSource = this.GetComponent<AudioSource>();
	}

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag != Collectible.COLLECTIBLE_TAG)
            this.StartCoroutine("PlayCollisionSoundEffect");
    }

    private IEnumerator PlayCollisionSoundEffect()
    {
        if (_audioSource.clip == LOOP_AUDIO)
            _musicPauseTime = _audioSource.time;

        /* Play the collision sound effect */
        _audioSource.Stop();
        _audioSource.PlayOneShot(COLLISION_AUDIO);

        yield return new WaitForSeconds(COLLISION_AUDIO.length);

        _audioSource.Stop();
        _audioSource.clip = LOOP_AUDIO;
        _audioSource.time = _musicPauseTime;
        _audioSource.Play();
    }
}
