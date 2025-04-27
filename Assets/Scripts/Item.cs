using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour {
	
	#region public variables
	[SerializeField] private float velocity;
	[SerializeField] private ParticleSystem explosion;
	#endregion

	#region private variables
	private float lifeTime = 5;
	private Color[] _rainbowColors = new Color[]
	{
		Color.red,
		Color.yellow,
		Color.green,
		Color.blue,
	};
	#endregion

	void Start() {
		StartCoroutine(LifeTimeCoroutine());
		StartRainbowEffect();
	}

    void OnDestroy() {
        Instantiate(explosion, this.transform.position, Quaternion.identity, null);
    }

    void Update() {
		this.transform.Translate(new Vector3(-1, 0, 0) * (velocity * GlobalData.GameSpeed) * Time.deltaTime);
	}

	void StartRainbowEffect() {
		int currentIndex = 0;

		// Función recursiva para ciclar colores
		void CycleColor() {
			LeanTween.color(this.gameObject, _rainbowColors[currentIndex], 0.3f)
			.setOnComplete(() => 
			{
				currentIndex = (currentIndex + 1) % _rainbowColors.Length;
				CycleColor();
			});
		}

		CycleColor();
	}

	private IEnumerator LifeTimeCoroutine(){
		yield return new WaitForSeconds(lifeTime);
		Destroy(this.gameObject);
	}
}
