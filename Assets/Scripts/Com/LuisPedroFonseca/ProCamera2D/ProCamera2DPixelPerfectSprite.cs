using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[ExecuteInEditMode, HelpURL("http://www.procamera2d.com/user-guide/extension-pixel-perfect/")]
	public class ProCamera2DPixelPerfectSprite : BasePC2D, IPostMover
	{
		public bool IsAMovingObject;

		public bool IsAChildSprite;

		public Vector2 LocalPosition;

		[Range(-8f, 32f)]
		public int SpriteScale;

		private Sprite _sprite;

		private ProCamera2DPixelPerfect _pixelPerfectPlugin;

		[SerializeField]
		private Vector3 _initialScale = Vector3.one;

		private int _prevSpriteScale;

		private int _pmOrder = 2000;

		public int PMOrder
		{
			get
			{
				return this._pmOrder;
			}
			set
			{
				this._pmOrder = value;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			if (this.ProCamera2D == null)
			{
				base.enabled = false;
				return;
			}
			this.GetPixelPerfectPlugin();
			this.GetSprite();
			this.ProCamera2D.AddPostMover(this);
		}

		private void Start()
		{
			this.SetAsPixelPerfect();
		}

		public void PostMove(float deltaTime)
		{
			if (base.enabled)
			{
				this.Step();
			}
		}

		private void Step()
		{
			if (!this._pixelPerfectPlugin.enabled)
			{
				return;
			}
			if (this.IsAMovingObject)
			{
				this.SetAsPixelPerfect();
			}
			this._prevSpriteScale = this.SpriteScale;
		}

		private void GetPixelPerfectPlugin()
		{
			this._pixelPerfectPlugin = this.ProCamera2D.GetComponent<ProCamera2DPixelPerfect>();
		}

		private void GetSprite()
		{
			SpriteRenderer component = base.GetComponent<SpriteRenderer>();
			if (component != null)
			{
				this._sprite = component.sprite;
			}
		}

		public void SetAsPixelPerfect()
		{
			if (this.IsAChildSprite)
			{
				this.Trnsform.localPosition = this.VectorHVD(Utils.AlignToGrid(this.LocalPosition.x, this._pixelPerfectPlugin.PixelStep), Utils.AlignToGrid(this.LocalPosition.y, this._pixelPerfectPlugin.PixelStep), this.Vector3D(this.Trnsform.localPosition));
			}
			this.Trnsform.position = this.VectorHVD(Utils.AlignToGrid(this.Vector3H(this.Trnsform.position), this._pixelPerfectPlugin.PixelStep), Utils.AlignToGrid(this.Vector3V(this.Trnsform.position), this._pixelPerfectPlugin.PixelStep), this.Vector3D(this.Trnsform.position));
			if (this.SpriteScale == 0)
			{
				if (this._prevSpriteScale == 0)
				{
					this._initialScale = this.Trnsform.localScale;
				}
				else
				{
					this.Trnsform.localScale = this._initialScale;
				}
			}
			else
			{
				float num = (this.SpriteScale >= 0) ? ((float)this.SpriteScale) : (1f / (float)this.SpriteScale * -1f);
				float num2 = this._sprite.pixelsPerUnit * num * (1f / this._pixelPerfectPlugin.PixelsPerUnit);
				this.Trnsform.localScale = new Vector3(Mathf.Sign(this.Trnsform.localScale.x) * num2, Mathf.Sign(this.Trnsform.localScale.y) * num2, this.Trnsform.localScale.z);
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (this.ProCamera2D != null)
			{
				this.ProCamera2D.RemovePostMover(this);
			}
		}
	}
}
