using DynamicBox.CoinsInScripts;
using DynamicBox.DataContainer;
using DynamicBox.EventManagement;
using DynamicBox.SaveManagement;
using DynamicBox.UIEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataSaver : MonoBehaviour
{

	[SerializeField] private string containerName = "PlayerData";

	SaveManager saveManager;




	#region Unity Methods

	private void Awake()
	{
		saveManager = new SaveManager(StorageMethod.JSON);
	}

	private void Start()
	{
		if (saveManager.FileExists(containerName))
		{
			PlayerData playerData = saveManager.LoadFromFile<PlayerData>(containerName, null);

			EventManager.Instance.Raise(new OnPlayerDataExistsEvent(playerData));
			EventManager.Instance.Raise(new OnLoginCoinSetEvent(int.Parse(playerData.Coins)));
		}
	}

	private void OnEnable()
	{
		EventManager.Instance.AddListener<OnLoginButtonPressedEvent>(OnLoginButtonPressedEventHandler);
	}

	private void OnDisable()
	{
		EventManager.Instance.RemoveListener<OnLoginButtonPressedEvent>(OnLoginButtonPressedEventHandler);

	}

	#endregion

	private void OnLoginButtonPressedEventHandler(OnLoginButtonPressedEvent eventDetails)
	{
			PlayerData loginPlayerData = new PlayerData(eventDetails.Name,eventDetails.Coins);

			saveManager.SaveToFile<PlayerData>(loginPlayerData, containerName);
	}
}
