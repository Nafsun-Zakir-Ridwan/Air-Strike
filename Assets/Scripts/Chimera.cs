using Spine.Unity;
using System;
using UnityEngine;

public class Chimera : MonoBehaviour
{
	public SkeletonDataAsset skeletonDataSource;

	[SpineAttachment(false, true, false, "", "skeletonDataSource", "", true, false)]
	public string attachmentPath;

	[SpineSlot("", "", false, true, false)]
	public string targetSlot;

	private void Start()
	{
		base.GetComponent<SkeletonRenderer>().skeleton.FindSlot(this.targetSlot).Attachment = SpineAttachment.GetAttachment(this.attachmentPath, this.skeletonDataSource);
	}
}
