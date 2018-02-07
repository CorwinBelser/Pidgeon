using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    [SerializeField]
    private Rigidbody _rb;
	public GameObject PLAYER;
    public float SPEED; 				//Player speed
	public float JUMP_AMOUNT;
	public float MAXIMUM_VELOCITY;
	public CinemachineFreeLook CINE;
    
	public GameObject OTS_CAMERA;
	
	void Awake(){
		Cursor.lockState = CursorLockMode.Locked;
	}

    void Update()
    {
       if(Input.GetButtonDown("Fire1")){
		   _rb.AddExplosionForce(JUMP_AMOUNT,PLAYER.transform.position,1F,1F);
	   }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxisRaw("MoveHorizontal");
        float inputVertical = Input.GetAxisRaw("MoveVertical");

		Vector3 cameraForward = OTS_CAMERA.transform.forward;
		Vector3 cameraRight = OTS_CAMERA.transform.right;
		cameraForward.y = 0f;
		cameraRight.y = 0f;
		cameraForward.Normalize();
		cameraRight.Normalize();


		Vector3 direction = cameraForward * inputVertical + cameraRight * inputHorizontal;
		direction = direction.normalized;
		if(inputVertical > 0 || inputHorizontal != 0){
			for(int i=0; i < CINE.m_Orbits.Length; i++){
				CINE.m_Orbits[i].m_Radius = Mathf.Lerp(CINE.m_Orbits[i].m_Radius, CINE._farOrbits[i], Time.deltaTime);
				CINE.m_Lens.FieldOfView = Mathf.Lerp(CINE.m_Lens.FieldOfView, 100, Time.deltaTime);
				//m_Orbits[i].m_Radius = _farOrbits[i] ;
			}
		}
		else if(inputVertical < 1 || inputHorizontal < 1 || inputHorizontal > -1){
			for(int i=0; i < CINE.m_Orbits.Length; i++){
				CINE.m_Orbits[i].m_Radius = Mathf.Lerp(CINE.m_Orbits[i].m_Radius, CINE._defOrbits[i], Time.deltaTime);
				CINE.m_Lens.FieldOfView = Mathf.Lerp(CINE.m_Lens.FieldOfView, 50, Time.deltaTime);
				//m_Orbits[i].m_Radius = _defOrbits[i] ;
			}
		}
		_rb.velocity = new Vector3 (SPEED * direction.x, _rb.velocity.y, SPEED * direction.z);
		Vector3 look = new Vector3 (transform.position.x + direction.x, PLAYER.transform.position.y, transform.position.z + direction.z);
		transform.LookAt (look);
		
		_rb.velocity = Vector3.ClampMagnitude(_rb.velocity,MAXIMUM_VELOCITY);

    }

}
