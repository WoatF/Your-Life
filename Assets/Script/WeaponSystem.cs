using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponSystem : MonoBehaviour {
	[System.Serializable]
	public class Weapon
	{
		
		public string Name;
		public Sprite sprite;
		public int dame;
		public int duration;
	}
	public Weapon myWeapon;
	public Weapon punch;
	private float timeBtwAttack;
	private float startTimeBtwAttack = 1;
	public enum typeEquip{Weapon,Hook,Torch,Recipe}	
	public typeEquip type;
	public Transform attackPos;
	public LayerMask layerMask;
	public LayerMask layerBuild;
	public float radiusAttack;
	
	Transform hand;
	GameObject hook,attack;
	inventory inventory;
	Joystick joystick;
	Movement movement;
	void Start ()
    {

        inventory = GameObject.FindObjectOfType<inventory>();

        hand = transform.GetChild(0);

        hook = GameObject.FindGameObjectWithTag("Joystick Hook");

        attack = GameObject.FindGameObjectWithTag("Attack Button");

        joystick = hook.GetComponent<Joystick>();

        hook.SetActive(false);

        movement = GameObject.FindObjectOfType<Movement>();

        SetPunch();

		myWeapon = punch;
    }

    private void SetPunch()
    {
        punch = new Weapon();
        punch.dame = 1;
        punch.duration = 1000;
        punch.Name = "punch";
        punch.sprite = null;
    }

    void Update () {

		if(CrossPlatformInputManager.GetButtonDown("Attack"))
		{	
			if(Movement.target == null ){
				if(type == typeEquip.Recipe)
				{
					Instantiate(ItemHandle.PickItem.prefab,GameObject.FindGameObjectWithTag("InstanceUI").transform);
				}
				else
				if( hand.GetComponent<Animation>().IsPlaying("attack") == false)
				if(GameManager.instance.building == false)
				Attack();
				else
				if(ItemHandle.PickItem.canBuild == true && ItemHandle.PickItem != null)
				Building(ItemHandle.PickItem,ItemHandle.PickItemObj);
				
			}
			else
			{	
				Debug.Log(inventory.GetItem(Movement.target).name);

				inventory.AddItem(inventory.GetItem(Movement.target),Movement.target);
												
			}
				
				
				
		}
		if(ItemHandle.PickItemObj!= null)
		{
			hand.GetComponent<SpriteRenderer>().sprite = myWeapon.sprite;
		}
		else

			hand.GetComponent<SpriteRenderer>().sprite = null;

			hand.GetComponent<SpriteRenderer>().sortingOrder = transform.GetComponentInParent<SpriteRenderer>().sortingOrder+1;

		if(Mathf.Abs(joystick.Horizontal)>0 && Mathf.Abs(joystick.Vertical) > 0){
			LookAt(joystick.Direction);
		}
		}
	void LookAt(Vector3 pos)
	{
		float x = transform.position.x + pos.x;
		float y = transform.position.y + pos.y;
		Vector3 diff = new Vector3(x,y,0) - transform.position;
         diff.Normalize();
 
         float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		 if(movement.facingRight == false)
         transform.rotation = Quaternion.Euler(0f, 0f, rot_z );
		 else
		 transform.rotation = Quaternion.Euler(0f, 0f, rot_z+180 );
		 
	}
	public void UseWeapon(Item item)
	{	
		if(item != null)
		{
			myWeapon.Name = item.Name;
			myWeapon.sprite = item.sprite;
			myWeapon.dame = item.Dame;
			myWeapon.duration = item.Duration;
			string a = item.type.ToString();
			if(a == "Weapon")			
				type = typeEquip.Weapon;
			else if(a == "Hook")
				type = typeEquip.Hook;
			else if(a == "Torch")
				type = typeEquip.Torch;
			else if(a== "Recipe")
				type = typeEquip.Recipe;
			Debug.Log(a);
			
			
		}

		else if(item == null)
		{
			myWeapon = punch;
			type = typeEquip.Weapon;
			SetPunch();
		}
		transform.GetChild(1).gameObject.SetActive(false);
		attack.SetActive(true);
		hook.SetActive(false);
		switch(type){
				
				case typeEquip.Hook:
				hook.SetActive(true);
				attack.SetActive(false);				
				break;
				case typeEquip.Weapon:
				
				break;
				case typeEquip.Torch:

				transform.GetChild(1).gameObject.SetActive(true);
				
				break;
				case typeEquip.Recipe:
					
				break;
			}
		hand.eulerAngles = Vector3.zero;

		hand.parent.eulerAngles = Vector3.zero;
		
		Debug.Log("Use: "+myWeapon.Name);
	}
	void Building(Item item,ItemDisplay obj)
	{	
		Build build = GameObject.FindObjectOfType<Build>();
		if(item == true && obj.count>=1)
		{
			obj.count--;

			build.Building();

			if(obj.count<=0)
			{
				Destroy(obj.gameObject);
				build.clearPreview();
			}
			

		}
	}
	
	
	public void Attack()
	{	
		Collider2D[] hitToEnemy = Physics2D.OverlapCircleAll(attackPos.position,radiusAttack,layerMask);
		if(hitToEnemy.Length!=0)
		for(int x = 0; x<hitToEnemy.Length; x++)
		{	
			if(hitToEnemy[x].tag == "Enemy")
			{
				hitToEnemy[x].GetComponent<heal>().TakeDamage(myWeapon.dame,transform.parent);
				Debug.Log(-myWeapon.dame+"to enemy");
			}else
			{
				TakeDame(hitToEnemy[x].gameObject,myWeapon.dame);
				StartCoroutine(Shake(hitToEnemy[x].transform,5,0.1f));
			}
			
		}
		hand.GetComponent<Animation>().Play("attack");
	}
	
	void TakeDame(GameObject obj,float dame)
	{
		obj.GetComponent<HealOfGround>().heal -= dame;
	}
	
	IEnumerator Shake(Transform obj,float range, float time)
	{	
		Vector3 pos = obj.position;
		while(time >0)
		{	
			if(obj!= null)
			obj.position = pos + new Vector3(Mathf.Sin(Time.time* range)*time,0);

			time -= Time.deltaTime;
			yield return null;
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		
		Gizmos.DrawWireSphere(attackPos.position,radiusAttack);

	}
}
