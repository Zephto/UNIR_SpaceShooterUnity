using UnityEngine;
using UnityEngine.Events;

public class GlobalData : MonoBehaviour {
	
	#region Game Events
	public static UnityEvent<float> OnGameSpeedChange = new UnityEvent<float>();
	public static float GameSpeed{
		get => _gameSpeed;
		set {
			if(value >= 5f){
				_gameSpeed = 5f;
			}else{
				_gameSpeed = value;
			}
		}
	}

	private static float _gameSpeed = 1.0f;
	#endregion

	#region Player Events
	public static UnityEvent<int> OnPlayerHits = new UnityEvent<int>();
	public static UnityEvent OnGameOver = new UnityEvent();
	#endregion

	#region Enemy Events
	public static UnityEvent OnNewWave = new UnityEvent();
	public static UnityEvent OnBossStage = new UnityEvent();

	public static bool isBossBattle = false;
	#endregion
}
