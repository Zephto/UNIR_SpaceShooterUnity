using System.Collections;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour {
	#region Public references
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private TextMeshProUGUI wavesText
;
    #endregion

    void Start() {
        StartCoroutine(SpawnEnemy());
    }

	private IEnumerator SpawnEnemy(){
		
		for(int i=0; i<3; i++){ //Niveles
			
			for(int j=0; j<3; j++){//Oleadas

				for(int k=0; k<10; k++){//Enemigos
					Instantiate(enemyPrefab, transform.position, Quaternion.identity);
					yield return new WaitForSeconds(0.5f);
				}

				yield return new WaitForSeconds(2f);
			}

			yield return new WaitForSeconds(3f);
		}
	}
}
