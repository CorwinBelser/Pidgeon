using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    public static string ANIMATOR_BOOL_MOVING = "isMoving";
    public static string ANIMATOR_BOOL_GROUNDED = "isGrounded";

    [SerializeField]
    private Rigidbody _rb;
	public GameObject PLAYER;
    public float SPEED; 				//Player speed
	public float JUMP_AMOUNT;
	public float MAXIMUM_VELOCITY;
	public CinemachineFreeLook CINE;
    
	public GameObject OTS_CAMERA;
    public Animator _animator;
    public LayerMask GROUND_CHECK_LAYER_MASK;

	public bool DisableMovementInFavorOfSomeOTherScript = false;

	void Awake(){
		Cursor.lockState = CursorLockMode.Locked;
	}

    void Start()
    {
//        _animator = GetComponent<Animator>();
    }

    void Update()
    {
		if (!DisableMovementInFavorOfSomeOTherScript) {
			if (Input.GetButtonDown ("Fire1")) {
				_rb.AddExplosionForce (JUMP_AMOUNT, PLAYER.transform.position, 1F, 1F);
			}
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
		if (!DisableMovementInFavorOfSomeOTherScript) {
			if (inputVertical > 0 || inputHorizontal != 0) {
				for (int i = 0; i < CINE.m_Orbits.Length; i++) {
					CINE.m_Orbits [i].m_Radius = Mathf.Lerp (CINE.m_Orbits [i].m_Radius, CINE._farOrbits [i], Time.deltaTime);
					CINE.m_Lens.FieldOfView = Mathf.Lerp (CINE.m_Lens.FieldOfView, 100, Time.deltaTime);
					//m_Orbits[i].m_Radius = _farOrbits[i] ;
				}
			} else if (inputVertical < 1 || inputHorizontal < 1 || inputHorizontal > -1) {
				for (int i = 0; i < CINE.m_Orbits.Length; i++) {
					CINE.m_Orbits [i].m_Radius = Mathf.Lerp (CINE.m_Orbits [i].m_Radius, CINE._defOrbits [i], Time.deltaTime);
					CINE.m_Lens.FieldOfView = Mathf.Lerp (CINE.m_Lens.FieldOfView, 50, Time.deltaTime);
					//m_Orbits[i].m_Radius = _defOrbits[i] ;
				}
			}
		
			_rb.velocity = new Vector3 (SPEED * direction.x, _rb.velocity.y, SPEED * direction.z);
			Vector3 look = new Vector3 (transform.position.x + direction.x, PLAYER.transform.position.y, transform.position.z + direction.z);
			transform.LookAt (look);
		
			_rb.velocity = Vector3.ClampMagnitude (_rb.velocity, MAXIMUM_VELOCITY);

		}
        /* if velocity is greater than a threshold, set the isMoving flag on the animator */
        if (_animator != null)
        {
            _animator.SetBool(ANIMATOR_BOOL_MOVING, _rb.velocity.sqrMagnitude > 1f);

            /* Shoot a raycast to determine if there the ground is below, setting the isGrounded flag on the animator */
            _animator.SetBool(ANIMATOR_BOOL_GROUNDED, Physics.Raycast(this.transform.position + Vector3.up, Vector3.down, 1.5f, GROUND_CHECK_LAYER_MASK));
        }

    }

}
