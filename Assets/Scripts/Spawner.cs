using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour {
	#region Public references
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private TextMeshProUGUI wavesText;
    #endregion

	#region Private references
	private List<Enemy> listOfEnemies = new List<Enemy>();
	#endregion

    void Start() {
		PrepareEnemies();
        StartCoroutine(SpawnEnemy());
    }

	#region Private Methods
	private void PrepareEnemies(){
		for(int i=0; i<100; i++){
			var newEnemy = Instantiate(enemyPrefab, this.gameObject.transform.position, Quaternion.identity, this.transform);
			listOfEnemies.Add(newEnemy.GetComponent<Enemy>());
			newEnemy.SetActive(false);
		}
	}
	#endregion

	#region Coroutines
	private IEnumerator SpawnEnemy(){
		
		for(int i=0; i<5; i++){ //Niveles
			for(int j=0; j<3; j++){//Oleadas
				wavesText.text = "Nivel " + (i+1) + " - Oleada " + (j+1);
				yield return new WaitForSeconds(2f);
				wavesText.text = "";

				for(int k=0; k<10; k++){//Enemigos

					Vector3 randomPosition = new Vector3(this.transform.position.x, Random.Range(-4.5f,4.5f), 0);
					//Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
					
					//Get random enemy
					Enemy selectedEnemy = listOfEnemies.FirstOrDefault(x => !x.gameObject.activeSelf);
					if(selectedEnemy == null) break;
					
					selectedEnemy.CanShot(Random.value > 0.5);
					selectedEnemy.transform.position = randomPosition;
					selectedEnemy.gameObject.SetActive(true);
					
					yield return new WaitForSeconds(0.5f);
				}

				yield return new WaitForSeconds(2f);
				foreach(var enemy in listOfEnemies){
					enemy.gameObject.SetActive(false);
				}
			}

			yield return new WaitForSeconds(3f);
		}
	}
	#endregion
}
