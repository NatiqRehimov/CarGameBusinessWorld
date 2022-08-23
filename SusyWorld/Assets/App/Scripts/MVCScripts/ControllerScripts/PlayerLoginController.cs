using UnityEngine;
using DynamicBox.UIViews;
using DynamicBox.UIEvents;
using DynamicBox.EventManagement;

namespace DynamicBox.UIControllers
{
	public class PlayerLoginController : MonoBehaviour
	{
		[SerializeField] private PlayerLoginView view;


		private void OnEnable()
		{
			EventManager.Instance.AddListener<OnPlayerDataExistsEvent>(OnPlayerDataExistsEventHandler);
		}

		private void OnDisable()
		{
			EventManager.Instance.RemoveListener<OnPlayerDataExistsEvent>(OnPlayerDataExistsEventHandler);
		}

		public void OnLoginButtonPressed(string name , string coins)
		{
			EventManager.Instance.Raise(new OnLoginButtonPressedEvent(name,coins));
		}

		private void OnPlayerDataExistsEventHandler(OnPlayerDataExistsEvent eventDetails)
		{
			view.SetupPlayerData(eventDetails.PlayerData);
		}
	}
}