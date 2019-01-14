using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneSetup : MonoBehaviour {
	[SerializeField]
	private RectTransform child,background;
	
	void Start () {
		Caching.ClearCache();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void LoadScene(string name){
		
		StartCoroutine(AsynchronousLoad(name));
		
	}
	

IEnumerator AsynchronousLoad (string scene)
{
 yield return null;
 
 AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
 ao.allowSceneActivation = false;
 background.parent.gameObject.SetActive(true);
	while (! ao.isDone)
	{
	// [0, 0.9] > [0, 1]
	float progress = Mathf.Clamp01(ao.progress / 0.9f);
	Debug.Log("Loading progress: " + (progress * 100) + "%");
	child.anchoredPosition = new Vector2(progress*100*background.sizeDelta.x/100,child.anchoredPosition.y);
	// Loading completed
	if (ao.progress == 0.9f)
	{
	Debug.Log("Press a key to start");
	ao.allowSceneActivation = true;
	
	
	}
	
	yield return null;
	}
 background.parent.gameObject.SetActive(false);
}
}
