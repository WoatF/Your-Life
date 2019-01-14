using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory/Recipe")]
public class Recipe : ScriptableObject {

	public Item input01;
	public Item input02;
	public int count = 1;
	public Item result;


}
