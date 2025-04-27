using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class TransitionScreen : MonoBehaviour {
	
	public static TransitionScreen Instance {get; private set;}

	#region Public References
	[SerializeField] private Image imageComponent;
	#endregion

	#region Private references
	private Canvas thisCanvas;
	#endregion

	#region Private variables
	private RectTransform canvasRect;
	private RectTransform imageRect;
	private Vector2 screenSize;
	#endregion

	void Awake() {
		if(Instance != null){
			Debug.Log("Ya existe el transition screen we :v");
			Destroy(this.gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(this.gameObject);

		thisCanvas = this.GetComponent<Canvas>();
		canvasRect = thisCanvas.GetComponent<RectTransform>();
		imageRect	= imageComponent.GetComponent<RectTransform>();
		screenSize = canvasRect.sizeDelta;
	}

	void Start() {
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Alpha1)) StartIn();
		if(Input.GetKeyDown(KeyCode.Alpha2)) StartOut();
		if(Input.GetKeyDown(KeyCode.Alpha3)) In(null);
		if(Input.GetKeyDown(KeyCode.Alpha4)) Out(null);
	}

	#region Public Methods
	public void StartIn(){
		Debug.Log("[Start In transition]");
		imageRect.anchoredPosition = Vector3.zero;
	}

	public void StartOut(){
		Debug.Log("[Start Out transition]");
		imageRect.anchoredPosition = new Vector2(0, screenSize.y + imageRect.sizeDelta.y);
	}

	public void In(Action callback){
		Debug.Log("[In transition]");

		StartOut();
		LeanTween.moveLocalY(imageComponent.gameObject, 0, 0.5f)
		.setEaseInOutSine()
		.setOnComplete(()=> callback?.Invoke());
	}

	public void Out(Action callback){
		Debug.Log("[Out transition]");

		StartIn();
		LeanTween.moveLocalY(imageComponent.gameObject, -(screenSize.y + imageRect.sizeDelta.y), 0.5f)
		.setEaseInOutSine()
		.setOnComplete(()=> callback?.Invoke());
	}
	#endregion
}
