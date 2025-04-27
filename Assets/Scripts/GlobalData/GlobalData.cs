using UnityEngine;
using UnityEngine.Events;

public class GlobalData : MonoBehaviour {
	
	#region Player Events
	public static UnityEvent<int> OnPlayerHits = new UnityEvent<int>();
	public static UnityEvent OnNewWave = new UnityEvent();
	public static UnityEvent OnBossStage = new UnityEvent();

	public static bool isBossBattle = false;
	#endregion
}
