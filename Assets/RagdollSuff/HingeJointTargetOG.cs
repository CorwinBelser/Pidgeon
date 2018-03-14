using UnityEngine;
using System.Collections;

public class HingeJointTargetOG : MonoBehaviour {

    public HingeJoint hj;
    public Transform target;
    [Tooltip("Only use one of these values at a time. Toggle invert if the rotation is backwards.")]
	public bool x, y, z, invert, flip180, flipOffsets;



	private Vector3 startRotationOffsets;
	void Start ()
    {
		startRotationOffsets = target.transform.localEulerAngles;
	}
	
	void Update ()
    {
        if (hj != null)
        {
            if (x)
            {
                JointSpring js;
                js = hj.spring;

				if (flipOffsets) {
					js.targetPosition = target.transform.localEulerAngles.z + startRotationOffsets.x;
				} else {
					js.targetPosition = target.transform.localEulerAngles.z - startRotationOffsets.x;
				}
                if (js.targetPosition > 180)
					js.targetPosition = js.targetPosition - 360;
				if (flip180)
					js.targetPosition -= 180;
                if (invert)
                    js.targetPosition = js.targetPosition * -1;

                js.targetPosition = Mathf.Clamp(js.targetPosition, hj.limits.min + 5, hj.limits.max - 5);

                hj.spring = js;
            }
            else if (y)
            {
                JointSpring js;
                js = hj.spring;
				if (flipOffsets) {
					js.targetPosition = target.transform.localEulerAngles.y + startRotationOffsets.y;
				} else {
					js.targetPosition = target.transform.localEulerAngles.y - startRotationOffsets.y;
				}
                if (js.targetPosition > 180)
                    js.targetPosition = js.targetPosition - 360;
				if (flip180)
					js.targetPosition -= 180;
                if (invert)
                    js.targetPosition = js.targetPosition * -1;
				
                js.targetPosition = Mathf.Clamp(js.targetPosition, hj.limits.min + 5, hj.limits.max - 5);

                hj.spring = js;
            }
            else if (z)
            {
                JointSpring js;
                js = hj.spring;
				if (flipOffsets) {
					js.targetPosition = target.transform.localEulerAngles.z + startRotationOffsets.z;
				} else {
					js.targetPosition = target.transform.localEulerAngles.z - startRotationOffsets.z;
				}
				Debug.Log ("Target Rot = " + js.targetPosition);
				if (js.targetPosition > 180) {
					js.targetPosition = js.targetPosition - 360;
					Debug.Log ("Constrain");
				}
				if (flip180)
					js.targetPosition -= 180;
				if (invert) {
					js.targetPosition = js.targetPosition * -1;
					Debug.Log ("Invert");
				}

                js.targetPosition = Mathf.Clamp(js.targetPosition, hj.limits.min + 5, hj.limits.max - 5);
				Debug.Log ("Final Rot = " + js.targetPosition);

                hj.spring = js;
            }
        }
    }
}
