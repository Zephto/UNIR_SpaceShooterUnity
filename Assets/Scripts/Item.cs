using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour {
	
	#region public variables
	[SerializeField] private float velocity;
    #endregion

	#region private variables
	private float lifeTime = 5;
    #endregion

    void Start() {
        StartCoroutine(LifeTimeCoroutine());
    }

    void Update() {
		this.transform.Translate(new Vector3(-1, 0, 0) * (velocity * GlobalData.GameSpeed) * Time.deltaTime);
	}

	private IEnumerator LifeTimeCoroutine(){
		yield return new WaitForSeconds(lifeTime);
		Destroy(this.gameObject);
	}
}
