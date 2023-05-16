using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
	public static PathController Instance;

	public List<PathInfor> Paths;

	private void Awake()
	{
		PathController.Instance = this;
	}

	private void Start()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				transform.gameObject.transform.DOKill(false);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	[ContextMenu("Remove AutoPlay")]
	public void RemoveAutoPlay()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				transform.gameObject.GetComponent<DOTweenPath>().autoPlay = false;
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	[ContextMenu("Add Path")]
	public int AddNewPath()
	{
		GameObject gameObject = new GameObject((base.transform.childCount + 1).ToString());
		DOTweenPath dOTweenPath = gameObject.AddComponent<DOTweenPath>();
		dOTweenPath.pathType = PathType.CatmullRom;
		gameObject.transform.SetParent(base.transform);
		gameObject.transform.localPosition = Vector3.zero;
		List<Vector3> wps = gameObject.GetComponent<DOTweenPath>().wps;
		for (int i = 0; i < wps.Count; i++)
		{
			wps[i] = wps[i];
		}
		this.Paths.Add(new PathInfor
		{
			PathName = "Path " + (this.Paths.Count + 1),
			Points = wps
		});
		return this.Paths.Count - 1;
	}

	[ContextMenu("Update Path")]
	public void UpdatePath()
	{
		this.Paths.Clear();
		int num = 1;
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				transform.name = num.ToString();
				List<Vector3> wps = transform.GetComponent<DOTweenPath>().wps;
				for (int i = 0; i < wps.Count; i++)
				{
					wps[i] = wps[i];
				}
				this.Paths.Add(new PathInfor
				{
					PathName = "Path " + num,
					Points = wps
				});
				num++;
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}
}
