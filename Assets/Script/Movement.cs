using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : heal {
	[Header("Infomation")]
	public float speed;
	[HideInInspector]
	public Animator myAni;
	[Space]
	[Header("Hook")]
	public float range;
	public float speedReload;
	LineRenderer line;
	Rigidbody2D rid ;
	[HideInInspector]
	public bool facingRight,useHook;
	Joystick joystick;
	Joystick hook;
	public static GameObject target;
	void Awake () {

		joystick = GameObject.FindGameObjectWithTag("Joystick Player").GetComponent<Joystick>();
		if(GameObject.FindGameObjectWithTag("Joystick Hook")!= null)
		hook = GameObject.FindGameObjectWithTag("Joystick Hook").GetComponent<Joystick>();

		rid = GetComponent<Rigidbody2D>();

		myAni = GetComponent<Animator>();

		line = transform.Find("Hook").GetComponent<LineRenderer>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

			target = FindGameObjectNearest();			

			Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.up * joystick.Vertical);			
			
			if(moveVector.x>0) facingRight = true;

			else if(moveVector.x < 0) facingRight = false;

			transform.localScale = new Vector2(facingRight ? 1:-1,1);

			myAni.SetBool("Run",rid.velocity != Vector2.zero?true:false);

			if(beAttack == false)
			{	
				rid.velocity = moveVector*speed;
				if(!base.onPlant && useHook == false)
				{	
					rid.drag = 50;
				}
				else
				{
					rid.drag = 0;
				}

				if(hook != null)
				{
					Vector3 HookVector = (Vector3.right * hook.Horizontal + Vector3.up * hook.Vertical);
				Vector3 BlinkPos = transform.position - HookVector*range;

				Debug.DrawLine(transform.position,BlinkPos,Color.green);
				if(Mathf.Abs(HookVector.x) > 0 || Mathf.Abs(HookVector.y) > 0)
				{
					line.SetPosition(0,line.transform.position);
					line.SetPosition(1,BlinkPos);
				}
				else if((Mathf.Abs(HookVector.x) == 0 || Mathf.Abs(HookVector.y) == 0) && useHook == false )
				{
					line.SetPosition(0,transform.position);
					line.SetPosition(1,transform.position);
				}
				}
				
				if(Mathf.Abs(moveVector.x) > 0 || Mathf.Abs(moveVector.y) > 0)
				{
					StopAllCoroutines();
					useHook = false;
				}
			}else rid.velocity = Vector2.zero;
			
			
			
	}
	public GameObject FindGameObjectNearest()
	{
		GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Item");
        GameObject closest = null;
        float distance = 0.5f;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
       
	}
	public void StartHook(Vector3 pos)
	{	if(useHook == false)
		StartCoroutine(MoveHook(pos));
		line.transform.GetChild(0).GetComponent<arrow>().SetRotation();
	}
	IEnumerator MoveHook(Vector3 pos)
	{	
		useHook = true;

		float x = 3;

		while(x > 0)
		{	
			line.SetPosition(0,line.transform.position);
			line.SetPosition(1,pos);
			transform.position = Vector2.Lerp(transform.position,pos,1*Time.deltaTime);
			x -= Time.deltaTime;
			yield return null;
		}
		useHook = false;
		
	}
	void OnDrawGizmos()
	{
		Gizmos.color = Color.grey;
		Gizmos.DrawWireSphere(transform.position + new Vector3(0,0.1f,0),sizeBody);
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		
	}	
	void OnTriggerExit2D(Collider2D col)
	{
		
	}
}

