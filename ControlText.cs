using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020001A8 RID: 424
public class ControlText : MonoBehaviour
{
	// Token: 0x060008CE RID: 2254 RVA: 0x0002F550 File Offset: 0x0002D750
	private void Start()
	{
		this.GMObject = GameObject.Find("GameManager");
		this.gameManager = this.GMObject.GetComponent<GameManager>();
		this.TimerObj = GameObject.Find("TimerController");
		this.TimerScript = this.TimerObj.GetComponent<TimerController>();
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x0002F5A0 File Offset: 0x0002D7A0
	private void Update()
	{
		if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && this.canChangeText)
		{
			this.canChangeText = false;
			this.lineIndex++;
			if (this.lineIndex <= this.lines.Length - 1)
			{
				base.GetComponent<Animator>().SetTrigger("ChangeText");
				this.text.text = this.lines[this.lineIndex];
			}
			else
			{
				AudioManager.instance.StopAllMusic();
				if (SceneManager.GetActiveScene().name == "Ending2BlindLove")
				{
					if (!this.TimerScript.AllEndings)
					{
						this.TimerScript.EndTimer();
					}
					this.gameManager.blindLoveCompleted = true;
					if (!this.TimerScript.BLGiven)
					{
						this.TimerScript.EndingCount++;
						this.TimerScript.BLGiven = true;
					}
					AudioManager.instance.PlayMusic("JumpScare");
					this.uiManager.StartSceneChange(this.nextScene);
				}
				else if (SceneManager.GetActiveScene().name == "Ending4TrueLove")
				{
					this.gameManager.innocentLoveCompleted = true;
					if (!this.TimerScript.AllEndings)
					{
						this.TimerScript.EndTimer();
					}
					if (!this.TimerScript.ILGiven)
					{
						this.TimerScript.EndingCount++;
						this.TimerScript.ILGiven = true;
					}
					AudioManager.instance.PlaySound("PlayerHurt2");
					AudioManager.instance.ChangePitch("JumpScare", 1.5f);
					AudioManager.instance.PlayMusic("JumpScare");
					if (this.redScreen != null)
					{
						this.redScreen.SetTrigger("Death");
					}
					base.StartCoroutine(this.ChangeScene());
				}
				else
				{
					if (this.nextScene == "Ending")
					{
						if (SceneManager.GetActiveScene().name == "Ending0Awake")
						{
							if (!this.TimerScript.AllEndings)
							{
								this.TimerScript.EndTimer();
							}
							this.gameManager.awakeCompleted = true;
							if (!this.TimerScript.AwakeGiven)
							{
								this.TimerScript.EndingCount++;
								this.TimerScript.AwakeGiven = true;
							}
						}
						if (SceneManager.GetActiveScene().name == "Ending1Puppet")
						{
							if (!this.TimerScript.AllEndings)
							{
								this.TimerScript.EndTimer();
							}
							this.gameManager.puppetCompleted = true;
							if (!this.TimerScript.PuppetGiven)
							{
								this.TimerScript.EndingCount++;
								this.TimerScript.PuppetGiven = true;
							}
						}
					}
					this.uiManager.StartSceneChange(this.nextScene);
				}
			}
		}
		if (SceneManager.GetActiveScene().name == "Ending4TrueLove" && this.lineIndex == 13 && !this._hasStoppedAudio)
		{
			this._hasStoppedAudio = true;
			AudioManager.instance.StopAllMusic();
		}
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x0000F376 File Offset: 0x0000D576
	public IEnumerator ChangeScene()
	{
		yield return new WaitForSeconds(1f);
		this.uiManager.StartSceneChange(this.nextScene);
		yield break;
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x0000F385 File Offset: 0x0000D585
	public void TextHasReachedEnd()
	{
		this.canChangeText = true;
	}

	// Token: 0x04000AF3 RID: 2803
	public bool canChangeText;

	// Token: 0x04000AF4 RID: 2804
	public string[] lines;

	// Token: 0x04000AF5 RID: 2805
	public int lineIndex;

	// Token: 0x04000AF6 RID: 2806
	public TextMeshProUGUI text;

	// Token: 0x04000AF7 RID: 2807
	public Animator redScreen;

	// Token: 0x04000AF8 RID: 2808
	[SerializeField]
	private bool _hasStoppedAudio;

	// Token: 0x04000AF9 RID: 2809
	public string nextScene;

	// Token: 0x04000AFA RID: 2810
	public UIManager uiManager;

	// Token: 0x04000AFB RID: 2811
	public GameManager gameManager;

	// Token: 0x04000AFC RID: 2812
	public GameObject GMObject;

	// Token: 0x04000AFD RID: 2813
	public GameObject TimerObj;

	// Token: 0x04000AFE RID: 2814
	public TimerController TimerScript;
}
