using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour {
	#region Private variables'
	[Header("Banner References")]
	[SerializeField] private GameObject banner;
	[SerializeField] private TextMeshProUGUI bannerText;

	[Header("Player Life")]
	[SerializeField] private List<GameObject> lifeReferences;
    #endregion

	#region static variables
	private float banPos = 2000f;
	#endregion

    void Start() {
		GlobalData.OnPlayerHits.AddListener((int life)=>CheckPlayerLife(life));
		GlobalData.OnNewWave.AddListener(()=>ShowWaveBanner());
		GlobalData.OnBossStage.AddListener(()=>ShowBossBanner());


		LeanTween.moveLocalX(banner, banPos, 0f);
	}

    #region Private methods
	private void CheckPlayerLife(int currentLife){
		for(int i=0; i<lifeReferences.Count; i++){
			lifeReferences[i].SetActive(i < currentLife);
		}
	}

	private void ShowWaveBanner(){
		bannerText.text = "Next Wave";
		MoveBanner(0f);
	}

	private void ShowBossBanner(){
		bannerText.text = "Boss Battle!";
		MoveBanner(0f);
	}

	private void MoveBanner(float toPosition){
		LeanTween.moveLocalX(banner, banPos, 0f);

		LeanTween.moveLocalX(banner, toPosition, 0.3f)
		.setEaseOutSine()
		.setOnComplete(()=>{
			LeanTween.delayedCall(1.5f, ()=>{
				LeanTween.moveLocalX(banner, -banPos, 0.3f)
			.setEaseOutSine();
			});
		});
	}
    #endregion
}
