using System.Collections.Generic;
using MyBox;
using UnityEngine;
using System.Linq;
using System.Collections;

public class Player : MonoBehaviour {
	#region Public variables
	[SerializeField] private float velocity;
	[SerializeField] private GameObject shotPrefab;
	[SerializeField] private float ratioShoot;
	[SerializeField] private SpriteRenderer playerVisual;

	[SerializeField] private GameObject spawnPoint;
	[SerializeField] private GameObject poolContainer;
	#endregion

	#region Private variables
	private float ratioTimer = 0.5f;
	private int life = 4;
	private bool isDoubleRatioActive = false;
	private bool isInvencibleActive = false;
	private BoxCollider2D thisBoxCollider;
    #endregion

    void Awake() {
		thisBoxCollider = this.GetComponent<BoxCollider2D>();
    }

    void Start() {
		poolContainer.transform.SetParent(null);

        PrepareShots();
    }

    void Update() {
		Movement();
		MovementLimits();
		Shot();
    }

	#region Private Methods
	private void PrepareShots(){
		for(int i=0; i<50; i++){
			GameObject newShot = Instantiate(shotPrefab, spawnPoint.transform.position, Quaternion.identity, poolContainer.transform);
			newShot.gameObject.SetActive(false);
		}
	}

	private void Movement(){
		float inputH = Input.GetAxisRaw("Horizontal");
		float inputV = Input.GetAxisRaw("Vertical");
		transform.Translate(new Vector2(inputH, inputV).normalized * (velocity * GlobalData.GameSpeed) * Time.deltaTime);
	}

	private void MovementLimits(){
		float xClamped = Mathf.Clamp(transform.position.x, -8.4f, 8.4f);
		float yClamped = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
		transform.position = new Vector3(xClamped, yClamped, 0);
	}

	private void Shot(){
		ratioTimer += 1 * Time.deltaTime;

		if(Input.GetKey(KeyCode.Space) && ratioTimer > ratioShoot ){
			
			foreach(Transform child in poolContainer.transform){
				if(!child.gameObject.activeSelf){
					GameObject selectedShot = child.gameObject;
					selectedShot.transform.position = spawnPoint.transform.position;
					selectedShot.SetActive(true);
					ratioTimer = 0;
					break;
				}
			}

			//Pendiente de agregar disparos cuando se acaben
			Debug.Log("No hay disparos disponibles.");
		}
	}
    #endregion

    #region Trigger Methods
    void OnTriggerEnter2D(Collider2D collision) {
		if(collision.CompareTag("EnemyShoot") || collision.CompareTag("Enemy")){
			if(isInvencibleActive) return;
			
			life -= 1;
			//Destroy(collision.gameObject);
			collision.gameObject.SetActive(false);
			GlobalData.OnPlayerHits?.Invoke(life);

			if(life <= 0){
				GlobalData.OnGameOver?.Invoke();
				this.gameObject.SetActive(false);
				// Destroy(this.gameObject);
			}
		}

		if(collision.CompareTag("Item")){
			Destroy(collision.gameObject);

			switch(Random.value){
				case float r when r < 0.5f:
					GlobalData.Score += 500;
					Debug.Log("Doble disparo");
					if(!isDoubleRatioActive) StartCoroutine(DoubleShot());
				break;

				case float r when r >= 0.5f && r<0.8:
					GlobalData.Score += 800;
					Debug.Log("Invencible");
					if(!isInvencibleActive) StartCoroutine(Invencible());
				break;

				case float r when r >=0.8f && r<=1:
					GlobalData.Score += 1000;
					Debug.Log("Vida Extra");
					life++;
					if(life >= 4) life = 4;
					GlobalData.OnPlayerHits?.Invoke(life);
				break;

				default:
				break;
			}
		}
    }
    #endregion

	#region Coroutines
	private IEnumerator DoubleShot() {
		isDoubleRatioActive = true;
		float previousRatio = ratioShoot;

		ratioShoot /= 3;
		yield return new WaitForSeconds(5f);

		ratioShoot = previousRatio;
		isDoubleRatioActive = false;
	}

	private IEnumerator Invencible() {
		isInvencibleActive = true;
		
		playerVisual.color = new Color(
			playerVisual.color.r,
			playerVisual.color.g,
			playerVisual.color.b, 0.3f);
		yield return new WaitForSeconds(5f);

		playerVisual.color = new Color(
			playerVisual.color.r,
			playerVisual.color.g,
			playerVisual.color.b, 1f);
		isInvencibleActive = false;
	}
	#endregion
}
