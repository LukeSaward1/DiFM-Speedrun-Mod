using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020001C6 RID: 454
public class Player : MonoBehaviour
{
	// Token: 0x06000978 RID: 2424 RVA: 0x0003139C File Offset: 0x0002F59C
	private void Start()
	{
		this._rb = base.GetComponent<Rigidbody2D>();
		this._animator = base.GetComponent<Animator>();
		this._gameManager = GameManager.instance;
		if (this._gameManager.lastCheckpoint != Vector3.zero)
		{
			base.transform.position = this._gameManager.lastCheckpoint;
		}
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x000313FC File Offset: 0x0002F5FC
	private void Update()
	{
		this.TIMEROBJ = GameObject.Find("TimerController");
		this.TheTC = this.TIMEROBJ.GetComponent<TimerController>();
		if (this.isDisabled)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.R) && this._allowRestart)
		{
			this._gameManager.AddRestart();
			this._gameManager.RemoveDeath();
			this.Die();
		}
		this.FlipPlayer();
		this.Attack();
		this.ControlCoyoteTime();
		this.Jump();
		this._isGrounded = this.IsGrounded();
		Vector3 position = this._rb.transform.position;
		this.posx = position.x.ToString();
		position = this._rb.transform.position;
		this.posy = position.y.ToString();
		this.GMObject = GameObject.Find("GameManager");
		this.GMScript = this.GMObject.GetComponent<GameManager>();
		if (Input.GetKeyDown(KeyCode.F1))
		{
			this.GMScript.ResetValues();
			this.GMScript.lastCameraPosition = new Vector3(0f, 0f, -10f);
			this._rb.transform.position = new Vector2(-9.5f, -3f);
			this._rb.position = new Vector2(-9.5f, -3f);
			this.GMScript.SetLastCheckpoint(this._rb.transform.position);
			this.GMScript.checkpointNumber = 0;
			if (this.TheTC.timerGoing && this.TheTC.hasWatchedCutscene && this.TheTC.elapsedTime > 0f)
			{
				this.TheTC.timerGoing = false;
				this.TheTC.elapsedTime = 0f;
			}
			this.TheTC.AwakeGiven = false;
			this.TheTC.BLGiven = false;
			this.TheTC.PuppetGiven = false;
			this.TheTC.PsychoGiven = false;
			this.TheTC.ILGiven = false;
			this.TheTC.EndingCount = 0;
			AudioManager.instance.StopAllMusic();
			SceneManager.LoadScene("MainMenu");
		}
		if (Input.GetKeyDown(KeyCode.F4))
		{
			this.GMScript.ResetValues();
			this.GMScript.lastCameraPosition = new Vector3(0f, 0f, -10f);
			this._rb.transform.position = new Vector2(-9.5f, -3f);
			this._rb.position = new Vector2(-9.5f, -3f);
			this.GMScript.SetLastCheckpoint(this._rb.transform.position);
			this.GMScript.checkpointNumber = 0;
			if (this.TheTC.timerGoing && this.TheTC.hasWatchedCutscene && this.TheTC.elapsedTime > 0f)
			{
				this.TheTC.timerGoing = false;
				this.TheTC.elapsedTime = 0f;
			}
			this.TheTC.countingEnabled = true;
			this.TheTC.AwakeGiven = false;
			this.TheTC.BLGiven = false;
			this.TheTC.PuppetGiven = false;
			this.TheTC.PsychoGiven = false;
			this.TheTC.ILGiven = false;
			this.TheTC.EndingCount = 0;
			AudioManager.instance.StopAllMusic();
			SceneManager.LoadScene("MainMenu");
		}
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x0000F833 File Offset: 0x0000DA33
	private void FixedUpdate()
	{
		this.Movement();
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x0003177C File Offset: 0x0002F97C
	private void Movement()
	{
		if (this._canMove)
		{
			float num = Input.GetAxisRaw("Horizontal") * this._speed * Time.deltaTime;
			this._rb.velocity = new Vector2(num, this._rb.velocity.y);
			this._animator.SetInteger("xSpeed", Mathf.FloorToInt(Mathf.Abs(num)));
			this._animator.SetFloat("ySpeed", this._rb.velocity.y);
		}
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x00031808 File Offset: 0x0002FA08
	private void FlipPlayer()
	{
		if (this._rb.velocity.x < 0f && !this._isFlipped)
		{
			this._isFlipped = true;
			this._newScale.x = -1f;
		}
		else if (this._rb.velocity.x > 0f && this._isFlipped)
		{
			this._isFlipped = false;
			this._newScale.x = 1f;
		}
		base.transform.localScale = this._newScale;
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x00031894 File Offset: 0x0002FA94
	private void ControlCoyoteTime()
	{
		if (this.IsGrounded())
		{
			if (this._groundCheckEnabled)
			{
				this._coyoteTime = this._maxCoyoteTime;
				this._animator.SetBool("Jumping", false);
				this._isFalling = false;
			}
		}
		else
		{
			this._coyoteTime -= Time.deltaTime;
			this._animator.SetBool("Jumping", true);
		}
		if (!this._groundCheckEnabled)
		{
			this._groundCheckTimer -= Time.deltaTime * 3f;
			if (this._groundCheckTimer < 0f)
			{
				this._groundCheckEnabled = true;
				this._groundCheckTimer = 1f;
			}
		}
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x0003193C File Offset: 0x0002FB3C
	private void Jump()
	{
		if (Input.GetButtonDown("Jump") && this._coyoteTime > 0f && this._canJump)
		{
			this._canJump = false;
			this._groundCheckEnabled = false;
			this._hasHitFloor = false;
			this._rb.gravityScale = 2f;
			this._coyoteTime = -10000f;
			this._rb.velocity = new Vector2(this._rb.velocity.x, this._jumpForce * Time.fixedDeltaTime);
			AudioManager.instance.PlaySound("PlayerJump");
		}
		if (Input.GetButtonUp("Jump") || this._rb.velocity.y < 0f)
		{
			if (this._rb.velocity.y > 0f)
			{
				this._rb.velocity = new Vector2(this._rb.velocity.x, this._rb.velocity.y * this._fallForce);
			}
			this._rb.gravityScale = 4f;
			this._canJump = true;
			this._isFalling = true;
		}
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x0000F83B File Offset: 0x0000DA3B
	private bool IsGrounded()
	{
		return Physics2D.OverlapBox(this._groundCheck.position, this._groundCheckSize, 0f, LayerMask.GetMask(new string[]
		{
			"Ground"
		}));
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x00031A68 File Offset: 0x0002FC68
	private void Attack()
	{
		if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0)) && this._canAttack && this.IsGrounded())
		{
			this._canJump = false;
			this._canAttack = false;
			this._canMove = false;
			this._animator.SetTrigger("Attack");
			AudioManager.instance.PlaySound("PlayerAttack");
			this._rb.velocity = new Vector2(0f, 0f);
		}
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x0000F875 File Offset: 0x0000DA75
	public void FinishAttack()
	{
		this._canMove = true;
		this._canAttack = true;
		this._canJump = true;
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x0000F88C File Offset: 0x0000DA8C
	public void SetCheckpoint(Vector3 newCheckpoint)
	{
		this._checkpoint = newCheckpoint;
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x00031AF0 File Offset: 0x0002FCF0
	public void Die()
	{
		AudioManager.instance.PlaySound("PlayerHurt2");
		this._gameManager.PreviousValues();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		this._gameManager.AddDeath();
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x0000F89A File Offset: 0x0000DA9A
	public void Step()
	{
		this._stepParticle.Play();
		AudioManager.instance.PlaySound("PlayerLand");
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x0000F8B6 File Offset: 0x0000DAB6
	public void DisablePlayer()
	{
		this._speed = 0f;
		this._canMove = false;
		this.isDisabled = true;
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x00031B34 File Offset: 0x0002FD34
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Checkpoint"))
		{
			this._gameManager.SetLastCheckpoint(other.transform.position);
			UnityEngine.Object.Destroy(other.gameObject);
		}
		if (other.CompareTag("DeathCollision"))
		{
			this.Die();
		}
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0000F8D1 File Offset: 0x0000DAD1
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (this._isFalling)
		{
			this.Step();
		}
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x0000F8E1 File Offset: 0x0000DAE1
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireCube(this._groundCheck.position, this._groundCheckSize);
	}

	// Token: 0x04000B8F RID: 2959
	public float _speed;

	// Token: 0x04000B90 RID: 2960
	[SerializeField]
	private float _jumpForce;

	// Token: 0x04000B91 RID: 2961
	[SerializeField]
	[Range(0f, 1f)]
	private float _fallForce;

	// Token: 0x04000B92 RID: 2962
	[SerializeField]
	private float _coyoteTime;

	// Token: 0x04000B93 RID: 2963
	[SerializeField]
	private float _maxCoyoteTime = 1f;

	// Token: 0x04000B94 RID: 2964
	[SerializeField]
	private float _groundCheckTimer = 1f;

	// Token: 0x04000B95 RID: 2965
	[SerializeField]
	private ParticleSystem _stepParticle;

	// Token: 0x04000B96 RID: 2966
	[SerializeField]
	private Transform _groundCheck;

	// Token: 0x04000B97 RID: 2967
	[SerializeField]
	private Vector2 _groundCheckSize;

	// Token: 0x04000B98 RID: 2968
	public bool _canMove = true;

	// Token: 0x04000B99 RID: 2969
	public bool isDisabled;

	// Token: 0x04000B9A RID: 2970
	public bool _allowRestart = true;

	// Token: 0x04000B9B RID: 2971
	[SerializeField]
	private bool _isGrounded;

	// Token: 0x04000B9C RID: 2972
	[SerializeField]
	private bool _groundCheckEnabled = true;

	// Token: 0x04000B9D RID: 2973
	[SerializeField]
	private bool _canAttack = true;

	// Token: 0x04000B9E RID: 2974
	[SerializeField]
	private bool _canJump = true;

	// Token: 0x04000B9F RID: 2975
	[SerializeField]
	private bool _hasHitFloor = true;

	// Token: 0x04000BA0 RID: 2976
	[SerializeField]
	private bool _isFlipped;

	// Token: 0x04000BA1 RID: 2977
	[SerializeField]
	private bool _isFalling;

	// Token: 0x04000BA2 RID: 2978
	private Vector3 _newScale = Vector3.one;

	// Token: 0x04000BA3 RID: 2979
	private Vector2 _checkpoint;

	// Token: 0x04000BA4 RID: 2980
	private Rigidbody2D _rb;

	// Token: 0x04000BA5 RID: 2981
	private Animator _animator;

	// Token: 0x04000BA6 RID: 2982
	private GameManager _gameManager;

	// Token: 0x04000BA7 RID: 2983
	public GameManager GMScript;

	// Token: 0x04000BA8 RID: 2984
	public GameObject GMObject;

	// Token: 0x04000BA9 RID: 2985
	public string posx;

	// Token: 0x04000BAA RID: 2986
	public string posy;

	// Token: 0x04000BAB RID: 2987
	public TimerController TheTC;

	// Token: 0x04000BAC RID: 2988
	public GameObject TIMEROBJ;
}
