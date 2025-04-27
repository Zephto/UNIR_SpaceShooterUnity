using UnityEngine;
using UnityEngine.Events;

public class GlobalData : MonoBehaviour {
	
	public static void ResetVariables(){
		isBossBattle = false;
		_gameSpeed = 1.0f;
		_score = 0;
	}

	#region Game Events
	public static UnityEvent<float> OnGameSpeedChange = new UnityEvent<float>();
	private static float _gameSpeed = 1.0f;
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
	#endregion

	#region Player Events
	public static UnityEvent<int> OnPlayerHits = new UnityEvent<int>();
	public static UnityEvent OnGameOver = new UnityEvent();
	public static UnityEvent OnScoreUpdate = new UnityEvent();
	private static int _score = 0;
	public static int Score{
		get => _score;
		set {
			OnScoreUpdate?.Invoke();
			_score = value;
		}
	}
	#endregion

	#region Enemy Events
	public static UnityEvent OnNewWave = new UnityEvent();
	public static UnityEvent OnBossStage = new UnityEvent();

	public static bool isBossBattle = false;
	#endregion
}
