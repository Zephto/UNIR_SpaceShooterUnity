using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
	#region public variables
	[SerializeField] private float velocity;
	[SerializeField] private GameObject shootPrefab;
	[SerializeField] private GameObject spawnPoint;
    #endregion

    void Start() {
		StartCoroutine(SpawnShoots());
    }

    void Update() {
		this.transform.Translate(new Vector3(-1, 0, 0) * velocity * Time.deltaTime);
	}

	private IEnumerator SpawnShoots(){
		for(int i = 0; i<5; i++){
			Instantiate(shootPrefab, spawnPoint.transform.position, Quaternion.identity);
			yield return new WaitForSeconds(1f);
		}
	}
}
