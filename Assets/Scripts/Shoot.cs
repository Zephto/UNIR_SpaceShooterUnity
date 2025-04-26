using UnityEngine;

public class Shoot : MonoBehaviour {

	#region public variables
	[SerializeField] private float velocity;
	[SerializeField] private Vector3 direction;
	#endregion

    void Update() {
        this.transform.Translate(direction * velocity * Time.deltaTime);
    }
}
