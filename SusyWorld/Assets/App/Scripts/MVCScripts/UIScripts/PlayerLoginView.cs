using DynamicBox.EventManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.UIControllers;
using UnityEngine.UI;
using DynamicBox.DataContainer;
using DynamicBox.CheckCar;
using System;

namespace DynamicBox.UIViews
{
    public class PlayerLoginView : MonoBehaviour
	{
		[SerializeField] private PlayerLoginController controller;

		[SerializeField] private InputField playerNameInputField;

		[SerializeField] private GameObject loginPanel;

		[SerializeField] private GameObject infoPanel;

		[SerializeField] private GameObject shopPanel;

		[SerializeField] private Text nameText;

		[SerializeField] private Text coinsText;

		private string coins;
		private bool inShop;
        private void OnEnable()
        {
			EventManager.Instance.AddListener<CarInTunningPlaceEvent>(CarInTunningPlaceEventHandler);
        }
        private void OnDisable()
		{
			EventManager.Instance.RemoveListener<CarInTunningPlaceEvent>(CarInTunningPlaceEventHandler);
		}
		private void CarInTunningPlaceEventHandler(CarInTunningPlaceEvent eventDetails)
		{
			inShop = eventDetails.inShop;
		}
		private void Update()
        {
			shopPanel.SetActive(inShop);
        }

        public void OnLoginButtonPressed()
		{
			if (playerNameInputField.text == "DynamicBox")
			{
				coins = "999999";
				controller.OnLoginButtonPressed(playerNameInputField.text, coins);
			}
			else if (playerNameInputField.text != null && playerNameInputField.text.Length > 0)
			{
				coins = "10";
				controller.OnLoginButtonPressed(playerNameInputField.text, coins);
			}
			loginPanel.SetActive(false);
			shopPanel.SetActive(false);
			infoPanel.SetActive(true);

			nameText.text = playerNameInputField.text;
			coinsText.text = coins;
		}

		public void SetupPlayerData(PlayerData playerData)
		{
			loginPanel.SetActive(false);
			shopPanel.SetActive(false);
			infoPanel.SetActive(true);

			nameText.text = playerData.Name;
			coinsText.text = playerData.Coins;
		}
		public void OnShopClose()
        {
			inShop = false;
        }
	}
}
