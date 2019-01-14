using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal : MonoBehaviour {
	public enum Type{Human,Enemy}
	public Type type;
	public float sizeBody = 0.5f;
	public int Health = 30;
	[HideInInspector]
	public Animator animator;
	[HideInInspector]
	public bool die,beAttack,onPlant;
	[SerializeField]
	LayerMask layerMaskGround;
	SpriteMask WaterMask;
	SpriteRenderer sr;

	public void Start () {
		sr = GetComponent<SpriteRenderer>();

		die = false;
		if(GetComponent<Animator>()!= null)
		{
			animator = GetComponent<Animator>();
		}
		WaterMask = transform.Find("WaterMask").GetComponent<SpriteMask>();
		WaterMask.enabled = false;
		
	}
	// Update is called once per frame
	void Update () {
		
		sr.sortingOrder = (int)-(transform.position.y*100-3);

		if(Health <= 0)
		{
			Health = 0;
			if(animator != false)
			animator.SetBool("Die",true);
			die = true;
		}
		UpdateInChild();
		onPlant = Physics2D.OverlapCircle(transform.position+new Vector3(0,0.1f,0f),sizeBody,layerMaskGround);

		WaterMask.enabled =!onPlant;
	}
	public void TakeDamage(int heal,Transform original)
	{
		if(Health > 0)
		{
			switch(type)
			{
			case Type.Human:
			GameManager.instance.Shake();
			break;
			case Type.Enemy:
			break;
			}
			
			Health -= heal;
		}
		StartCoroutine(Knockback(original));
	}
	IEnumerator Knockback(Transform obj)
	{	
		beAttack = true;
		float x = 0.1f;
		while(x>0)
		{
			transform.position = Vector2.MoveTowards(transform.position,obj.position,-2*Time.deltaTime);
			x -= Time.deltaTime;
			yield return null;
		}
		beAttack = false;

	}
	protected virtual void UpdateInChild(){} 
}
