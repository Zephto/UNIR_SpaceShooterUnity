using UnityEngine.Events;
using UnityEngine;
using MyBox;

///<summary>
///Component used to make a single transition of two numbers
///</summary>
public class Numbers_FromTo: AnimationBase {
	#region Public variables
	///<summary>
	///Initial float number when start the animation
	///</summary>
	[Header("General Settings")]
	[SerializeField] private float fromNumber = 1f;

	///<summary>
	///Final float number where the animation finish
	///</summary>
	[SerializeField] private float toNumber = 10f;

	///<summary>
	///Current value of the transition
	///</summary>
	[ReadOnly] public float currentNumber = 0f;
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
	private float fromNumberReference = 1f;

	///<summary>
	///Reference of the toNumber variable
	///</summary>
	private float toNumberReference = 1f;

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
	[SerializeField] public UnityEvent<float> OnValueChange;
	#endregion

	public override void Start() {
		fromNumberReference = fromNumber;
		toNumberReference	= toNumber;
		base.Start();
	}

	#region Abstract Methods
	///<inheritdoc cref="AnimationBase.AbstractInit()"/>
	public override void AbstractInit(){
		//Initialize current value with the from value
		currentNumber = fromNumber;
		OnValueChange.Invoke(currentNumber);
	}

	///<inheritdoc cref="AnimationBase.AbstractPlay()"/>
	public override void AbstractPlay(){
		fromNumberReference = fromNumber;
		toNumberReference	= toNumber;
		currentLoopCount = 0;
		NumberTransition();
	}

	///<inheritdoc cref="AnimationBase.AbstractStopStop()"/>
	public override void AbstractStop(){
		LeanTween.cancel(this.gameObject);
	}

	///<summary>
	///Gets the current value in currentNumber variable
	///</summary>
	public float GetCurrentNumber() => currentNumber;
	#endregion

	#region Private Methods
	///<summary>
	///Start the number transition animation
	///</summary>
	private void NumberTransition(){
		LeanTween.value(this.gameObject, fromNumberReference, toNumberReference, duration)
		.setEase(tweenType)
		.setOnUpdate((value) => {
			currentNumber = value;
			OnValueChange.Invoke(currentNumber);
		})
		.setOnComplete(()=>{
			fromNumberReference = fromNumber;
			toNumberReference	= toNumber;

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