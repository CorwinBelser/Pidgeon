using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    [SerializeField]
    private Rigidbody _rb;
	public GameObject PLAYER;
    public float SPEED; 				//Player speed
	public float JUMP_AMOUNT;
    
	public GameObject OTS_CAMERA;

    void Update()
    {
		
       if(Input.GetButtonDown("Fire1")){
		   _rb.AddExplosionForce(JUMP_AMOUNT,PLAYER.transform.position,1F,1F);
	   }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxis("MoveHorizontal");
        float inputVertical = Input.GetAxis("MoveVertical");

		Vector3 cameraForward = OTS_CAMERA.transform.forward;
		Vector3 cameraRight = OTS_CAMERA.transform.right;
		cameraForward.y = 0f;
		cameraRight.y = 0f;
		cameraForward.Normalize();
		cameraRight.Normalize();


		Vector3 direction = cameraForward * inputVertical + cameraRight * inputHorizontal;
		direction = direction.normalized;
		_rb.velocity = new Vector3 (SPEED * direction.x, _rb.velocity.y, SPEED * direction.z);
		Vector3 look = new Vector3 (transform.position.x + direction.x, PLAYER.transform.position.y, transform.position.z + direction.z);
		PLAYER.transform.LookAt (look);

    }

}
