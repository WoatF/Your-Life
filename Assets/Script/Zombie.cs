using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : heal {
	[Header("Info")]
	public int damage;
	Transform player;
	public float near = 0,far = 5,stop = 0.25f,range = 1,delayAttack = 1,speed = 1,radius = 0.25f;
	float distance,rate,slowSpeed,originSpeed;
	public Transform attackPos;
	public LayerMask layerMaskAttack;
	//Animator animator;
	new void Start()
	{	
		originSpeed = speed;
		slowSpeed =  speed/2;
		base.Start();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		animator = GetComponent<Animator>();
	}
	protected override void UpdateInChild()
	{	
		if(!base.onPlant )
				{	
					speed = slowSpeed;
				}
				else
				{
					speed = originSpeed;
				}

		distance = Vector2.Distance(transform.position,player.position);
		if(distance < far && beAttack == false && die == false)
		{
			
			if(distance <near){
				transform.position = Vector2.MoveTowards(transform.position,player.position,-speed*Time.deltaTime);
				animator.SetBool("Run",true);
				}
		
			else if(distance>stop)
			{
				transform.position = Vector2.MoveTowards(transform.position,player.position,speed*Time.deltaTime);
				animator.SetBool("Run",true);
			}
		

			else if(distance>near && distance <stop)
			{
				transform.position = this.transform.position;
				animator.SetBool("Run",false);
			}
			int x = player.position.x > transform.position.x ? 1:-1;
			transform.localScale = new Vector2(x,transform.localScale.y);
			Physics2D.IgnoreCollision(GetComponent<Collider2D>(),player.GetComponent<Collider2D>());

		}
		else if(distance >far)
		{
			animator.SetBool("Run",false);
		}
		if(distance < range &&	rate <= 0)
		{
			StartCoroutine(NearAttack(0.1f));
			Debug.Log("attack by enemy");
			rate = delayAttack;
		}
		if(rate > 0)
		rate -= Time.deltaTime;
	}
	IEnumerator NearAttack(float time)
	{

		yield return new WaitForSeconds(time);

		Collider2D target = Physics2D.OverlapCircle(attackPos.position,radius,layerMaskAttack);
		if(target!= null)
		target.GetComponent<heal>().TakeDamage(damage,transform);
	}
		
	void OnCollsionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			Debug.Log("collider");
		}
	}
	void OnDrawGizmos()
	{
		Gizmos.color =Color.red;
		Gizmos.DrawWireSphere(attackPos.position,radius);
	}
}
