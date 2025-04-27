using UnityEngine;
using UnityEngine.Events;

///<summary>
///Animation Base used to make consistency in the code
///</summary>
public abstract class AnimationBase: MonoBehaviour {
	///<summary>
	///Enum behaviour type used to know the moment to active the rotate animation
	///</summary>
	public enum BehaviourType{ MANUAL, START, ON_ENABLE }

	///<summary>
	/// Time behavior type
	///</summary>
	public enum TimeBehaviourType { BY_SECONDS, BY_FRAMES }

	#region Behaviour type
	///<inheritdoc cref="BehaviourType"/>
	[Header("Behaviour type")]
	[Tooltip("IMPORTANT: Be carefull with the case START, because the animation will not stop even if the object is deactivated. If you want the animation stops when object is not active use ON_ENABLE or in any other case, control the momment of activation of the script with MANUAL and reference in your code.")]
	public BehaviourType behaviourType = BehaviourType.ON_ENABLE;

	///<inheritdoc cref="TimeBehaviourType">
	public TimeBehaviourType timeBehaviourType;

	///<summary>
	///Float to set a initial delay before start the animation
	///</summary>
	[Tooltip("Initial delay before start the current animation")]
	public float initDelay = 0f;

	///<summary>
	///Boolean to know if activate the Init function at the start of the game
	///</summary>
	[Tooltip("Depends of each script, initialize default values (like start position, scale or velocity)")]
	public bool setInitValues = false;
	#endregion

	#region Callbacks
	[Header("Callback Events")]
	///<summary>
	///Callback variable
	///</summary>
	[SerializeField] public float eventDelay = 0;
	///<summary>
	///Callback variable
	///</summary>
	[SerializeField] public UnityEvent events;
	#endregion

	#region On Enable behaviour
	public virtual void OnEnable() {
		if(behaviourType == BehaviourType.ON_ENABLE) Play();
	}

	public virtual void OnDisable() {
		if(behaviourType == BehaviourType.ON_ENABLE) Stop();
	}
	#endregion

	public virtual void Start() {
		if(setInitValues) Init();
		if(behaviourType == BehaviourType.START) Play();
	}

	public virtual void OnDestroy() {
		LeanTween.cancel(this.gameObject);
	}

	#region Methods
	///<summary>
	///Initialize the default values or necessary to start the animation
	///</summary>
	public void Init(){
		AbstractInit();
	}

	///<summary>
	///Start the current animation
	///</summary>
	public void Play(){
		LeanTween.delayedCall(this.gameObject, initDelay, ()=>{
			AbstractPlay();
		});
	}

	///<summary>
	///Stop the current animation
	///</summary>
	public void Stop(){
		AbstractStop();
	}

	///<summary>
	///Invoke the events
	///</summary>
	public void InvokeEvents(){
		LeanTween.delayedCall(this.gameObject, eventDelay, ()=> events?.Invoke());
	}
	#endregion

	#region Abstract Methods
	///<summary>
	///Abstract Init function, used to initialize variables in each animation script. Usually if you want to set the initial values to start the animations.
	///</summary>
	public abstract void AbstractInit();

	///<summary>
	///Abstract Play function, used to play the animation according to each animation script
	///</summary>
	public abstract void AbstractPlay();

	///<summary>
	///Abstract Stop function, used to stop the current animation of each animation script
	///</summary>
	public abstract void AbstractStop();
	#endregion
}