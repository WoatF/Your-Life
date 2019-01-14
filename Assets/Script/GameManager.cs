using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public Text fpsText;
	public float deltaTime;
	shakeCamera shake;
	public bool building;
	public Transform buttonBuilding;
	public GameObject panelExit;
	void Awake () {
		instance = this;
	}
	void Start() 
	{	
		Application.targetFrameRate = 120;
		building = true;
		shake = GameObject.FindObjectOfType<shakeCamera>();
		changeBuid();
		
	}
	public void Shake()
	{
		shake.DoShake();
	}
	public void changeBuid()
	{
		building = !building;
		buttonBuilding.GetChild(0).gameObject.SetActive(!building);
		buttonBuilding.GetChild(1).gameObject.SetActive(building);
		if(!building)
		GameObject.FindObjectOfType<Build>().clearPreview();
		else
		if(ItemHandle.PickItemObj != null)
		GameObject.FindObjectOfType<Build>().updatePreview(ItemHandle.PickItemObj.item.sprite);
	}
	
	// Update is called once per frame
	void Update () {
		
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
         float fps = 1.0f / deltaTime;
         fpsText.text = Mathf.Ceil (fps).ToString();
		 if(Input.GetKeyDown(KeyCode.Escape))
		 {
			 panelExit.SetActive(true);
		 }
	}
	public void ChangeScene(string name)
	{
		SceneManager.LoadScene(name);
	}
}
