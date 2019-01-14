using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	Joystick hook;
	public Transform Target;
	public float smooth;
	void Awake () {
		if(GameObject.FindGameObjectWithTag("Joystick Hook")!= null)
		hook = GameObject.FindGameObjectWithTag("Joystick Hook").GetComponent<Joystick>();
	}
	
	float size(float a, float b)
	{
		if(Mathf.Abs(a)>Mathf.Abs(b))
		return Mathf.Abs(a);
		else
		return Mathf.Abs(b);
	}
	void Update () {
		if(hook != null)
		gameObject.GetComponent<Camera>().orthographicSize = 4+ size(hook.Horizontal,hook.Vertical);
		Vector3 target = new Vector3(Target.position.x,Target.position.y,transform.position.z);
		transform.position = Vector3.Lerp(transform.position,target,smooth);
		
	}
}
