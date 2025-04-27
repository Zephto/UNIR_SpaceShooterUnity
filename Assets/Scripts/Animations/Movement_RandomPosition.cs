using UnityEngine;

///<summary>
///Makes a random movement to a 2d gameobject
///</summary>
public class Movement_RandomPosition: AnimationBase {

	#region General Settings
	///<summary>
	///Frequency with which the movement will be made
	///</summary>
	[SerializeField] private float frecuency;

	///<summary>
	///Velocity with which the object will move
	///</summary>
	[SerializeField] private float movementVelocity;

	///<summary>
	///Distance that the object will move in X vector
	///</summary>
	[SerializeField] private float xOffset;

	///<summary>
	///Distance that the object will move in Y vector
	///</summary>
	[SerializeField] private float yOffset;
	#endregion

	#region Internal variables
	///<summary>
	///Original position of the gameobject
	///</summary>
	private Vector3 originalPosition;
	#endregion

	private void Awake() {
		if(this.GetComponent<Transform>() != null){
			originalPosition = this.gameObject.transform.localPosition;
		}else if(this.GetComponent<RectTransform>() != null){
			originalPosition = this.gameObject.GetComponent<RectTransform>().anchoredPosition;
		}else{
			#if FIESTA_COMPONENT
			Debug.LogWarning("No type of Transform was detected. Make sure you have a Transform or RectTransform included in the object");
			#endif
		}
	}

	#region Abstract Methods
	///<inheritdoc cref="AnimationBase.AbstractStop"/>
	public override void AbstractInit(){}

	///<inheritdoc cref="AnimationBase.AbstractPlay"/>
	public override void AbstractPlay(){
		LeanTween.moveLocal(this.gameObject, ObtainRandomValue(), movementVelocity).setOnComplete(()=>{
			LeanTween.delayedCall(this.gameObject, frecuency, ()=> Play());
		});
	}

	///<inheritdoc cref="AnimationBase.AbstractStop"/>
	public override void AbstractStop(){
		LeanTween.cancel(this.gameObject);
	}
	#endregion

	#region Private Methods
	///<summary>
	///Returns a Vector2 with random values using offsets params
	///</summary>
	private Vector2 ObtainRandomValue(){
		return new Vector2(
			originalPosition.x + Random.Range(-xOffset, xOffset),
			originalPosition.y + Random.Range(-yOffset, yOffset)
		);
	}
	#endregion
}