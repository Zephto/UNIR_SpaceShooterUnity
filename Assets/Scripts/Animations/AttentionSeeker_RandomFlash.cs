using UnityEngine;
using UnityEngine.UI;

///<summary>
///Makes a random alpha to an image
///</summary>
[RequireComponent(typeof (Image))]
public class AttentionSeeker_RandomFlash: AnimationBase {
	#region General Settings
	///<summary>
	///Frequency with which the movement will be made
	///</summary>
	[SerializeField] private float frecuency;

	///<summary>
	///Velocity with which the object will move
	///</summary>
	[SerializeField] private float timeToAlpha;

	///<summary>
	///Minimum angle to make the rotation
	///</summary>
	[Range(0, 1)]
	[SerializeField] private float minAlpha;

	///<summary>
	///Maximum angle to make the rotation
	///</summary>
	[Range(0, 1)]
	[SerializeField] private float maxAlpha;
	#endregion

	#region Internal variables
	///<summary>
	///Rect Transform reference of the image
	///</summary>
	private RectTransform imageRectTransform;
	#endregion

	private void Awake() {
		imageRectTransform = gameObject.GetComponent<Image>().rectTransform;
	}

	public override void OnDestroy() {
		base.OnDestroy();
		Stop();
	}

	#region Abstract Methods
	///<inheritdoc cref="AnimationBase.AbstractInit()"/>
	public override void AbstractInit(){
		
	}

	///<inheritdoc cref="AnimationBase.Play"/>
	public override void AbstractPlay(){
		LeanTween.alpha(imageRectTransform, ObtainRandomValue(), timeToAlpha).setOnComplete(()=>{
			LeanTween.delayedCall(this.gameObject, frecuency, ()=> Play());
		});
	}

	///<inheritdoc cref="AnimationBase.Stop"/>
	public override void AbstractStop(){
		LeanTween.cancel(this.gameObject);
		LeanTween.cancel(imageRectTransform);
	}

	///<summary>
	///Returns a random value for alpha
	///</summary>
	private float ObtainRandomValue(){
		return Random.Range(minAlpha, maxAlpha);
	}
	#endregion
}