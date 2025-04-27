using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Parallax : MonoBehaviour {
	#region Public variables
	[SerializeField] private float velocity;
	[SerializeField] private Vector3 direction;
	[SerializeField] private float imageSize;
	#endregion

	#region Private variables
	private Vector3 initialPosition;
	private float currentSpeed = 0f;
	private bool canStop = false;
	private float accumulatedMovement = 0f;
    #endregion

    void Start() {
		initialPosition = this.transform.position;
		GlobalData.OnGameOver.AddListener(()=> canStop = true);
    }

    void Update() {
		if(canStop){
			currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, 2f * Time.deltaTime);
		}else{
			currentSpeed = velocity * GlobalData.GameSpeed;
		}

		//Cuanto me queda de recorrido para alcanzar un nuevo ciclo
		// float movement = (currentSpeed * Time.time) % imageSize;
		float frameMovement = currentSpeed * Time.deltaTime;
		accumulatedMovement += frameMovement;
		float cyclicMovement = accumulatedMovement % imageSize;

		//Posicion actual se va refrescando desde la inicial SUMANDO
		//el movimiento que me quede en la direccion deseada
		// this.transform.position = initialPosition + movement * direction;
		this.transform.position = initialPosition + cyclicMovement * direction;
    }

}
