using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MySceneManager: MonoBehaviour {

	private static int currentLevel = 0;

	//Method called when promoting level i.e. we hit the Goal Object
	public static void PromoteLevel(){
		SceneManager.LoadScene(currentLevel + 1);
		currentLevel++;
	}

	void Start() {
		DontDestroyOnLoad (gameObject);
	}
}
