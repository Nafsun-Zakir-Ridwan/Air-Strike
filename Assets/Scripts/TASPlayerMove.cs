using Com.LuisPedroFonseca.ProCamera2D;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TASPlayerMove : MonoBehaviour
{
	public static TASPlayerMove Instance;

	public int speed = 10;

	private Transform Trns;

	public Vector2 distance;

	private float MinX;

	private float MinY;

	private float MaxX;

	private float MaxY;

	public bool _isMove;

	private Vector2 ToPosition;

	public Camera MainCamera;

	public Vector2 OldPos;

	public bool IsStart;

	public bool IsWin;

	public Transform PointFlyWin;

	public void Awake()
	{
		TASPlayerMove.Instance = this;
		this.Trns = base.transform;
		this.MainCamera = Camera.main;
		this.OldPos = base.transform.position;
	}

	private void OnEnable()
	{
		this.IsStart = false;
		this.ResetPlayer();
	}

	public void ResetPlayer()
	{
		this.IsWin = false;
		this._isMove = false;
		base.transform.position = this.OldPos;
		this.ToPosition = base.transform.position;
		GPSManager.Instance.ShowSelectItem(true, false);
		this.MainCamera.transform.position = new Vector3(0f, 0f, -10f);
		this.MainCamera.GetComponent<ProCamera2D>().ResetMovement();
		this.MainCamera.GetComponent<ProCamera2D>().enabled = true;
	}

	private void Start()
	{
		float orthographicSize = this.MainCamera.orthographicSize;
		float num = this.MainCamera.aspect * orthographicSize;
		this.MaxX = num;
		this.MinX = -num;
		this.MaxY = orthographicSize;
		this.MinY = -orthographicSize;
	}

	public void Update()
	{
		if (this.IsWin)
		{
			return;
		}
		if (this.IsStart && !GPSManager.Instance.IsPause)
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				this.MobileMove();
			}
			else
			{
				this.PCMove();
			}
		}
	}

	private void PCMove()
	{
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.currentSelectedGameObject)
		{
			this._isMove = true;
			Time.timeScale = 1f;
			if (PlayerPrefs.GetInt("ModeMove", 1) == 1)
			{
				this.distance = Vector2.zero;
			}
			else
			{
				this.distance = base.transform.position - this.MainCamera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			}
			GPSManager.Instance.SelectItem.SetActive(false);
			GPSManager.Instance.Health1Player.gameObject.SetActive(false);
			this.MainCamera.GetComponent<ProCamera2D>().enabled = true;
		}
		else if (Input.GetMouseButtonUp(0) && !EventSystem.current.currentSelectedGameObject)
		{
			this._isMove = false;
			this.ToPosition = base.transform.position;
			Time.timeScale = 0.5f;
			GPSManager.Instance.ShowSelectItem(false, false);
			GPSManager.Instance.ShowHealth1();
			this.MainCamera.GetComponent<ProCamera2D>().enabled = false;
		}
		else if (Input.GetMouseButton(0) && !EventSystem.current.currentSelectedGameObject && !this._isMove)
		{
			this._isMove = true;
			Time.timeScale = 1f;
			if (PlayerPrefs.GetInt("ModeMove", 1) == 1)
			{
				this.distance = Vector2.zero;
			}
			else
			{
				this.distance = base.transform.position - this.MainCamera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			}
			GPSManager.Instance.SelectItem.SetActive(false);
			this.MainCamera.GetComponent<ProCamera2D>().enabled = true;
		}
		this.OnMove(this.MainCamera.ScreenToWorldPoint(UnityEngine.Input.mousePosition));
	}

	private void MobileMove()
	{
		if (UnityEngine.Input.touchCount > 0)
		{
			if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.currentSelectedGameObject)
			{
				this._isMove = true;
				Time.timeScale = 1f;
				if (PlayerPrefs.GetInt("ModeMove", 1) == 1)
				{
					this.distance = Vector2.zero;
				}
				else
				{
					this.distance = base.transform.position - this.MainCamera.ScreenToWorldPoint(UnityEngine.Input.GetTouch(0).position);
				}
				GPSManager.Instance.SelectItem.SetActive(false);
				GPSManager.Instance.Health1Player.gameObject.SetActive(false);
				this.MainCamera.GetComponent<ProCamera2D>().enabled = true;
			}
			else if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Moved && !EventSystem.current.currentSelectedGameObject)
			{
				if (!this._isMove)
				{
					this._isMove = true;
					Time.timeScale = 1f;
					if (PlayerPrefs.GetInt("ModeMove", 1) == 1)
					{
						this.distance = Vector2.zero;
					}
					else
					{
						this.distance = base.transform.position - this.MainCamera.ScreenToWorldPoint(UnityEngine.Input.GetTouch(0).position);
					}
					GPSManager.Instance.SelectItem.SetActive(false);
					this.MainCamera.GetComponent<ProCamera2D>().enabled = true;
				}
			}
			else if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Ended && !EventSystem.current.currentSelectedGameObject)
			{
				this._isMove = false;
				Time.timeScale = 0.5f;
				this.ToPosition = base.transform.position;
				GPSManager.Instance.ShowSelectItem(false, false);
				GPSManager.Instance.ShowHealth1();
				this.MainCamera.GetComponent<ProCamera2D>().enabled = false;
			}
			if (this._isMove)
			{
				this.OnMove(this.MainCamera.ScreenToWorldPoint(UnityEngine.Input.GetTouch(0).position));
			}
		}
	}

	private void OnStartMove(Vector3 position)
	{
		this._isMove = true;
		Time.timeScale = 1f;
		this.distance = base.transform.position - position;
	}

	private void OnMove(Vector2 position)
	{
		if (!this._isMove)
		{
			return;
		}
		if (PlayerPrefs.GetInt("ModeMove", 1) == 1)
		{
			this.distance = Vector2.zero + new Vector2(0f, 1f);
		}
		this.ToPosition = position + this.distance;
		if (this.ToPosition.x < this.MinX)
		{
			this.ToPosition.x = this.MinX;
		}
		if (this.ToPosition.x > this.MaxX)
		{
			this.ToPosition.x = this.MaxX;
		}
		if (this.ToPosition.y < this.MinY)
		{
			this.ToPosition.y = this.MinY;
		}
		if (this.ToPosition.y > this.MaxY)
		{
			this.ToPosition.y = this.MaxY;
		}
		base.transform.position = Vector2.Lerp(base.transform.position, this.ToPosition, (float)this.speed * Time.deltaTime);
	}

	private void OnEndMove()
	{
		this._isMove = false;
		this.ToPosition = base.transform.position;
	}

	public void FlyWin()
	{
		if (!GPSManager.Instance.PlayerDie)
		{
			GPSManager.Instance.HideScoreAndBtnPause();
			base.transform.DOMove(this.PointFlyWin.position, 2f, false).OnComplete(delegate
			{
				if (Advertisements.Instance && Advertisements.Instance.IsInterstitialAvailable())
				{
					Advertisements.Instance.ShowInterstitial(new UnityAction(this.ShowEndWin));
				}
				else
				{
					this.ShowEndWin();
				}
			});
		}
	}

	public void ShowEndWin()
	{
		PopupManager.Instance.ShowEnd(true);
	}

	private void OnDisable()
	{
		base.transform.position = this.OldPos;
		GPSManager.Instance.SelectItem.SetActive(false);
		GPSManager.Instance.Health1Player.gameObject.SetActive(false);
	}
}
