using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.UIControllers;
using UnityEngine.UI;
using DynamicBox.DataContainer;
using System;

namespace DynamicBox.UIViews
{
    public class PlayerLoginView : MonoBehaviour
	{
		[SerializeField] private PlayerLoginController controller;

		[SerializeField] private InputField playerNameInputField;

		[SerializeField] private GameObject loginPanel;

		[SerializeField] private GameObject infoPanel;

		[SerializeField] private Text nameText;

		[SerializeField] private Text coinsText;

        public void OnLoginButtonPressed()
		{
			if (playerNameInputField.text == "DynamicBox")
			{
				controller.OnLoginButtonPressed(playerNameInputField.text, "999999");
			}
			else if (playerNameInputField.text != null && playerNameInputField.text.Length > 0)
			{
				controller.OnLoginButtonPressed(playerNameInputField.text, "10");
			}
		}

		public void SetupPlayerData(PlayerData playerData)
		{
			loginPanel.SetActive(false);

			infoPanel.SetActive(true);

			nameText.text = playerData.Name;
			coinsText.text = playerData.Coins;
		}
    }
}
