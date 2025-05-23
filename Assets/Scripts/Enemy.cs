using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
	#region public variables
	[SerializeField] private float velocity;
	[SerializeField] private int life = 1;
	[SerializeField] private GameObject shotPrefab;
	[SerializeField] private GameObject spawnPoint;
	[SerializeField] private GameObject itemPrefab;
	[SerializeField] private GameObject poolContainer;
	[SerializeField] private ParticleSystem explosion;
    #endregion

	#region Private variables
	private bool canShoot = false;
	private int currentLife;
	private IEnumerator shotCoroutine;
	private List<Shoot> listOfShots = new List<Shoot>();
	#endregion	

    void Start() {
		shotCoroutine = null;
		poolContainer.transform.SetParent(null);

		PrepareShots();
    }

    void OnEnable() {
		currentLife = life;

		if(canShoot){
			if(shotCoroutine == null) shotCoroutine = SpawnShoots();
			StartCoroutine(shotCoroutine);
		}
    }

    void OnDisable() {
        if(shotCoroutine != null){
			StopCoroutine(shotCoroutine);
			shotCoroutine = null;
		}

		Instantiate(explosion, this.transform.position, Quaternion.identity, null);
    }

    void Update() {
		this.transform.Translate(new Vector3(-1, 0, 0) * (velocity * GlobalData.GameSpeed) * Time.deltaTime);
	}

	#region Private Methods
	private void PrepareShots(){
		for(int i=0; i<50; i++){
			GameObject newShot = Instantiate(shotPrefab, spawnPoint.transform.position, Quaternion.identity, poolContainer.transform);
			newShot.gameObject.SetActive(false);
			listOfShots.Add(newShot.GetComponent<Shoot>());
		}
	}
	#endregion

	#region Public Methods
	public void CanShot(bool set) => canShoot = set;
	#endregion

	#region Trigger Methods
	void OnTriggerEnter2D(Collider2D collision) {
		if(collision.CompareTag("PlayerShoot")){
			collision.gameObject.SetActive(false);
			currentLife--;

			if(currentLife > 0) return;

			this.gameObject.SetActive(false);
			GlobalData.Score += 100;
			// Instantiate(explosion, this.transform.position, Quaternion.identity, null);

			if(Random.value > 0.5f){
				Instantiate(itemPrefab, spawnPoint.transform.position, Quaternion.identity);
			}
		}
    }
	#endregion

	#region Coroutines
	private IEnumerator SpawnShoots(){
		while (true) {
			// Instantiate(shootPrefab, spawnPoint.transform.position, Quaternion.identity);
			
			//Get random enemy
			Shoot selectedEnemy = listOfShots.FirstOrDefault(x => !x.gameObject.activeSelf);
			if(selectedEnemy == null) break;

			selectedEnemy.transform.position = spawnPoint.transform.position;
			selectedEnemy.gameObject.SetActive(true);

			yield return new WaitForSeconds(1f);
		}
	}
	#endregion
}
