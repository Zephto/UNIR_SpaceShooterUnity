using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {
	#region Public references
	[SerializeField] private GameObject enemyPrefab;
    #endregion

    void Start() {
        StartCoroutine(SpawnEnemy());
    }

	private IEnumerator SpawnEnemy(){

		for(int i=0; i<5; i++){
			Instantiate(enemyPrefab, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(1f);
		}
	}
}
