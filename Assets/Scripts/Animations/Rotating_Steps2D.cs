using UnityEngine;

///<summary>
///Makes a gameobject to rotate to an specific angles
///</summary>
public class Rotating_Steps2D: AnimationBase {
	
	#region Parameters
	///<sumary>
	///Frecuency with which the rotation will be made
	///</summary>
	public float frecuency;

	///<summary>
	///Velocity with which the object will rotate
	///</summary>
	public float rotateVelocity;

	///<summary>
	///The direction of the angle to which the object will be rotated.
	///</summary>
	public float angleDirection;
	#endregion

	#region Internal variables
	///<summary>
	///Original rotation of the gameobject
	///</summary>
	private Vector3 originalRotation;

	///<summary>
	///New rotation of the gameobject
	///</summary>
	private Vector3 newRotation;
	#endregion

	private void Awake() {
		if(this.GetComponent<Transform>() != null){
			originalRotation = gameObject.transform.eulerAngles;
			newRotation = originalRotation;
		
		}else if(this.GetComponent<RectTransform>() != null){
			RectTransform rectTransform = GetComponent<RectTransform>();
			originalRotation = new Vector3(
				rectTransform.localRotation.x,
				rectTransform.localRotation.y,
				rectTransform.localRotation.z
			);
			newRotation = originalRotation;

		}else{
			#if FIESTA_COMPONENT
			Debug.LogWarning("No type of Transform was detected. Make sure you have a Transform or RectTransform included in the object");
			#endif
		}
	}

	#region Abstract Methods
	///<inheritdoc cref="AnimationBase.AbstractInit"/>
	public override void AbstractInit(){

	}

	///<inheritdoc cref="AnimationBase.AbstractPlay"/>
	public override void AbstractPlay(){
		LeanTween.rotateLocal(gameObject, ObtainNewPosition(), rotateVelocity).setOnComplete(()=>{
			LeanTween.delayedCall(gameObject, frecuency, ()=> Play());
		}).setEaseInOutSine();
	}

	///<inheritdoc cref="AnimationBase.AbstractStop"/>
	public override void AbstractStop(){
		LeanTween.cancel(this.gameObject);
	}
	#endregion

	#region Private Methods
	///<summary>
	///Obtain the next step rotation
	///</summary>
	private Vector3 ObtainNewPosition(){
		newRotation.z += angleDirection;
		if(Mathf.Abs(newRotation.z) >= 360f) newRotation.z = originalRotation.z;
		
		return newRotation;
	}
	#endregion
}