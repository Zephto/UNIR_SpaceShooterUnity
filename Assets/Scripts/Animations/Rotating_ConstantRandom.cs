using UnityEngine;

///<summary>
///Makes a random rotation to a 2d gameobject
///</summary>
public class Rotating_ConstantRandom: AnimationBase {
	
	#region General Settings
	///<summary>
	///Frequency with which the rotation will be made
	///</summary>
	[Header("General Settings")]
	[SerializeField] private float frecuency;

	///<summary>
	///Velocity with which the object will rotate
	///</summary>
	[SerializeField] private float rotateVelocity;

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
			#if UNITY_EDITOR
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
		LeanTween.rotateLocal(this.gameObject, ObtainRandomValue(), rotateVelocity).setOnComplete(()=>{
			LeanTween.delayedCall(this.gameObject, frecuency, ()=> Play());
		}).setEaseInOutSine();
	}

	///<inheritdoc cref ="AnimationBase.AbstractStop"/>
	public override void AbstractStop(){
		LeanTween.cancel(this.gameObject);
	}

	///<summary>
	///Returns a Vector3 with a z random value using min-max params
	///</summary>
	private Vector3 ObtainRandomValue(){
		return new Vector3(
			originalRotation.x,
			originalRotation.y,
			originalRotation.z + Random.Range(minAngle, maxAngle)
		);
	}
	#endregion
}
