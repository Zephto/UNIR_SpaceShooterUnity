using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Global component used to control the async load scene 
/// </summary>
public class SceneChanger : MonoBehaviour {
	
	public static SceneChanger Instance {get; private set;}

	void Awake() {
		if(Instance != null){
			Debug.Log("Este controlador de escenas ya existe, cudao pa >:v");
			Destroy(this.gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(this);
	}

	public void LoadSceneAsync(string sceneName){
		StartCoroutine(LoadSceneCoroutine(sceneName));
	}

	private IEnumerator LoadSceneCoroutine(string sceneName){
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

		while(!asyncOperation.isDone){
			Debug.Log($"Viajando a la siguiente escena {asyncOperation.progress}%");
			yield return null;
		}
	}
}
