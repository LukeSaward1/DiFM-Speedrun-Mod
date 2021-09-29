using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020001B2 RID: 434
public class GameManager : MonoBehaviour
{
	// Token: 0x06000903 RID: 2307 RVA: 0x0000F522 File Offset: 0x0000D722
	public GameManager()
	{
		this.lastCameraPosition = new Vector3(0f, 0f, -10f);
		this.checkpointNumber = -1;
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x00030158 File Offset: 0x0002E358
	private void Awake()
	{
		if (GameManager.instance != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		GameManager.instance = this;
		UnityEngine.Object.DontDestroyOnLoad(this);
		this.normWidth = Screen.width;
		this.normHeight = Screen.height;
		this.ActWidth = Screen.currentResolution.width;
		this.ActHeight = Screen.currentResolution.height;
		this.ThePickup._heartObject.SetActive(true);
		this.ThePickup._woofleObject.SetActive(true);
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x0000F54B File Offset: 0x0000D74B
	private void Start()
	{
		this.LoadGame();
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x0000F553 File Offset: 0x0000D753
	public void SetLastCheckpoint(Vector3 newcheckpoint)
	{
		this.lastCheckpoint = newcheckpoint;
		this.checkpointNumber++;
		this.previousHeartAmount = this.heartAmount;
		this.previousEnemiesKilledAmount = this.enemiesKilledAmount;
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x0000F582 File Offset: 0x0000D782
	public void PreviousValues()
	{
		this.checkpointNumber--;
		this.heartAmount = this.previousHeartAmount;
		this.enemiesKilledAmount = this.previousEnemiesKilledAmount;
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x0000F5AA File Offset: 0x0000D7AA
	public void AddHeart()
	{
		this.heartAmount++;
		this.totalHeartsCollected++;
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x0000F5C8 File Offset: 0x0000D7C8
	public void AddEnemy()
	{
		this.enemiesKilledAmount++;
		this.totalEnemiesKilled++;
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x0000F5E6 File Offset: 0x0000D7E6
	public void AddDeath()
	{
		this.totalDeaths++;
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x0000F5F6 File Offset: 0x0000D7F6
	public void RemoveDeath()
	{
		this.totalDeaths--;
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x0000F606 File Offset: 0x0000D806
	public void AddRestart()
	{
		this.totalRestarts++;
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x0000F616 File Offset: 0x0000D816
	public void AddGamesPlayed()
	{
		this.totalOfGamesPlayed++;
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x0000F626 File Offset: 0x0000D826
	public void ResetValues()
	{
		this.heartAmount = 0;
		this.previousHeartAmount = 0;
		this.enemiesKilledAmount = 0;
		this.previousEnemiesKilledAmount = 0;
		AudioManager.instance.ChangePitch("LevelMusic", 1f);
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x000301E8 File Offset: 0x0002E3E8
	public void SaveGame()
	{
		if (this.hasFinishedGame)
		{
			PlayerPrefs.SetInt("HasFinishedGame", 1);
		}
		if (this.has0AwakeEnding)
		{
			PlayerPrefs.SetInt("Has0AwakeEnding", 1);
		}
		if (this.has1PuppetEnding)
		{
			PlayerPrefs.SetInt("Has1PuppetEnding", 1);
		}
		if (this.has2BlindLoveEnding)
		{
			PlayerPrefs.SetInt("Has2BlindLoveEnding", 1);
		}
		if (this.has3PsychoEnding)
		{
			PlayerPrefs.SetInt("Has3PsychoEnding", 1);
		}
		if (this.has4TrueLoveEnding)
		{
			PlayerPrefs.SetInt("Has4TrueLoveEnding", 1);
		}
		PlayerPrefs.SetInt("TotalHeartsCollected", this.totalHeartsCollected);
		PlayerPrefs.SetInt("TotalEnemiesKilled", this.totalEnemiesKilled);
		PlayerPrefs.SetInt("TotalOfGamesPlayed", this.totalOfGamesPlayed);
		PlayerPrefs.SetInt("TotalOfDeaths", this.totalDeaths);
		PlayerPrefs.SetInt("TotalofRestarts", this.totalRestarts);
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x000302B8 File Offset: 0x0002E4B8
	public void LoadGame()
	{
		if (PlayerPrefs.GetInt("HasFinishedGame") == 1)
		{
			this.hasFinishedGame = true;
		}
		if (PlayerPrefs.GetInt("Has0AwakeEnding") == 1)
		{
			this.has0AwakeEnding = true;
		}
		if (PlayerPrefs.GetInt("Has1PuppetEnding") == 1)
		{
			this.has1PuppetEnding = true;
		}
		if (PlayerPrefs.GetInt("Has2BlindLoveEnding") == 1)
		{
			this.has2BlindLoveEnding = true;
		}
		if (PlayerPrefs.GetInt("Has3PsychoEnding") == 1)
		{
			this.has3PsychoEnding = true;
		}
		if (PlayerPrefs.GetInt("Has4TrueLoveEnding") == 1)
		{
			this.has4TrueLoveEnding = true;
		}
		this.totalHeartsCollected = PlayerPrefs.GetInt("TotalHeartsCollected", 0);
		this.totalEnemiesKilled = PlayerPrefs.GetInt("TotalEnemiesKilled", 0);
		this.totalOfGamesPlayed = PlayerPrefs.GetInt("TotalOfGamesPlayed", 0);
		this.totalDeaths = PlayerPrefs.GetInt("TotalOfDeaths", 0);
		this.totalRestarts = PlayerPrefs.GetInt("TotalofRestarts", 0);
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x00030394 File Offset: 0x0002E594
	public void ResetGame()
	{
		PlayerPrefs.SetInt("HasFinishedGame", 0);
		PlayerPrefs.SetInt("Has0AwakeEnding", 0);
		PlayerPrefs.SetInt("Has1PuppetEnding", 0);
		PlayerPrefs.SetInt("Has2BlindLoveEnding", 0);
		PlayerPrefs.SetInt("Has3PsychoEnding", 0);
		PlayerPrefs.SetInt("Has4TrueLoveEnding", 0);
		this.hasFinishedGame = false;
		this.has0AwakeEnding = false;
		this.has1PuppetEnding = false;
		this.has2BlindLoveEnding = false;
		this.has3PsychoEnding = false;
		this.has4TrueLoveEnding = false;
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x00030410 File Offset: 0x0002E610
	public void Update()
	{
		this.TheSceneName = SceneManager.GetActiveScene().name;
		this.UI = GameObject.Find("UI");
		if (Input.GetKey(KeyCode.U))
		{
			this.IGT += Time.deltaTime;
		}
	}

	// Token: 0x04000B23 RID: 2851
	public static GameManager instance;

	// Token: 0x04000B24 RID: 2852
	public Vector3 lastCheckpoint;

	// Token: 0x04000B25 RID: 2853
	public Vector3 lastCameraPosition;

	// Token: 0x04000B26 RID: 2854
	public int checkpointNumber;

	// Token: 0x04000B27 RID: 2855
	[Header("Stats")]
	public int heartAmount;

	// Token: 0x04000B28 RID: 2856
	public int previousHeartAmount;

	// Token: 0x04000B29 RID: 2857
	public int enemiesKilledAmount;

	// Token: 0x04000B2A RID: 2858
	public int previousEnemiesKilledAmount;

	// Token: 0x04000B2B RID: 2859
	public bool hasFinishedGame;

	// Token: 0x04000B2C RID: 2860
	[Header("Secret Stats")]
	public int totalHeartsCollected;

	// Token: 0x04000B2D RID: 2861
	public int totalEnemiesKilled;

	// Token: 0x04000B2E RID: 2862
	public int totalOfGamesPlayed;

	// Token: 0x04000B2F RID: 2863
	public int totalDeaths;

	// Token: 0x04000B30 RID: 2864
	public int totalRestarts;

	// Token: 0x04000B31 RID: 2865
	[Header("Endings")]
	public int endingIndex;

	// Token: 0x04000B32 RID: 2866
	public bool has0AwakeEnding;

	// Token: 0x04000B33 RID: 2867
	public bool has1PuppetEnding;

	// Token: 0x04000B34 RID: 2868
	public bool has2BlindLoveEnding;

	// Token: 0x04000B35 RID: 2869
	public bool has3PsychoEnding;

	// Token: 0x04000B36 RID: 2870
	public bool has4TrueLoveEnding;

	// Token: 0x04000B37 RID: 2871
	public string curRes;

	// Token: 0x04000B38 RID: 2872
	public int normWidth;

	// Token: 0x04000B39 RID: 2873
	public int normHeight;

	// Token: 0x04000B3A RID: 2874
	public int ActWidth;

	// Token: 0x04000B3B RID: 2875
	public int ActHeight;

	// Token: 0x04000B3C RID: 2876
	public GameObject UI;

	// Token: 0x04000B3D RID: 2877
	public string TheSceneName;

	// Token: 0x04000B3E RID: 2878
	public bool isDisabled;

	// Token: 0x04000B3F RID: 2879
	public PickupCounterUI ThePickup;

	// Token: 0x04000B40 RID: 2880
	public bool heartEnabled;

	// Token: 0x04000B41 RID: 2881
	public bool woofleEnabled;

	// Token: 0x04000B42 RID: 2882
	public bool puppetCompleted;

	// Token: 0x04000B43 RID: 2883
	public bool awakeCompleted;

	// Token: 0x04000B44 RID: 2884
	public bool psychoCompleted;

	// Token: 0x04000B45 RID: 2885
	public bool innocentLoveCompleted;

	// Token: 0x04000B46 RID: 2886
	public bool blindLoveCompleted;

	// Token: 0x04000B47 RID: 2887
	public bool isTimerGoing;

	// Token: 0x04000B48 RID: 2888
	public float IGT;
}
