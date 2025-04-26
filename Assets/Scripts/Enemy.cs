using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
	#region public variables
	[SerializeField] private float velocity;
	[SerializeField] private GameObject shootPrefab;
	[SerializeField] private GameObject spawnPoint;
    #endregion

	#region Private variables
	private bool canShoot = false;
	#endregion	

    void Start() {
		if(canShoot) StartCoroutine(SpawnShoots());
    }

    void Update() {
		this.transform.Translate(new Vector3(-1, 0, 0) * velocity * Time.deltaTime);
	}

	#region Public Methods
	public void CanShot(bool set) => canShoot = set;
	#endregion

	#region Trigger Methods
	void OnTriggerEnter2D(Collider2D collision) {
		if(collision.CompareTag("PlayerShoot")){
			collision.gameObject.SetActive(false);
			this.gameObject.SetActive(false);

			// Destroy(collision.gameObject);
			// Destroy(this.gameObject);
		}
    }
	#endregion

	#region Coroutines
	private IEnumerator SpawnShoots(){
		while (true) {
			Instantiate(shootPrefab, spawnPoint.transform.position, Quaternion.identity);
			yield return new WaitForSeconds(1f);
		}
	}
	#endregion
}
