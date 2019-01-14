using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {
	public GameObject Inventory;
	public static Transform setParent;
	public static bool canUse;
	void Start () {

		setParent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		canUse = !Inventory.activeInHierarchy;
	}
}
