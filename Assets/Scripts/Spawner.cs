using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour {
	#region Public references
	[SerializeField] private List<GameObject> enemyPrefab;
    #endregion

	#region Private references
	private List<Enemy> listOfEnemies = new List<Enemy>();
	private IEnumerator spawnCoroutine;
	private int currentEnemies = 10;
	#endregion

    void Start() {
		PrepareEnemies();
		spawnCoroutine = SpawnEnemy();
        StartCoroutine(spawnCoroutine);

		GlobalData.OnGameOver.AddListener(() => StopCoroutine(spawnCoroutine));
    }

	#region Private Methods
	private void PrepareEnemies(){
		for(int i=0; i<100; i++){
			GameObject prefabToUse = i>50? enemyPrefab[0] : enemyPrefab[1];

			var newEnemy = Instantiate(prefabToUse, this.gameObject.transform.position, Quaternion.identity, this.transform);
			listOfEnemies.Add(newEnemy.GetComponent<Enemy>());
			newEnemy.SetActive(false);
		}
	}
	#endregion

	#region Coroutines
	private IEnumerator SpawnEnemy(){
		while(true){

			// wavesText.text = "Nivel " + (i+1) + " - Oleada " + (j+1);
			GlobalData.OnNewWave?.Invoke();
			yield return new WaitForSeconds(2f);
			// wavesText.text = "";

			for(int k=0; k<currentEnemies; k++){//Enemigos

				Vector3 randomPosition = new Vector3(this.transform.position.x, Random.Range(-4.5f,4.5f), 0);
				//Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
				
				//Get random enemy
				Enemy selectedEnemy = listOfEnemies.FirstOrDefault(x => !x.gameObject.activeSelf);
				if(selectedEnemy == null) break;
				
				selectedEnemy.CanShot(Random.value > 0.5);
				selectedEnemy.transform.position = randomPosition;
				selectedEnemy.gameObject.SetActive(true);
				
				yield return new WaitForSeconds(0.5f + Random.Range(0f, 2f));
			}

			yield return new WaitForSeconds(2f);
			foreach(var enemy in listOfEnemies){
				enemy.gameObject.SetActive(false);
			}

			//Activar boss battle
			// GlobalData.OnBossStage?.Invoke();
			// while(GlobalData.isBossBattle){
			// 	yield return null;
			// }

			yield return new WaitForSeconds(2f);
			GlobalData.GameSpeed += 0.07f;
			currentEnemies++;
		}
	}
	#endregion
}
