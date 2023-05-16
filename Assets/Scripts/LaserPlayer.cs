using System;
using UnityEngine;

public class LaserPlayer : MonoBehaviour
{
	[global::Tooltip("Laser instantiate position")]
	public Transform rayBeginPos;

	[global::Tooltip("Laser Hit Layer")]
	public LayerMask enemyHitLayer;

	[global::Tooltip("Allocate the The Laser Size"), Range(5f, 50f)]
	public float maxLaserSize;

	[Range(1f, 20f)]
	public float laserGlowMultiplier;

	[global::Tooltip("Emitter when laser collide with Enemy/EnemyHitLayer"), Space]
	public GameObject laserHitEmitter;

	[global::Tooltip("Emitter when laser start firingr")]
	public GameObject laserMeltEmitter;

	[global::Tooltip("Laser Damage amount for enemy")]
	public float laserDamage;

	[global::Tooltip("Laser Firing Duration")]
	public float rayDuration;

	[global::Tooltip("The Laser Glow Sprite")]
	public Transform laserGlow;

	private bool laserOn;

	private LineRenderer lineRenderer;

	private float length;

	private float lerpTime = 1f;

	private float currentLerpTime;

	private GameObject hitParticle;

	private GameObject meltParticle;

	private Vector3 endPos;

	private bool canFire = true;

	public GameObject Pos;

	private void OnEnable()
	{
		this.laserGlow.gameObject.SetActive(false);
		this.lineRenderer = base.GetComponent<LineRenderer>();
		this.lineRenderer.enabled = false;
		this.currentLerpTime = 0f;
		this.laserOn = true;
		this.laserDamage = TASData.Instance.LstLaser[TASData.Instance.LstItemPP[6].Lv];
	}

	private void OnDisable()
	{
		this.laserOn = false;
		UnityEngine.Object.Destroy(this.hitParticle);
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (this.laserOn)
		{
			float distance = this.maxLaserSize;
			this.endPos = this.Pos.transform.position;
			Vector2 up = Vector2.up;
			float num = Mathf.Atan2(this.Pos.transform.position.y - this.rayBeginPos.position.y, this.Pos.transform.position.x - this.rayBeginPos.position.x) * 57.29578f - 90f;
			this.lineRenderer.enabled = true;
			RaycastHit2D hit = Physics2D.Raycast(this.rayBeginPos.position, this.Pos.transform.position - this.rayBeginPos.position, distance, this.enemyHitLayer);
			if (this.meltParticle == null)
			{
				this.meltParticle = UnityEngine.Object.Instantiate<GameObject>(this.laserMeltEmitter, this.rayBeginPos.position + new Vector3(0f, 0.1f), Quaternion.identity);
				this.meltParticle.transform.parent = base.transform;
			}
			if (hit.collider != null)
			{
				this.lineRenderer.SetPosition(0, this.rayBeginPos.position);
				this.lineRenderer.SetPosition(1, hit.point);
				hit = this.laserColGlow(hit);
				if (this.hitParticle == null)
				{
					this.hitParticle = UnityEngine.Object.Instantiate<GameObject>(this.laserHitEmitter, hit.point, Quaternion.identity);
				}
				if (this.hitParticle != null)
				{
					this.hitParticle.transform.position = hit.point;
				}
				if (hit.collider.tag == "Enemy")
				{
					hit.collider.gameObject.GetComponent<TASEnemy>().LaserHit(this.laserDamage);
				}
				else if (hit.collider.tag == "GunBoss")
				{
					hit.collider.gameObject.GetComponent<GunBossCs>().LaserHit(this.laserDamage);
				}
			}
			else
			{
				if (this.hitParticle != null)
				{
					UnityEngine.Object.Destroy(this.hitParticle);
				}
				float num2 = this._lerpLaser();
				Vector2 v = Vector2.Lerp(this.rayBeginPos.position, this.endPos, num2 * 3f);
				this.lineRenderer.SetPosition(0, this.rayBeginPos.position);
				this.lineRenderer.SetPosition(1, v);
				this.laserNotColGlow();
			}
		}
		else if (!this.laserOn)
		{
			if (this.hitParticle != null)
			{
				UnityEngine.Object.Destroy(this.hitParticle);
			}
			if (this.meltParticle != null)
			{
				UnityEngine.Object.Destroy(this.meltParticle);
			}
			this.turnOfLaser();
			this.laserGlow.gameObject.SetActive(false);
		}
	}

	private void laserNotColGlow()
	{
		if (!this.laserGlow.gameObject.activeInHierarchy)
		{
			this.laserGlow.gameObject.SetActive(true);
		}
		float t = this._lerpLaser();
		float num = this.endPos.y + this.maxLaserSize * this.laserGlowMultiplier;
		float y = Mathf.Lerp(this.laserGlow.localScale.y, -num, t);
		this.laserGlow.localScale = new Vector2(this.laserGlow.localScale.x, y);
	}

	private RaycastHit2D laserColGlow(RaycastHit2D hit)
	{
		if (!this.laserGlow.gameObject.activeInHierarchy)
		{
			this.laserGlow.gameObject.SetActive(true);
		}
		this.laserGlow.localScale = new Vector2(this.laserGlow.localScale.x, -hit.distance * 4.35f);
		return hit;
	}

	private void enableFiring()
	{
		this.canFire = true;
	}

	private float _lerpLaser()
	{
		this.currentLerpTime += Time.deltaTime;
		if (this.currentLerpTime > this.lerpTime)
		{
			this.currentLerpTime = this.lerpTime;
		}
		return this.currentLerpTime / this.lerpTime;
	}

	private void turnOfLaser()
	{
		Vector2 b = this.lineRenderer.GetPosition(0);
		Vector2 a = this.lineRenderer.GetPosition(1);
		this.length = (a - b).magnitude;
		float t = this._lerpLaser();
		Vector2 v = Vector2.Lerp(a, b, t);
		if (this.length > 0.3f)
		{
			this.lineRenderer.SetPosition(1, v);
		}
		else
		{
			this.lineRenderer.enabled = false;
			this.currentLerpTime = 0f;
		}
	}
}
