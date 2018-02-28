using UnityEngine;
using System.Collections;

public class HingeJointTarget : MonoBehaviour {

    public HingeJoint hj;
    public Transform target;
    [Tooltip("Only use one of these values at a time. Toggle invert if the rotation is backwards.")]
    public bool x, y, z, invert;
	public bool log = false;

	void Start ()
    {

	}
	
	void Update ()
    {
        if (hj != null)
        {
            if (x)
            {
                JointSpring js;
                js = hj.spring;
                js.targetPosition = target.transform.localEulerAngles.x;
				if (log) {
					Debug.Log(js.targetPosition);
				}
//                if (js.targetPosition > 180)
//                    js.targetPosition = js.targetPosition - 360;
                if (invert)
                    js.targetPosition = js.targetPosition * -1;

                js.targetPosition = Mathf.Clamp(js.targetPosition, hj.limits.min + 5, hj.limits.max - 5);

                hj.spring = js;
            }
            else if (y)
            {
                JointSpring js;
                js = hj.spring;
                js.targetPosition = target.transform.localEulerAngles.y;
				if (log) {
					Debug.Log(js.targetPosition);
				}
//                if (js.targetPosition > 180)
//                    js.targetPosition = js.targetPosition - 360;
                if (invert)
                    js.targetPosition = js.targetPosition * -1;

                js.targetPosition = Mathf.Clamp(js.targetPosition, hj.limits.min + 5, hj.limits.max - 5);

                hj.spring = js;
            }
            else if (z)
            {
                JointSpring js;
                js = hj.spring;
                js.targetPosition = target.transform.localEulerAngles.z;
				if (log) {
					Debug.Log(js.targetPosition);
				}
//                if (js.targetPosition > 180)
//                    js.targetPosition = js.targetPosition - 360;
                if (invert)
                    js.targetPosition = js.targetPosition * -1;

                js.targetPosition = Mathf.Clamp(js.targetPosition, hj.limits.min + 5, hj.limits.max - 5);

                hj.spring = js;
            }
        }
    }
}
