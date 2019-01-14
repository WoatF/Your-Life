using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/Unlock Recipe",order = 3)]
public class UnlockRecipe : ScriptableObject {
	[System.Serializable]
	public struct Point
	{
		public int max;
		public int min;

	}
	public Point[] points;
	public Recipe[] recipe;
}
