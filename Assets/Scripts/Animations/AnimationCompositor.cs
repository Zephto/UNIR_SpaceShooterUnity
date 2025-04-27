using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

///<summary>
/// Helps composit animation components with timings
///</summary>
public class AnimationCompositor : AnimationBase {
	///<summary>
	/// Animation event internal class
	///</summary>
	[Serializable]
	public class AnimationEvent {
		///<summary>
		///Event delay
		///</summary>
		[Tooltip("Float delay used to activate the sequence of events")]
		[SerializeField] public float delay = 0;
		
		///<summary>
		///Callback variable
		///</summary>
		[Header("On use Play")]
		[Tooltip("Set the events that will be called when compositor Play function is used")]
		[SerializeField] public UnityEvent playEvents;
	
		///<summary>
		///Callback variable
		[Header("On use Stop")]
		///</summary>
		[Tooltip("Set the events that will be called when compositor Stop function is used")]
		[SerializeField] public UnityEvent stopEvents;
	}

	#region Private variables
	///<summary>
	///The sum of the delay time of each animation event
	///</summary>
	private float totalTimeDelay = 0f;
	#endregion

	#region Animations
	[Header("Animation Events")]
	///<summary>
	/// Animation event list
	///</summary>
	[SerializeField] public List<AnimationEvent> animations;
	#endregion

	public override void Start() {
		base.Start();
		totalTimeDelay = animations[animations.Count-1].delay;
	}

	#region Methods
	///<inheritdoc cref="AnimationBase.Init()"/>
	public override void AbstractInit(){}

	///<inheritdoc cref="AnimationBase.Play()"/>
	public override void AbstractPlay(){
		foreach(var item in animations){
			LeanTween.delayedCall(this.gameObject, item.delay, () => {
				item.playEvents.Invoke();
			});
		}

		LeanTween.delayedCall(this.gameObject, totalTimeDelay, ()=>{
			base.events?.Invoke();
		});
	}

	///<inheritdoc cref="AnimationBase.Stop()"/>
	public override void AbstractStop(){
		foreach(var item in animations){
			item.stopEvents.Invoke();
		}
		LeanTween.cancel(this.gameObject);
	}

	public float GetTotalTime() => totalTimeDelay;
	#endregion
}
