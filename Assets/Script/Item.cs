using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/Item",order = 2)]
[System.Serializable]
public class Item : ScriptableObject {
	public enum TypeEquip
	{
		Weapon,Hook,Torch,Recipe
	}
	public TypeEquip type;
	public string Name;
	public Sprite sprite;
	public int Dame;
	public int Duration;
	public bool canBuild;
	public bool canDuplicate;
	public int maxAmount;
	public GameObject prefab;
	
}
