using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000216 RID: 534
public class TimerController : MonoBehaviour
{
	// Token: 0x06000ABF RID: 2751
	public void Start()
	{
		this.epilepsyMode = true;
		this.ActiveScene = SceneManager.GetActiveScene().name;
		this.GMObject = GameObject.Find("GameManager");
		this.GMScript = this.GMObject.GetComponent<GameManager>();
		this.timerGoing = false;
		this.LevelAudioSource = GameObject.Find("LevelMusic").GetComponent<AudioSource>();
		this.HallwayAudioSource = GameObject.Find("SomethingsWrong").GetComponent<AudioSource>();
		this.CutsceneAudioSource = GameObject.Find("TheGirl").GetComponent<AudioSource>();
		this.BadEndingAudioSource = GameObject.Find("BadEnding").GetComponent<AudioSource>();
		this.MenuAudioSource = GameObject.Find("Theme").GetComponent<AudioSource>();
		this.LevelAudioSource.volume = 1f;
		this.HallwayAudioSource.volume = 1f;
		this.CutsceneAudioSource.volume = 1f;
		this.BadEndingAudioSource.volume = 1f;
		this.MenuAudioSource.volume = 1f;
		this.ThemeMusicObject = GameObject.Find("Theme");
		this.TheGirlMusicObject = GameObject.Find("TheGirl");
		this.LevelMusicMusicObject = GameObject.Find("LevelMusic");
		this.SomethingsWrongMusicObject = GameObject.Find("SomethingsWrong");
		this.BadEndingMusicObject = GameObject.Find("BadEnding");
		this.ThemeMusicObject.AddComponent<AudioLowPassFilter>();
		this.TheGirlMusicObject.AddComponent<AudioLowPassFilter>();
		this.LevelMusicMusicObject.AddComponent<AudioLowPassFilter>();
		this.SomethingsWrongMusicObject.AddComponent<AudioLowPassFilter>();
		this.BadEndingMusicObject.AddComponent<AudioLowPassFilter>();
		this.audioLowPass1 = this.ThemeMusicObject.GetComponent<AudioLowPassFilter>();
		this.audioLowPass2 = this.TheGirlMusicObject.GetComponent<AudioLowPassFilter>();
		this.audioLowPass3 = this.LevelMusicMusicObject.GetComponent<AudioLowPassFilter>();
		this.audioLowPass4 = this.SomethingsWrongMusicObject.GetComponent<AudioLowPassFilter>();
		this.audioLowPass5 = this.BadEndingMusicObject.GetComponent<AudioLowPassFilter>();
		this.PlayerLandObj = GameObject.Find("PlayerLand");
		this.PlayerLand = this.PlayerLandObj.GetComponent<AudioSource>();
		this.normWidth = Screen.width;
		this.normHeight = Screen.height;
		if (!this.hasCreatedTimerShit)
		{
			this.WIBBLE = new GameObject();
			this.WIBBLE.name = "SpeedrunCanvas";
			this.WIBBLE.AddComponent<Canvas>();
			this.TIMERCANVAS = this.WIBBLE.GetComponent<Canvas>();
			this.TIMERCANVAS.renderMode = RenderMode.ScreenSpaceOverlay;
			this.WIBBLE.AddComponent<CanvasScaler>();
			this.CS = this.WIBBLE.GetComponent<CanvasScaler>();
			this.CS.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			this.CS.referenceResolution = new Vector2(1280f, 720f);
			this.CS.matchWidthOrHeight = 0.5f;
			this.WIBBLE.AddComponent<GraphicRaycaster>();
			this.TextGO = new GameObject();
			this.TextGO.transform.parent = this.WIBBLE.transform;
			this.TextGO.name = "WOBBLE";
			this.TCTextThing = this.TextGO.AddComponent<TextMeshProUGUI>();
			this.TCTextThing.fontSize = 50f;
			this.TCTextThing.outlineWidth = 100f;
			this.TCTextThing.outlineColor = Color.black;
			this.rectTransform = this.TCTextThing.GetComponent<RectTransform>();
			this.rectTransform.localPosition = new Vector3(0f, -4.4f, 0f);
			this.rectTransform.sizeDelta = new Vector2(800f, 50f);
			this.MasterID = new GameObject();
			this.MasterID.transform.SetParent(this.TIMERCANVAS.transform);
			this.JumpKeyObject = new GameObject();
			this.JumpKeyObject.transform.SetParent(this.MasterID.transform);
			this.JumpKeyObject.AddComponent<Image>();
			this.JumpKey = this.JumpKeyObject.GetComponent<Image>();
			this.JumpKey.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 127);
			this.hasCreatedTimerShit = false;
			this.isGamePaused = false;
			this.AudioMuted = false;
			this.TestGOBJECT = new GameObject();
			this.TestGOBJECT.transform.SetParent(this.WIBBLE.transform);
			this.TestGOBJECT.transform.localPosition = new Vector3(160f, 200f);
			this.TestGOBJECT.transform.localScale = new Vector3(1.25f, 1.25f);
			this.Hahahaha = new GameObject();
			this.Hahahaha.transform.SetParent(this.TestGOBJECT.transform);
			this.Hahahaha.transform.localScale = new Vector3(1.25f, 1.25f);
			this.Hahahaha.AddComponent<TextMeshProUGUI>();
			this.WPressedTMP = this.Hahahaha.GetComponent<TextMeshProUGUI>();
			this.WPressedTMP.SetText("L");
			this.WPressedTMP.fontSize = 75f;
			this.WPressedTMP.transform.localPosition = new Vector3(210f, 180f);
			this.WPressedTMP.color = new Color(1f, 1f, 1f);
			this.WPressedTMP.outlineWidth = 1f;
			this.WPressedTMP.outlineColor = new Color32(0, 0, 0, byte.MaxValue);
			this.TestGOBJECT.transform.localScale = new Vector3(0.65f, 0.65f);
			this.AButtonObj = new GameObject();
			this.AButtonObj.transform.SetParent(this.TestGOBJECT.transform);
			this.AButtonObj.AddComponent<TextMeshProUGUI>();
			this.AButtonTMP = this.AButtonObj.GetComponent<TextMeshProUGUI>();
			this.AButtonTMP.SetText("R");
			this.AButtonTMP.fontSize = 62f;
			this.AButtonTMP.transform.localPosition = new Vector3(320f, 174f);
			this.AButtonTMP.color = new Color(1f, 1f, 1f);
			this.AButtonTMP.outlineWidth = 1f;
			this.AButtonTMP.outlineColor = new Color32(0, 0, 0, byte.MaxValue);
			this.hasCreatedTimerShit = true;
		}
		this.CS.referenceResolution = new Vector2(1280f, 720f);
	}

	// Token: 0x06000AC0 RID: 2752
	public void Update()
	{
		this.PlayerLandObj = GameObject.Find("PlayerLand");
		this.PlayerLand = this.PlayerLandObj.GetComponent<AudioSource>();
		this.LeImage = this.TestGOBJECT.GetComponent<Image>();
		this.CS.referenceResolution = new Vector2(1280f, 720f);
		this.ActiveScene = SceneManager.GetActiveScene().name;
		this.timePlaying = TimeSpan.FromSeconds((double)this.elapsedTime);
		this.CS.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		this.TCTextThing.text = Input.GetAxisRaw("Horizontal").ToString();
		if (Input.GetKeyDown(KeyCode.P))
		{
			this.AllEndings = !this.AllEndings;
			this.PlayerLand.Play();
		}
		if (this.ActiveScene == "Level1")
		{
			this.hasWatchedCutscene = true;
		}
		if (this.timerGoing)
		{
			this.elapsedTime += Time.unscaledDeltaTime;
		}
		if (Input.GetKeyDown(KeyCode.M))
		{
			this.AudioMuted = !this.AudioMuted;
		}
		if (!this.AudioMuted)
		{
			this.LevelAudioSource.volume = 1f;
			this.HallwayAudioSource.volume = 1f;
			this.CutsceneAudioSource.volume = 1f;
			this.BadEndingAudioSource.volume = 1f;
			this.MenuAudioSource.volume = 1f;
		}
		if (this.AudioMuted)
		{
			this.LevelAudioSource.volume = 0f;
			this.HallwayAudioSource.volume = 0f;
			this.CutsceneAudioSource.volume = 0f;
			this.BadEndingAudioSource.volume = 0f;
			this.MenuAudioSource.volume = 0f;
		}
		if (Input.GetKeyDown(KeyCode.F11))
		{
			if (!Screen.fullScreen)
			{
				Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
			}
			if (Screen.fullScreen)
			{
				Screen.SetResolution(this.normWidth, this.normHeight, false);
			}
		}
		this.JumpKeyObject.transform.localPosition = new Vector2(-235f, 335f);
		this.TCTextThing.transform.localPosition = new Vector2(-235f, 335f);
		this.TCTextThing.alignment = TextAlignmentOptions.MidlineLeft;
		UnityEngine.Object.DontDestroyOnLoad(this.WIBBLE);
		UnityEngine.Object.DontDestroyOnLoad(this.TestGOBJECT);
		if (this.countingEnabled)
		{
			this.timeCounter += Time.deltaTime;
		}
		if (this.GMScript.psychoCompleted && !this.PsychoGiven)
		{
			this.EndingCount++;
			this.PsychoGiven = true;
		}
		if (this.EndingCount == 5)
		{
			this.EndTimer();
		}
		this.ThemeMusicObject = GameObject.Find("Theme");
		this.TheGirlMusicObject = GameObject.Find("TheGirl");
		this.LevelMusicMusicObject = GameObject.Find("LevelMusic");
		this.SomethingsWrongMusicObject = GameObject.Find("SomethingsWrong");
		this.BadEndingMusicObject = GameObject.Find("BadEnding");
		this.audioLowPass1 = this.ThemeMusicObject.GetComponent<AudioLowPassFilter>();
		this.audioLowPass2 = this.TheGirlMusicObject.GetComponent<AudioLowPassFilter>();
		this.audioLowPass3 = this.LevelMusicMusicObject.GetComponent<AudioLowPassFilter>();
		this.audioLowPass4 = this.SomethingsWrongMusicObject.GetComponent<AudioLowPassFilter>();
		this.audioLowPass5 = this.BadEndingMusicObject.GetComponent<AudioLowPassFilter>();
		if (this.isGamePaused)
		{
			this.audioLowPass1.cutoffFrequency = 250f;
			this.audioLowPass2.cutoffFrequency = 250f;
			this.audioLowPass3.cutoffFrequency = 250f;
			this.audioLowPass4.cutoffFrequency = 250f;
			this.audioLowPass5.cutoffFrequency = 250f;
		}
		if (!this.isGamePaused)
		{
			this.audioLowPass1.cutoffFrequency = 99999f;
			this.audioLowPass2.cutoffFrequency = 99999f;
			this.audioLowPass3.cutoffFrequency = 99999f;
			this.audioLowPass4.cutoffFrequency = 99999f;
			this.audioLowPass5.cutoffFrequency = 99999f;
		}
		if (Input.GetAxisRaw("Horizontal") == 1f)
		{
			this.AButtonTMP.color = Color.HSVToRGB(this.ColorNEW, 1f, 1f);
		}
		if (Input.GetAxisRaw("Horizontal") < 1f)
		{
			this.AButtonTMP.color = new Color(1f, 1f, 1f);
		}
		if (Input.GetAxisRaw("Horizontal") == -1f)
		{
			this.WPressedTMP.color = Color.HSVToRGB(this.ColorNEW, 1f, 1f);
		}
		if (Input.GetAxisRaw("Horizontal") > -1f)
		{
			this.WPressedTMP.color = new Color(1f, 1f, 1f);
		}
		if (this.ColorNEW >= 1f)
		{
			this.ColorNEW = 0f;
		}
		this.ColorNEW += Time.deltaTime * 0.1f;
	}

	// Token: 0x06000AC2 RID: 2754
	public void EndTimer()
	{
		this.timerGoing = false;
	}

	// Token: 0x06000AC3 RID: 2755
	public void Awake()
	{
	}

	// Token: 0x04000D6D RID: 3437
	public float inGameTime;

	// Token: 0x04000D6E RID: 3438
	public float GameStartTime;

	// Token: 0x04000D6F RID: 3439
	public float frozenGameTime;

	// Token: 0x04000D70 RID: 3440
	public GameObject GMObject;

	// Token: 0x04000D71 RID: 3441
	public GameManager GMScript;

	// Token: 0x04000D72 RID: 3442
	public bool timerGoing;

	// Token: 0x04000D73 RID: 3443
	public float elapsedTime;

	// Token: 0x04000D74 RID: 3444
	public TimeSpan timePlaying;

	// Token: 0x04000D75 RID: 3445
	public bool AllEndings;

	// Token: 0x04000D76 RID: 3446
	public string ActiveScene;

	// Token: 0x04000D77 RID: 3447
	public bool hasWatchedCutscene;

	// Token: 0x04000D78 RID: 3448
	public AudioSource LevelAudioSource;

	// Token: 0x04000D79 RID: 3449
	public AudioSource HallwayAudioSource;

	// Token: 0x04000D7A RID: 3450
	public AudioSource CutsceneAudioSource;

	// Token: 0x04000D7B RID: 3451
	public AudioSource BadEndingAudioSource;

	// Token: 0x04000D7C RID: 3452
	public AudioSource MenuAudioSource;

	// Token: 0x04000D7D RID: 3453
	public bool AudioMuted;

	// Token: 0x04000D7E RID: 3454
	public int normWidth;

	// Token: 0x04000D7F RID: 3455
	public int normHeight;

	// Token: 0x04000D80 RID: 3456
	public GameObject WIBBLE;

	// Token: 0x04000D81 RID: 3457
	public Canvas TIMERCANVAS;

	// Token: 0x04000D82 RID: 3458
	public GameObject TextGO;

	// Token: 0x04000D83 RID: 3459
	public TextMeshProUGUI TCTextThing;

	// Token: 0x04000D84 RID: 3460
	public RectTransform rectTransform;

	// Token: 0x04000D85 RID: 3461
	public string hours;

	// Token: 0x04000D86 RID: 3462
	public string minutes;

	// Token: 0x04000D87 RID: 3463
	public string seconds;

	// Token: 0x04000D88 RID: 3464
	public string ms;

	// Token: 0x04000D89 RID: 3465
	public CanvasScaler CS;

	// Token: 0x04000D8A RID: 3466
	public int EndingCount;

	// Token: 0x04000D8B RID: 3467
	public bool AwakeGiven;

	// Token: 0x04000D8C RID: 3468
	public bool BLGiven;

	// Token: 0x04000D8D RID: 3469
	public bool PuppetGiven;

	// Token: 0x04000D8E RID: 3470
	public bool ILGiven;

	// Token: 0x04000D8F RID: 3471
	public bool PsychoGiven;

	// Token: 0x04000D90 RID: 3472
	public float timeCounter;

	// Token: 0x04000D91 RID: 3473
	public bool countingEnabled;

	// Token: 0x04000D92 RID: 3474
	public GameObject FADEPANEL;

	// Token: 0x04000D93 RID: 3475
	public PsychopathEndingController laPEC;

	// Token: 0x04000D94 RID: 3476
	public bool hasCreatedTimerShit;

	// Token: 0x04000D95 RID: 3477
	public AudioLowPassFilter audioLowPass1;

	// Token: 0x04000D96 RID: 3478
	public AudioLowPassFilter audioLowPass2;

	// Token: 0x04000D97 RID: 3479
	public AudioLowPassFilter audioLowPass3;

	// Token: 0x04000D98 RID: 3480
	public AudioLowPassFilter audioLowPass4;

	// Token: 0x04000D99 RID: 3481
	public AudioLowPassFilter audioLowPass5;

	// Token: 0x04000D9A RID: 3482
	public GameObject ThemeMusicObject;

	// Token: 0x04000D9B RID: 3483
	public GameObject TheGirlMusicObject;

	// Token: 0x04000D9C RID: 3484
	public GameObject LevelMusicMusicObject;

	// Token: 0x04000D9D RID: 3485
	public GameObject SomethingsWrongMusicObject;

	// Token: 0x04000D9E RID: 3486
	public GameObject BadEndingMusicObject;

	// Token: 0x04000D9F RID: 3487
	public bool epilepsyMode;

	// Token: 0x04000DA0 RID: 3488
	public bool isGamePaused;

	// Token: 0x04000DA1 RID: 3489
	public GameObject MasterID;

	// Token: 0x04000DA2 RID: 3490
	public GameObject JumpKeyObject;

	// Token: 0x04000DA3 RID: 3491
	public Image JumpKey;

	// Token: 0x04000DA4 RID: 3492
	public RectTransform secondRT;

	// Token: 0x04000DA5 RID: 3493
	public GameObject TestGOBJECT;

	// Token: 0x04000DA6 RID: 3494
	public Image LeImage;

	// Token: 0x04000DA7 RID: 3495
	public bool WPressed;

	// Token: 0x04000DA8 RID: 3496
	public TextMeshProUGUI WPressedTMP;

	// Token: 0x04000DA9 RID: 3497
	public GameObject Hahahaha;

	// Token: 0x04000DAA RID: 3498
	public GameObject AButtonE;

	// Token: 0x04000DAB RID: 3499
	public Image AButtonImageE;

	// Token: 0x04000DAC RID: 3500
	public GameObject AButtonObj;

	// Token: 0x04000DAD RID: 3501
	public TextMeshProUGUI AButtonTMP;

	// Token: 0x04000DAE RID: 3502
	public float ColorNEW;

	// Token: 0x04000DF1 RID: 3569
	public GameObject PlayerLandObj;

	// Token: 0x04000DF2 RID: 3570
	public AudioSource PlayerLand;
}
