using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
	#region public variables
	[SerializeField] private float velocity;
	[SerializeField] private GameObject shootPrefab;
	[SerializeField] private GameObject spawnPoint;
	[SerializeField] private GameObject itemPrefab;
    #endregion

	#region Private variables
	private bool canShoot = false;
	private IEnumerator shotCoroutine;
	#endregion	

    void Start() {
		shotCoroutine = null;
    }

    void OnEnable() {
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
    }

    void Update() {
		this.transform.Translate(new Vector3(-1, 0, 0) * (velocity * GlobalData.GameSpeed) * Time.deltaTime);
	}

	#region Public Methods
	public void CanShot(bool set) => canShoot = set;
	#endregion

	#region Trigger Methods
	void OnTriggerEnter2D(Collider2D collision) {
		if(collision.CompareTag("PlayerShoot")){
			collision.gameObject.SetActive(false);
			this.gameObject.SetActive(false);

			if(Random.value > 0.1f){
				Instantiate(itemPrefab, spawnPoint.transform.position, Quaternion.identity);
			}
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
