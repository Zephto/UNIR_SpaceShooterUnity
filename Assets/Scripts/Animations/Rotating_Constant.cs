using UnityEngine;

///<summary>
///Component used to rotate a 2d object constantly (only move the Z axis)
///</summary>
public class Rotating_Constant: AnimationBase {

	#region General Setings
	///<summary>
	///The time to do the rotation
	///</summary>
	[Header("General settings")]
	[SerializeField] private float time = 1.0f;

	///<summary>
	///Int variable used to set the quantity of loops of the animation, can be set to -1 if you need to set a infinite loop.
	///</summary>
	[Tooltip("Use -1 if need a infinity loop")]
	[SerializeField] private int loopCount = -1;

	///<summary>
	///The final degrees to rotate
	///</summary>
	[Tooltip("If is true the direction of the rotation is to clock hand.")]
	[SerializeField] private bool clockHandDirection = true;
	#endregion

	#region Consts
	///<summary>
	///Degrees to make the constant rotation.
	///</summary>
	private int degrees = 180;
	#endregion

	#region Abstract Methods
	///<inheritdoc cref="AnimationBase.AbstractInit()"/>
	public override void AbstractInit(){
		LeanTween.rotateAround(this.gameObject,clockHandDirection?Vector3.back:Vector3.forward,360,time).setLoopClamp();
	}

	///<inheritdoc cref="AnimationBase.AbstractPlay()"/>
	public override void AbstractPlay(){
		LeanTween.rotateAround(this.gameObject,clockHandDirection?Vector3.back:Vector3.forward,360,time).setLoopClamp();
	}

	///<inheritdoc cref="AnimationBase.AbstractStop()"/>
	public override void AbstractStop(){
		LeanTween.cancel(this.gameObject);
	}
	#endregion

	#region Methods
	///<summary>
	///Set the necessary values if need to do by code
	///</summary>
	///<param name="_behaviourType">		<inheritdoc cref="behaviourType" 		path="/summary"/></param>
	///<param name="_time">					<inheritdoc cref="time" 				path="/summary"/></param>
	///<param name="_loopCount">			<inheritdoc cref="loopCount" 			path="/summary"/></param>
	///<param name="_clockHandDirection">	<inheritdoc cref="clockHandDirection" 	path="/summary"/></param>
	public void SetValues(BehaviourType _behaviourType, float _time, int _loopCount, bool _clockHandDirection){
		this.behaviourType		= _behaviourType;
		this.time 				= _time;
		this.loopCount 			= _loopCount;
		this.clockHandDirection	= _clockHandDirection;
	}
	#endregion
}