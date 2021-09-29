using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020001CC RID: 460
public class SplashScreen : MonoBehaviour
{
	// Token: 0x0600099C RID: 2460 RVA: 0x0000F968 File Offset: 0x0000DB68
	private void Update()
	{
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && this._canSkipSplash)
		{
			base.GetComponent<Animator>().SetTrigger("Skip");
			this._canSkipSplash = false;
		}
		UnityEngine.Object.DontDestroyOnLoad(this.timerObj);
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x0000F9A5 File Offset: 0x0000DBA5
	public void AllowSkip()
	{
		this._canSkipSplash = true;
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x0000F9AE File Offset: 0x0000DBAE
	public void GoToMainMenu()
	{
		this.uiManager.StartSceneChange("MainMenu");
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x00031F2C File Offset: 0x0003012C
	public void Start()
	{
		this.timerObj = new GameObject();
		this.timerObj.AddComponent<TimerController>();
		this.timerObj.name = "TimerController";
		this.timerScript = this.timerObj.GetComponent<TimerController>();
		UnityEngine.Object.DontDestroyOnLoad(this.timerObj);
		SceneManager.LoadScene("MainMenu");
	}

	// Token: 0x04000BBE RID: 3006
	[SerializeField]
	private bool _canSkipSplash;

	// Token: 0x04000BBF RID: 3007
	public UIManager uiManager;

	// Token: 0x04000BC0 RID: 3008
	public GameObject timerObj;

	// Token: 0x04000BC1 RID: 3009
	public TimerController timerScript;
}
