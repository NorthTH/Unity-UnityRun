using UnityEngine;
using System.Collections;

public class PlayerContorller : MonoBehaviour {

	private Vector2 startPos;
	private Vector2 direction;
	private Vector2 endPos;

	private float clickTime;
	private float distance;
	private float angle;

	private bool isClick = false;
	private bool isDirectClick = false;
	private bool isTabToLeft = false;
	private bool isTabToRight = false;
	private bool isTabToTop = false;
	private bool isTabToBottom = false;

	// Use this for initialization
	void Start () {
		clickTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (true) {
			if (Input.GetMouseButtonDown (0))
				BeginClick ();
			if (Input.GetMouseButtonUp (0))
				EndClick ();
			if (Input.GetMouseButton (0))
				TrackingClick ();
		}
	}

	void clickTimeout()
	{
		isDirectClick = false;
		isTabToLeft = false;
		isTabToRight = false;
		isTabToTop = false;
		isTabToBottom = false;
	}

	private void BeginClick(){
		startPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		//Debug.Log ("startPos :" + startPos.x + " " + startPos.y);
	}

	private void EndClick(){
		endPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

		if (clickTime <= 0.5f) {
			isClick = true;
		}

		if (isClick && (distance <= 1.5f))
			isDirectClick = true;
		
		if (isClick && (-45f < angle && angle < 45f))
			isTabToLeft = true;

		if (isClick && ((angle > 135f || angle < -135f) && angle != 360.0f))
			isTabToRight = true;

		if (isClick && (45f < angle && angle < 135f))
			isTabToTop = true;

		if (isClick && (-135f < angle && angle < -45))
			isTabToBottom = true;

		//値リセット
		clickTime = 0.0f;
		isClick = false;

		Invoke ("clickTimeout", 0.1f);
	}

	private void TrackingClick(){
		distance = 0.0f;
		angle = 360.0f;
		clickTime += Time.deltaTime;
		Vector2 currentPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

		distance = Vector2.Distance (currentPos, startPos) / 50.0f;
		if (distance >= 1.5f) {
			direction = currentPos - startPos;
			float sign = (currentPos.y < startPos.y) ? -1.0f : 1.0f;
			angle = (Vector2.Angle (Vector2.left, direction) * sign);
			//Debug.Log (angle);
		}
	}

	public bool IsClick()
	{
		return isClick;
	}

	public bool IsDirectClick()
	{
		if (isDirectClick) {
			isDirectClick = false;
			return true;
		}
		else
			return false;
	}

	public bool IsTabToLeft()
	{
		if (isTabToLeft) {
			isTabToLeft = false;
			return true;
		}
		else
			return false;
	}

	public bool IsTabToRight()
	{
		if (isTabToRight) {
			isTabToRight = false;
			return true;
		}
		else
			return false;
	}

	public bool IsTabToTop()
	{
		if (isTabToTop) {
			isTabToTop = false;
			return true;
		}
		else
			return false;
	}

	public bool IsTabToBottom()
	{
		if (isTabToBottom) {
			isTabToBottom = false;
			return true;
		}
		else
			return false;
	}
}
