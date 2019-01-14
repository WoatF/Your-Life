using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
public class AiEnemy : heal {
	[SerializeField]
	float speed,range,angle;
	[SerializeField]
	[Header("Attack")]
	Transform attackPos;
	[SerializeField]
	float radius;
	[SerializeField]
	int damage;
	[SerializeField]
	LayerMask layerMaskAttack;
	Vector3 dir;
	GameObject target;
	new void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player");
		base.Start();
	}
	[Task]
	void move(float range)
	{
		if(dir == Vector3.zero)
		{
			float x = Random.Range(-range,range);
			float y = Random.Range(-range,range);
			dir =transform.position + new Vector3(x,y,0);
			if(x > 0)
			transform.localScale =new Vector3(1,1,1);
			else
			transform.localScale = new Vector3(-1,1,1);
		}
		Vector3 destination =  dir;
            Vector3 delta = (destination - transform.position);
            Vector3 velocity = speed*delta.normalized;

            transform.position = transform.position + velocity * Time.deltaTime;

            Vector3 newDelta = (destination - transform.position);
            float d = newDelta.magnitude;

            if (Task.isInspected)
			{
				Task.current.debugInfo = string.Format("d={0:0.000}", d);
				
			}          

            if ( Vector3.Dot(delta, newDelta) <= 0.0f || d < 1e-3)
            {				
                transform.position = destination;
                Task.current.Succeed();
                d = 0.0f;
				dir = Vector2.zero;
                Task.current.debugInfo = "d=0.000";		
            }
	}
	[Task]
	bool beenAttack()
	{
		bool bol = base.beAttack;
		return bol;

	}
	[Task]
	void MoveToEnemy()
	{		
			Vector3 destination =  target.transform.position;
			if(destination.x > transform.position.x)
			transform.localScale =new Vector3(1,1,1);
			else
			transform.localScale = new Vector3(-1,1,1);
            Vector3 delta = (destination - transform.position);
            Vector3 velocity = speed*delta.normalized;

            transform.position = transform.position + velocity * Time.deltaTime;

            Vector3 newDelta = (destination - transform.position);
            float d = newDelta.magnitude;

            if (Task.isInspected)
                Task.current.debugInfo = string.Format("d={0:0.000}", d);
		
            if ( Vector3.Dot(delta, newDelta) <= 0.0f || d < 1e-3)
            {				
                transform.position = destination;
                Task.current.Succeed();
                d = 0.0f;
				dir = Vector2.zero;
                Task.current.debugInfo = "d=0.000";
            }
	}
	[Task]
	void attack()
	{
		Collider2D target = Physics2D.OverlapCircle(attackPos.position,radius,layerMaskAttack);
		if(target!= null)
		target.GetComponent<heal>().TakeDamage(damage,transform);
		Task.current.Succeed();
		
	}
	[Task]
	bool IsClosestAndCanAttack(float range)
	{
		bool closest = false;
		float dis = Vector2.Distance(target.transform.position,transform.position);
		if(dis < range)
		{
			closest = true;
		}
		
		return closest;
	}

	IEnumerator NearAttack(float time)
	{

		yield return new WaitForSeconds(time);

		Collider2D target = Physics2D.OverlapCircle(attackPos.position,radius,layerMaskAttack);
		if(target!= null)
		target.GetComponent<heal>().TakeDamage(damage,transform);
		Task.current.Succeed();
	}
	[Task]
	bool Alive()
	{
		bool beAlive = !die;
		return beAlive;
	}
	[Task]
	void setAnimation(string s,bool b)
	{	
		
		animator.SetBool(s,b);
		if(animator.GetBool(s) == b)
		Task.current.Succeed();
	}
	void OnDrawGizmos()
	{
		Gizmos.color =Color.red;
		Gizmos.DrawWireSphere(attackPos.position,radius);
	}
}
