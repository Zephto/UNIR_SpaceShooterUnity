using UnityEngine.Events;
using UnityEngine;
using MyBox;

///<summary>
///Component used to make a ping pong between two numbers
///</summary>
public class Numbers_VectorPingPong: AnimationBase {
	#region Public variables
	///<summary>
	///Initial float number when start the animation
	///</summary>
	[Header("General Settings")]
	[SerializeField] private Vector3 fromVector = Vector3.zero;

	///<summary>
	///Final float number where the animation finish
	///</summary>
	[SerializeField] private Vector3 toVector = Vector3.one;

	///<summary>
	///Current value of the transition
	///</summary>
	[ReadOnly] public Vector3 currentVector = Vector3.zero;
	[Space]

	///<summary>
	/// Int variable used to set the quantity of loops of the animation, can be set to -1 if you need to set a infinite loop.
	///</summary>
	[Tooltip("Sets -1 to make a infinite loop")]
	[Header("Time settings")]
	[SerializeField] private int loopCount = -1;

	///<summary>
	///Total time to complete the animation
	///</summary>
	[Tooltip("Time in seconds that will takes the animation until finish and loop")]
	[SerializeField] private float duration = 1f;

	///<summary>
	///Delay time when animation finish
	///<summary>
	[Tooltip("Set this variable if you want a delay when animation finish")]
	[SerializeField] private float delay = 0f;

	///<summary>
	///Leantween type, used to set the tween animation of the number change velocity
	///</summary>
	[SerializeField] private LeanTweenType tweenType = LeanTweenType.easeInOutSine;
	#endregion

	#region Private variables
	///<summary>
	///Reference of the fromNumber variable
	///</summary>
	private Vector3 fromVectorReference = Vector3.zero;

	///<summary>
	///Reference of the toNumber variable
	///</summary>
	private Vector3 toVectorReference = Vector3.one;

	///<summary>
	///Reference of the current value of loop count
	///</summary>
	private int currentLoopCount = 1;
	#endregion

	#region Events
	///<summary>
	///Event when number change his value
	///</summary>
	[Space]
	[SerializeField] public UnityEvent<Vector3> OnValueChange;
	#endregion

	public override void Start() {
		fromVectorReference = fromVector;
		toVectorReference	= toVector;
		base.Start();
	}

	#region Abstract Methods
	///<inheritdoc cref="AnimationBase.AbstractInit()"/>
	public override void AbstractInit(){}

	///<inheritdoc cref="AnimationBase.AbstractPlay()"/>
	public override void AbstractPlay(){
		InitializeVariables();
		NumberTransition();
	}

	///<inheritdoc cref="AnimationBase.AbstractStop()"/>
	public override void AbstractStop(){
		LeanTween.cancel(this.gameObject);
	}

	///<summary>
	///Gets the current value in currentNumber variable
	///</summary>
	public Vector3 GetCurrentNumber() => currentVector;
	#endregion

	#region Private Methods
	///<summary>
	///Initialize necessary variables to start the animation
	///</summary>
	private void InitializeVariables(){
		fromVectorReference = fromVector;
		toVectorReference	= toVector;
		currentLoopCount = 0;
	}

	///<summary>
	///Start the number transition animation
	///</summary>
	private void NumberTransition(){
		LeanTween.value(this.gameObject, fromVectorReference, toVectorReference, duration)
		.setEase(tweenType)
		.setLoopPingPong(loopCount)
		.setOnUpdateVector3((value) => {
			currentVector = value;
			OnValueChange.Invoke(currentVector);
		})
		.setOnComplete(()=>{
			var tempNumber		= fromVectorReference;
			fromVectorReference = toVectorReference;
			toVectorReference	= tempNumber;

			if(loopCount == -1){
				LeanTween.delayedCall(this.gameObject, delay, ()=> NumberTransition());
			}else{
				currentLoopCount++;

				if(currentLoopCount < loopCount){
					LeanTween.delayedCall(this.gameObject, delay, ()=> NumberTransition());
				}else{
					events?.Invoke();
				}
			}
		});
	}
	#endregion

}