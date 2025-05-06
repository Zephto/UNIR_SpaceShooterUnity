using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	#region public variables
	[SerializeField] private float velocity;
	[SerializeField] private Vector3 direction;
	[SerializeField] private ParticleSystem explosion;
	#endregion

	#region private variables
	private float lifeTime = 5;
    #endregion

    private void OnEnable() {
		StartCoroutine(LifeTimeCoroutine());
    }

	private void OnDisable() {
		Instantiate(explosion, this.transform.position, Quaternion.identity, null);
	}

	void Update() {
        this.transform.Translate(direction * (velocity*GlobalData.GameSpeed) * Time.deltaTime);
    }

	private IEnumerator LifeTimeCoroutine(){
		yield return new WaitForSeconds(lifeTime);
		this.gameObject.SetActive(false);
	}
}
