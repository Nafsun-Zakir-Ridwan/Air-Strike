using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashCs : MonoBehaviour
{
	private sealed class _LoadAsync_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal AsyncOperation _async___0;

		internal float _progress___1;

		internal SplashCs _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _LoadAsync_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._async___0 = SceneManager.LoadSceneAsync("GameScene");
				break;
			case 1u:
				break;
			default:
				return false;
			}
			if (!this._async___0.isDone)
			{
				this._progress___1 = Mathf.Clamp01(this._async___0.progress / 0.9f);
				this._this.Loading.text = (int)(this._progress___1 * 100f) + "%";
				this._current = null;
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public Image Load;

	public Text Loading;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
	{
		this.Load.fillAmount = 0f;
		this.Loading.text = this.Load.fillAmount + "%";
		base.StartCoroutine(this.LoadAsync());
	}

	private IEnumerator LoadAsync()
	{
		SplashCs._LoadAsync_c__Iterator0 _LoadAsync_c__Iterator = new SplashCs._LoadAsync_c__Iterator0();
		_LoadAsync_c__Iterator._this = this;
		return _LoadAsync_c__Iterator;
	}
}
