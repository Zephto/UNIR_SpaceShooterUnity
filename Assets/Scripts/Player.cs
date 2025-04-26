using UnityEngine;

public class Player : MonoBehaviour {
	#region Public variables
	[SerializeField] private float velocity;
	[SerializeField] private GameObject shootPrefab;
	[SerializeField] private float ratioShoot;

	[SerializeField] private GameObject spawnPoint1;
	[SerializeField] private GameObject spawnPoint2;
	#endregion

	#region Private variables
	private float ratioTimer = 0.5f;
	#endregion

    void Update() {
		Movement();
		MovementLimits();
		Shoot();
    }

	#region Private Methods
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

	private void Shoot(){
		ratioTimer += 1 * Time.deltaTime;

		if(Input.GetKey(KeyCode.Space) && ratioTimer > ratioShoot ){
			Instantiate(shootPrefab, spawnPoint1.transform.position, Quaternion.identity);
			Instantiate(shootPrefab, spawnPoint2.transform.position, Quaternion.identity);
			ratioTimer = 0;
		}
	}
	#endregion
}
