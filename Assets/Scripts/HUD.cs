using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour {
	#region Private variables'
	[Header("Banner References")]
	[SerializeField] private GameObject banner;
	[SerializeField] private TextMeshProUGUI bannerText;

	[Header("Gameover")]
	[SerializeField] private GameObject finalBanner;
	[SerializeField] private TextMeshProUGUI finalScore;

	[Header("InGame References")]
	[SerializeField] private TextMeshProUGUI currentScore;
	[SerializeField] private List<GameObject> lifeReferences;
    #endregion

	#region Private variables
	private SceneChanger sceneChanger;
	private TransitionScreen transitionScreen;
	private bool isGameOver = false;
	#endregion

	#region static variables
	private float banPos = 2000f;
    #endregion

    void Awake() {
		if(TransitionScreen.Instance != null){
			transitionScreen = TransitionScreen.Instance;
		}else{
			transitionScreen = FindAnyObjectByType<TransitionScreen>(FindObjectsInactive.Exclude);
		}

		if(SceneChanger.Instance != null){
			sceneChanger = SceneChanger.Instance;
		}else{
			sceneChanger = FindAnyObjectByType<SceneChanger>(FindObjectsInactive.Exclude);
		}
    }

    void Start() {
		transitionScreen.StartIn();

		GlobalData.OnPlayerHits.AddListener((int life)=>CheckPlayerLife(life));
		GlobalData.OnNewWave.AddListener(()=>ShowWaveBanner());
		GlobalData.OnBossStage.AddListener(()=>ShowBossBanner());
		GlobalData.OnGameOver.AddListener(()=>ShowGameOverBanner());
		GlobalData.OnScoreUpdate.AddListener(()=>UpdateScore());
		GlobalData.Score = 0;

		LeanTween.moveLocalX(banner, banPos, 0f);
		LeanTween.moveLocalX(finalBanner, banPos, 0f);
		transitionScreen.Out(null);
	}

    void Update() {
		if(isGameOver){
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")){
				transitionScreen.In(()=>{
					GlobalData.ResetVariables();
					sceneChanger.LoadSceneAsync("MainGame");
				});
			}
		}
    }

    #region Private methods
    private void UpdateScore(){
		currentScore.text = GlobalData.Score.ToString();
	}

	private void CheckPlayerLife(int currentLife){
		for(int i=0; i<lifeReferences.Count; i++){
			lifeReferences[i].SetActive(i < currentLife);
		}
	}

	private void ShowWaveBanner(){
		bannerText.text = "Next Wave";
		MoveBanner();
	}

	private void ShowBossBanner(){
		bannerText.text = "Boss Battle!";
		MoveBanner();
	}

	private void ShowGameOverBanner(){
		currentScore.gameObject.SetActive(false);
		finalScore.text = "Score: " + GlobalData.Score;
		LeanTween.moveLocalX(finalBanner, 0f, 0.3f)
		.setEaseOutSine();

		isGameOver = true;
	}

	private void MoveBanner(){
		LeanTween.moveLocalX(banner, banPos, 0f);

		LeanTween.moveLocalX(banner, 0f, 0.3f)
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
