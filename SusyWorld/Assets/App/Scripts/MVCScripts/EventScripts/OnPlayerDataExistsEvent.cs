using DynamicBox.DataContainer;
using DynamicBox.EventManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnPlayerDataExistsEvent : GameEvent
{
	public PlayerData PlayerData;

	public OnPlayerDataExistsEvent(PlayerData _playerData)
	{
		PlayerData = _playerData;
	}
}
