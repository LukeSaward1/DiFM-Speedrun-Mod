using System;
using TMPro;
using UnityEngine;

// Token: 0x020001C3 RID: 451
public class MainMenu : MonoBehaviour
{
	// Token: 0x0600096B RID: 2411 RVA: 0x00030F50 File Offset: 0x0002F150
	public void Start()
	{
		this.extrasButton.SetActive(true);
		this.TitleTextText.SetText("DO IT FOR ME\nSPEEDRUN VERSION");
		this.TitleTextText.fontSize = 100f;
		this.LixianObj = GameObject.Find("Lixian");
		this.LixianText = this.LixianObj.GetComponent<TextMeshProUGUI>();
		this.LixianText.SetText("<#FF0000>MOD BY LUKE SAWARD");
		this.boomy = false;
		this.theGM = GameObject.Find("GameManager");
		this.gameManager = this.theGM.GetComponent<GameManager>();
		this.timerObj = GameObject.Find("TimerController");
		this.timerScript = this.timerObj.GetComponent<TimerController>();
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x0000F7B1 File Offset: 0x0000D9B1
	public void StartButton()
	{
		this.timerScript.timerGoing = true;
		this.uiManager.StartSceneChange("FirstCutscene");
		AudioManager.instance.StopAllMusic();
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x0000F7D9 File Offset: 0x0000D9D9
	public void CreditsButton()
	{
		this.uiManager.StartSceneChange("Credits");
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x0000F7EB File Offset: 0x0000D9EB
	public void ExtrasButton()
	{
		this.uiManager.StartSceneChange("CreditsExtra");
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0000F7FD File Offset: 0x0000D9FD
	public void QuitButton()
	{
		Application.Quit();
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x0000F804 File Offset: 0x0000DA04
	public void BackButton()
	{
		this.uiManager.StartSceneChange("MainMenu");
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x00031004 File Offset: 0x0002F204
	public void Awake()
	{
		this.TitleTextObject = GameObject.Find("TITLE");
		this.TitleTextText = this.TitleTextObject.GetComponent<TextMeshProUGUI>();
		this.ModBy = new GameObject();
		this.ModByText = this.ModBy.AddComponent<TextMeshProUGUI>();
		this.ModByText.SetText("<#FF0000>aiwhydawgydyuawgdyuagwduyagwdawuydtgawudygawudgawduyg");
		this.AGB = GameObject.Find("AGameBy");
		this.AGBText = this.AGB.GetComponent<TextMeshProUGUI>();
		this.AGBText.SetText("A GAME BY \nLIXIAN");
		this.ModByText.transform.localScale = this.AGBText.transform.localScale;
		this.ModByText.fontSize = 100f;
		this.ModByText.transform.position = new Vector3(100f, 5f);
		this.ModBy.transform.parent = this.TitleTextObject.transform;
		this.LixianObj = GameObject.Find("Lixian");
		this.LixianText = this.LixianObj.GetComponent<TextMeshProUGUI>();
		this.LixianText.SetText("<#FF0000>MOD BY LUKE SAWARD");
		this.LixianObj.transform.position = new Vector3(-1.315f, -4.25f, 90f);
		this.rectTransform = this.LixianText.GetComponent<RectTransform>();
		this.rectTransform.sizeDelta = new Vector2(1500f, 25f);
		this.boomy = false;
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x00031180 File Offset: 0x0002F380
	public void Update()
	{
		this.LixianObj = GameObject.Find("Lixian");
		this.LixianText = this.LixianObj.GetComponent<TextMeshProUGUI>();
		this.LixianText.SetText("<#FF0000>MOD BY LUKE SAWARD");
		if (this.timerScript.epilepsyMode)
		{
			this.Color += Time.deltaTime * 0.05f;
		}
		if (!this.timerScript.epilepsyMode)
		{
			this.Color += Time.deltaTime * 0.2f;
		}
		if (this.Color >= 1f)
		{
			this.Color = 0f;
		}
		this.TitleTextText.color = UnityEngine.Color.HSVToRGB(this.Color, 1f, 1f);
		if (!this.timerScript.AllEndings)
		{
			this.LixianText.SetText("<#FF0000>MOD BY LUKE SAWARD");
		}
		if (this.timerScript.AllEndings)
		{
			this.LixianText.SetText("<#FF0000>MOD BY LUKE SAWARD <#FFFFFF>|<#00BFFF> ALL ENDINGS MODE");
		}
		if (this.timerScript.timeCounter >= 0.25f)
		{
			this.timerScript.countingEnabled = false;
			this.timerScript.timeCounter = 0f;
			this.StartButton();
		}
	}

	// Token: 0x04000B79 RID: 2937
	public UIManager uiManager;

	// Token: 0x04000B7A RID: 2938
	public GameObject extrasButton;

	// Token: 0x04000B7B RID: 2939
	public GameObject TitleTextObject;

	// Token: 0x04000B7C RID: 2940
	public TextMeshProUGUI TitleTextText;

	// Token: 0x04000B7D RID: 2941
	public GameObject ModBy;

	// Token: 0x04000B7E RID: 2942
	public TextMeshProUGUI ModByText;

	// Token: 0x04000B7F RID: 2943
	public GameObject AGB;

	// Token: 0x04000B80 RID: 2944
	public TextMeshProUGUI AGBText;

	// Token: 0x04000B81 RID: 2945
	public GameObject LixianObj;

	// Token: 0x04000B82 RID: 2946
	public TextMeshProUGUI LixianText;

	// Token: 0x04000B83 RID: 2947
	public float newXPos;

	// Token: 0x04000B84 RID: 2948
	public RectTransform rectTransform;

	// Token: 0x04000B85 RID: 2949
	private GameManager TheGM;

	// Token: 0x04000B86 RID: 2950
	public bool boomy;

	// Token: 0x04000B87 RID: 2951
	public float inGameTime;

	// Token: 0x04000B88 RID: 2952
	public float Color;

	// Token: 0x04000B89 RID: 2953
	public GameObject theGM;

	// Token: 0x04000B8A RID: 2954
	public GameManager gameManager;

	// Token: 0x04000B8B RID: 2955
	public GameObject timerObj;

	// Token: 0x04000B8C RID: 2956
	public TimerController timerScript;
}
