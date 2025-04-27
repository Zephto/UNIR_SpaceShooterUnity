using UnityEngine;
using UnityEngine.Rendering;

///<summary>
///Component used to make bouncing grouth animation, used to highlight an element
///</summary>
public class AttentionSeeker_Pulse: AnimationBase {
	
	#region General Settings
	///<summary>
	///Float variable used to set the quantity of loops of the animation, can be set to -1 if you need to set a infinite loop.
	///</summary>
	[Tooltip("Sets -1 to make a infinite loop")]
	[SerializeField] private int loopCount = -1;

	///<summary>
	///Vector reference of the intiial scale to start the pulse animation
	///</summary>
	[SerializeField] private Vector3 initialScale = Vector3.one;

	///<summary>
	///Vector reference of the final scale to make the pulse animation
	///</summary>
	[SerializeField] private Vector3 toScale = Vector3.one;
	#endregion

	#region Pulse parameters
	///<summary>
	///Time to set the in animation (Growth effect)
	///</summary>
	[Header("In variables")]
	[SerializeField] private float time = 1.0f;

	///<summary>
	///Leantween type, used to set the tween animation of growth effect
	///</summary>
	[SerializeField] private LeanTweenType tweenType = LeanTweenType.linear;
	#endregion

	#region Methods
	///<summary>
	///Sets by code the parameters of the Pulse animation.
	///</summary>
	///<remarks>If you set the parameters by code, the behaviour type change to MANUAL</remarks>
	///<param name="_behaviourType">	<inheritdoc cref="behaviourType"	path="/summary"/></param>
	///<param name="_loopCount">		<inheritdoc cref="loopCount" 		path="/summary"/></param>
	///<param name="_initialScale">		<inheritdoc cref="initialScale" 	path="/summary"/></param>
	///<param name="_toScale">			<inheritdoc cref="toScale" 			path="/summary"/></param>
	///<param name="_time">				<inheritdoc cref="time" 			path="/summary"/></param>
	///<param name="_tweenType">		<inheritdoc cref="tweenType" 		path="/summary"/></param>
	public void SetParameters(int _loopCount, Vector3 _initialScale, Vector3 _toScale, float _time, LeanTweenType _tweenType){
		this.behaviourType 	= BehaviourType.MANUAL;
		this.loopCount		= _loopCount;
		this.initialScale	= _initialScale;
		this.toScale		= _toScale;
		this.time			= _time;
		this.tweenType		= _tweenType;
	}
	#endregion
	
	#region Abstract Methods
	///<inheritdoc cref="AnimationBase.AbstractInit"/>
	public override void AbstractInit(){

	}

	///<inheritdoc cref="AnimationBase.AbstractPlay"/>
	public override void AbstractPlay(){
		//OnDemandRendering.renderFrameInterval=1;

		if(LeanTween.isTweening(this.gameObject)) Stop();

		LeanTween.scale(this.gameObject, toScale, time)
		.setEase(tweenType)
		.setLoopCount(loopCount)
		.setLoopPingPong();

		if(loopCount != -1){
			InvokeEvents();
		}
	}

	///<inheritdoc cref="AnimationBase.AbstractStop"/>
	public override void AbstractStop() {
		LeanTween.cancel(this.gameObject);
		this.transform.localScale = initialScale;
	}
	#endregion
}
