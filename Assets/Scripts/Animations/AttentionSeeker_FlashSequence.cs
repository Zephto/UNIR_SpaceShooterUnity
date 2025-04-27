using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

///<summary>
///Component used to make a fade sequence animation to an array of images.
///</summary>
public class AttentionSeeker_FlashSequence: AnimationBase {

	#region Public variables
	[Header("Settings")]
	///<summary>
	/// On state duration
	///</summary>
	[SerializeField] private float OnStateDuration;

	///<summary>
	/// Off state duration
	///</summary>
	[SerializeField] private float OffStateDuration;

	///<summary>
	/// Alpha value On state
	///</summary>
	[SerializeField] private float alphaOnValue;

	///<summary>
	/// Alpha value Off state
	///</summary>
	[SerializeField] private float alphaOffValue;

	///<summary>
	///List of the images gameobject references
	///</summary>
	[Tooltip("Image components that will be affected by flash effect")]
	[SerializeField] private List <GameObject> imagesObjects;
	#endregion

	#region Internal variables
	///<summary>
	/// Images RectTransform components
	///</summary>
	private List <RectTransform> rectTransforms = new List<RectTransform>();

	///<summary>
	/// Images Image components
	///</summary>
	private List <Image> imageComponents = new List<Image>();

	///<summary>
	///
	///</summary>
	private Coroutine seekerCoroutine;

	///<summary>
	/// Images list indexNext for turning on
	///</summary>
	private int lastIndex = 0;

	///<summary>
	/// Images list indexNext for turning off
	///</summary>
	private int nextIndex = 1;
	
	///<summary>
	/// IsRunnig
	///</summary>
	bool IsRunnig=false;
	#endregion

	void Awake(){
		foreach(var item in imagesObjects){
			imageComponents.Add(item.GetComponent<Image>());
			rectTransforms.Add(item.GetComponent<RectTransform>());
		}
	}

	public override void Start() {
		base.Start ();
		rectTransforms.ForEach(image =>
			LeanTween.alpha(image, rectTransforms[0]==image?1f:alphaOffValue,0.0f)
		);

	}

	public override void OnDestroy() {
		// if(seekerCoroutine != null) StopCoroutine(seekerCoroutine);
		if (rectTransforms.Count > 0)
			rectTransforms.ForEach(image => LeanTween.cancel(image));
		LeanTween.cancel(this.gameObject);
		if(seekerCoroutine!=null)
		StopCoroutine(seekerCoroutine);
	}

	#region Abstract Methods
	///<inheritdoc cref="AnimationBase.AbstractInit"/>
	public override void AbstractInit(){}

	///<inheritdoc cref="AnimationBase.AbstractPlay"/Value>
	public override void AbstractPlay() {
		base.Stop();

		seekerCoroutine=StartCoroutine(Animationtest());
		// LeanTween.alpha(rectTransforms[lastIndex], alphaOnValue, 0f).setOnComplete(()=>{
		// 	LeanTween.delayedCall(this.gameObject, OnStateDuration, ()=>{
		// 		LeanTween.alpha(rectTransforms[lastIndex], alphaOffValue, 0f)
		// 		.setUseFrames((timeBehaviourType == TimeBehaviourType.BY_FRAMES));

		// 		LeanTween.alpha(rectTransforms[nextIndex], alphaOnValue, 0f).setOnComplete(()=>{
		// 			LeanTween.delayedCall(this.gameObject, OffStateDuration, ()=>{
		// 				Debug.Log("LastIndex"+lastIndex);
		// 				Debug.Log("NextIndex"+nextIndex);
		// 				lastIndex ++;
		// 				nextIndex++;
		// 				if(lastIndex >= rectTransforms.Count) lastIndex =0;
		// 				if(nextIndex >= rectTransforms.Count) nextIndex =0;
		// 				Play();
		// 			});
		// 		}).setUseFrames((timeBehaviourType == TimeBehaviourType.BY_FRAMES));
		// 	});
			
		// }).setUseFrames((timeBehaviourType == TimeBehaviourType.BY_FRAMES));



		// LeanTween.delayedCall(this.gameObject, OnStateDuration, ()=>{

		// 	LeanTween.alpha(rectTransforms[lastIndex], alphaOffValue, 0f)
		// 	.setUseFrames((timeBehaviourType == TimeBehaviourType.BY_FRAMES));

		// 	LeanTween.alpha(rectTransforms[nextIndex], alphaOnValue, 0f).setOnComplete(()=>{
		// 		LeanTween.delayedCall(this.gameObject, OffStateDuration, ()=>{
		// 			lastIndex ++;
		// 			nextIndex++;
		// 			if(lastIndex >= rectTransforms.Count) lastIndex =0;
		// 			if(nextIndex >= rectTransforms.Count) nextIndex =0;
		// 			Play();
		// 		}).setUseFrames((timeBehaviourType == TimeBehaviourType.BY_FRAMES));
		// 	}).setUseFrames((timeBehaviourType == TimeBehaviourType.BY_FRAMES));
		// }).setUseFrames((timeBehaviourType == TimeBehaviourType.BY_FRAMES));
	}

	IEnumerator Animationtest(){
		bool canChange=false;
		bool canRepeat=false;
		IsRunnig=true;
		
		yield return new WaitForSeconds(OnStateDuration);
		yield return new WaitForEndOfFrame();

		LeanTween.alpha(rectTransforms[lastIndex], alphaOffValue, 0f)
		.setUseFrames((timeBehaviourType == TimeBehaviourType.BY_FRAMES));

		LeanTween.alpha(rectTransforms[nextIndex], alphaOnValue, 0f).setOnComplete(()=>{
			canChange=true;
		}).setUseFrames((timeBehaviourType == TimeBehaviourType.BY_FRAMES));

		yield return new WaitUntil(()=>canChange);
		yield return new WaitForEndOfFrame();

		LeanTween.delayedCall(this.gameObject, OffStateDuration, ()=>{
				lastIndex ++;
				nextIndex++;
				if(lastIndex >= rectTransforms.Count) lastIndex =0;
				if(nextIndex >= rectTransforms.Count) nextIndex =0;
				canRepeat=true;
		}).setUseFrames((timeBehaviourType == TimeBehaviourType.BY_FRAMES));
		
		yield return new WaitUntil(()=> canRepeat);
		yield return new WaitForEndOfFrame();
		Play();
	}
	///<inheritdoc cref ="AnimationBase.AbstractStop"/>
	public override void AbstractStop() {
		LeanTween.cancel(this.gameObject);
		if(seekerCoroutine!=null){
			IsRunnig=false;
			StopCoroutine(seekerCoroutine);
		} 
	}
	#endregion
}