using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
	#region Public variables
	[SerializeField] private float velocity;
	[SerializeField] private Vector3 direction;
	[SerializeField] private float imageSize;
	#endregion

	#region Private variables
	private Vector3 initialPosition;
    #endregion

    void Start() {
		initialPosition = this.transform.position;
    }

    void Update() {


		//Cuanto me queda de recorrido para alcanzar un nuevo ciclo
		float movement = (velocity * Time.time) % imageSize;

		//Posicion actual se va refrescando desde la inicial SUMANDO
		//el movimiento que me quede en la direccion deseada
		this.transform.position = initialPosition + movement * direction;
    }

}
