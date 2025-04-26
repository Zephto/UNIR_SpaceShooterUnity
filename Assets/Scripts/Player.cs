using System.Collections.Generic;
using MyBox;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour {
	#region Public variables
	[SerializeField] private float velocity;
	[SerializeField] private GameObject shotPrefab;
	[SerializeField] private float ratioShoot;

	[SerializeField] private GameObject spawnPoint;
	[SerializeField] private GameObject poolContainer;
	#endregion

	#region Private variables
	private float ratioTimer = 0.5f;
	private float life = 100;
    #endregion

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
		transform.Translate(new Vector2(inputH, inputV).normalized * velocity * Time.deltaTime);
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
			life -=20;
			Destroy(collision.gameObject);

			if(life <= 0){
				Destroy(this.gameObject);
			}
		}
    }

    #endregion
}
