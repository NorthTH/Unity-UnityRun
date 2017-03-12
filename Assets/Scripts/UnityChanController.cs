using UnityEngine;
using System.Collections;

public class UnityChanController : MonoBehaviour {

	public float Speed = 5f;

	public float SpeedRatio = 1f;

	public float jumpForce = 300f;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;

	bool doubleJump = false;

	private Rigidbody myRigitbody;

	private Animator myAnimator;

	private PlayerContorller myPlayerController;

	public GameObject myPuniCon;


	// Use this for initialization
	void Awake () {
		myRigitbody = GetComponent<Rigidbody> ();
		myAnimator = GetComponent<Animator> ();
		myPlayerController = myPuniCon.GetComponent<PlayerContorller> ();
	}

	// Update is called once per frame
	void FixedUpdate () {

		grounded = Physics.Raycast(groundCheck.position, -Vector3.up, groundRadius);
		myAnimator.SetBool ("IsGround", grounded);

		if (grounded)
			doubleJump = false;

		myAnimator.SetFloat ("vSpeed", myRigitbody.velocity.y);

		Vector3 movement = transform.forward * Speed * SpeedRatio * Time.deltaTime;
		this.myRigitbody.MovePosition (myRigitbody.position + movement);
		this.myRigitbody.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);

		if (SpeedRatio >= 2)
			this.myAnimator.SetFloat ("Speed", 2);
		else if (SpeedRatio >= 1)
			this.myAnimator.SetFloat ("Speed", 1);
		else
			this.myAnimator.SetFloat ("Speed", 0);
	}

	void Update()
	{
		if ((grounded || !doubleJump) && myPlayerController.IsDirectClick()) {
			myAnimator.SetBool ("IsGround", false);
			myRigitbody.velocity = new Vector3 (0, 0, 0);
			myRigitbody.AddForce (new Vector3 (0, jumpForce, 0));

			if (!doubleJump && !grounded)
				doubleJump = true;
		}

		if (myPlayerController.IsTabToRight ())
			SpeedRatio = 2;
		else if (myPlayerController.IsTabToLeft ())
			SpeedRatio = 1;

		if (myPlayerController.IsTabToBottom ()) {
			myRigitbody.velocity = new Vector3 (0, 0, 0);
			myRigitbody.AddForce (new Vector3 (0, -jumpForce * 2.0f, 0));
		}
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "CoinTag") {

			//パーティクルを再生（追加）
			GetComponent<ParticleSystem> ().Play ();

			//接触したコインのオブジェクトを破棄（追加）
			Destroy (other.gameObject);
		}
	}
}
