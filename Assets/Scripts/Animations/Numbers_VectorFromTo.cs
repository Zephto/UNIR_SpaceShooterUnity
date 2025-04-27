using UnityEngine.Events;
using UnityEngine;
using MyBox;

///<summary>
///Component used to make a single transition of two numbers
///</summary>
public class Numbers_VectorFromTo: AnimationBase {
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
	[ReadOnly] public Vector3 currentVectorValue = Vector3.zero;
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
	[SerializeField] public float duration = 1f;

	///<summary>
	///Delay time when animation finish
	///<summary>
	[Tooltip("Set this variable if you want a delay when animation finish")]
	[SerializeField] private float delay = 0f;


	///<summary>
	///Leantween type, used to set the tween animation of the number change velocity
	///</summary>
	[SerializeField] private LeanTweenType tweenType = LeanTweenType.easeInOutSine;
	
	///<summary>
	///Modify curve animation
	///<summary>
	[Tooltip("Set if need more efects in curve animation")]
	public bool setOvershootAnimation;
	[ConditionalField("setOvershootAnimation")]
	[SerializeField] public float overshoot = 1f;
private float _myFloat = 0.5f;
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
		base.Start();
		fromVectorReference 		= fromVector;
		toVectorReference			= toVector;
		if(!setOvershootAnimation) overshoot=1f;
	}

	#region Abstract Methods
	///<inheritdoc cref="AnimationBase.AbstractInit()"/>
	public override void AbstractInit(){}

	///<inheritdoc cref="AnimationBase.AbstractPlay()"/>
	public override void AbstractPlay(){
		fromVectorReference = fromVector;
		toVectorReference	= toVector;
		currentLoopCount = 0;
		NumberTransition();
	}

	///<inheritdoc cref="AnimationBase.AbstractStop()"/>
	public override void AbstractStop(){
		LeanTween.cancel(this.gameObject);
	}
	#endregion

	#region Public Methods
	///<summary>
	///Gets the current value in currentVectorValue variable
	///</summary>
	public Vector3 GetCurrentNumber() => currentVectorValue;
	#endregion

	#region Private Methods
	///<summary>
	///Start the number transition animation
	///</summary>
	private void NumberTransition(){
		LeanTween.value(this.gameObject, fromVectorReference, toVectorReference, duration)
		.setEase(tweenType)
		.setOvershoot(overshoot)
		.setOnUpdateVector3((value) => {
			currentVectorValue = value;
			OnValueChange.Invoke(currentVectorValue);
		})
		.setOnComplete(()=>{
			fromVectorReference = fromVector;
			toVectorReference	= toVector;

			if(loopCount == -1){
				LeanTween.delayedCall(this.gameObject, delay, ()=> NumberTransition());
			}else{
				currentLoopCount++;

				if(currentLoopCount < loopCount){
					LeanTween.delayedCall(this.gameObject, delay, ()=> NumberTransition());
				}else{

					LeanTween.delayedCall(this.gameObject, eventDelay, ()=>{
						events?.Invoke();
					});
				}
			}
		});
	}
	#endregion

}