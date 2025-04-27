using UnityEngine;
using System.Collections;
using MyBox;

///<summary>
///Makes a seesaw rotation animation on a RectTransform.
///</summary>
public class Rotating_Seesaw: AnimationBase {
	
	#region General Settings
	///<summary>
	///Frequency with which the rotation will be made
	///</summary>
	[Header("General Settings")]
	[SerializeField] private float frequency;

	///<summary>
	///Duration of the animation
	///</summary>
	[SerializeField] private float movementDuration;

	///<summary>
	///Minimum angle to make the rotation
	///</summary>
	[SerializeField] private float minAngle;

	///<summary>
	///Maximum angle to make the rotation
	///</summary>
	[SerializeField] private float maxAngle;
	#endregion

	#region Internal variables
	///<summary>
	///Original rotation of the gameobject
	///</summary>
	private Vector3 originalRotation;
	#endregion

	private void Awake() {
		if(this.GetComponent<Transform>() != null){
			originalRotation = this.gameObject.transform.eulerAngles;
		}else if(this.GetComponent<RectTransform>() != null){
			originalRotation = this.gameObject.GetComponent<RectTransform>().eulerAngles;
		}else{
			#if FIESTA_COMPONENT
			Debug.LogWarning("No type of Transform was detected. Make sure you have a Transform or RectTransform included in the object");
			#endif
		}
	}
	
	#region Abstract Methods
	///<inheritdoc cref="AnimationBase.AbstractInit()"/>
	public override void AbstractInit(){

	}

	///<inheritdoc cref="AnimationBase.AbstractPlay"/>
	public override void AbstractPlay(){

		//TODO: Improve this script, need to use only 2 fps, .setFrameRate not work in this way
		LeanTween.value(this.gameObject, Rotate(minAngle), Rotate(maxAngle), movementDuration)
		.setOnUpdateVector3((value)=> this.gameObject.transform.localEulerAngles = value)
		.setUseFrames(timeBehaviourType == TimeBehaviourType.BY_FRAMES)
		.setOnComplete(()=>{
			minAngle *= -1;
			maxAngle *= -1;

			LeanTween.delayedCall(this.gameObject, frequency, ()=> Play());
		});
	}

	///<inheritdoc cref ="AnimationBase.AbstractStop"/>
	public override void AbstractStop(){
		LeanTween.cancel(this.gameObject);
	}

	///<summary>
	///Returns a Vector3 with a z custom value using min-max params
	///</summary>
	private Vector3 Rotate(float zValue){
		return new Vector3(
			originalRotation.x,
			originalRotation.y,
			zValue
		);
	}
	#endregion
}
