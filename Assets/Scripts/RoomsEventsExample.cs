using System;
using UnityEngine;

public class RoomsEventsExample : MonoBehaviour
{
	public void StartedRoomTransition(int currentRoom, int previousRoom)
	{
		UnityEngine.Debug.LogFormat("Started Room Transition - Current Room:{0} - Previous Room:{1}", new object[]
		{
			currentRoom,
			previousRoom
		});
	}

	public void FinishedRoomTransition(int currentRoom, int previousRoom)
	{
		UnityEngine.Debug.LogFormat("Finished Room Transition - Current Room:{0} - Previous Room:{1}", new object[]
		{
			currentRoom,
			previousRoom
		});
	}
}
