using System.Collections;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour {
	#region Public references
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private TextMeshProUGUI wavesText;
    #endregion

    void Start() {
        StartCoroutine(SpawnEnemy());
    }

	private IEnumerator SpawnEnemy(){
		
		for(int i=0; i<5; i++){ //Niveles
			for(int j=0; j<3; j++){//Oleadas
				wavesText.text = "Nivel " + (i+1) + " - Oleada " + (j+1);
				yield return new WaitForSeconds(2f);
				wavesText.text = "";

				for(int k=0; k<10; k++){//Enemigos

					Vector3 randomPosition = new Vector3(this.transform.position.x, Random.Range(-4.5f,4.5f), 0);
					Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
					yield return new WaitForSeconds(0.5f);
				}

				yield return new WaitForSeconds(2f);
			}

			yield return new WaitForSeconds(3f);
		}
	}
}
