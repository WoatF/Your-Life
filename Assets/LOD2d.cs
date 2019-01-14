using System.Collections;
using UnityEngine;

public class LOD2d : MonoBehaviour {

	void OnBecameVisible()
	{
		gameObject.SetActive(true);
	}
	void OnBecameInvisible()
	{
		gameObject.SetActive(false);
	}
}
