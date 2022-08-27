using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DynamicBox.EventManagement;
using DynamicBox.EventManagement.Customers;
using DynamicBox.EventManagement.Refuel;
using DynamicBox.DataContainer;
using DynamicBox.UIEvents;
using DynamicBox.CoinsInScripts;
using DynamicBox.FuelSpend;

public class GetCustomers : MonoBehaviour
{
    private int customersCount;
    public RandomGenerate notPickedCustomers;
    public int coinsCount;
    private bool petrol;
    [SerializeField] private CarController speed;
    private void OnEnable()
    {
        EventManager.Instance.AddListener<OnCustomersGetOrDeliverEvent>(OnCustomersGetOrDeliverEventHandler);
        EventManager.Instance.AddListener<OnSpendFuelEvent>(OnSpendFuelEventHandler);
        EventManager.Instance.AddListener<OnLoginCoinSetEvent>(OnLoginCoinSetEventHandler);
    }
    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<OnCustomersGetOrDeliverEvent>(OnCustomersGetOrDeliverEventHandler);
        EventManager.Instance.RemoveListener<OnSpendFuelEvent>(OnSpendFuelEventHandler);
        EventManager.Instance.RemoveListener<OnLoginCoinSetEvent>(OnLoginCoinSetEventHandler);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer")&&petrol)
        {
            Destroy(other.gameObject);
            customersCount++;
        }
        else if (other.CompareTag("Station") && customersCount>0)
        {
            for(int i = 0; i < customersCount; i++)
            {
                coinsCount += Random.Range(5, 30);
            }
            notPickedCustomers.notPickedCustomers = 3-customersCount;
            customersCount = 0;
        }
        else if (other.CompareTag("PetrolStation"))
        {
            EventManager.Instance.AddListener<OnRefuelEvent>(OnRefuelEventHandler);
        }
        else
        {
            EventManager.Instance.RemoveListener<OnRefuelEvent>(OnRefuelEventHandler);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PetrolStation"))
        {
            EventManager.Instance.AddListener<OnRefuelEvent>(NoMoneyTextHandler);
        }
        else
        {
            EventManager.Instance.RemoveListener<OnRefuelEvent>(NoMoneyTextHandler);
        }
    }
    private void OnRefuelEventHandler(OnRefuelEvent eventDetails)
    {
        if(eventDetails.PetrolValue.value<eventDetails.PetrolValue.maxValue && (eventDetails.PetrolValue.maxValue - eventDetails.PetrolValue.value) < coinsCount)
        {
            coinsCount -= (int)(eventDetails.PetrolValue.maxValue - eventDetails.PetrolValue.value);
            eventDetails.PetrolValue.value = eventDetails.PetrolValue.maxValue;
        }
        else if(eventDetails.PetrolValue.value == eventDetails.PetrolValue.maxValue)
        {
            eventDetails.FuelOrMoney.text = "Full!";
        }
        else
        {
            eventDetails.FuelOrMoney.text = "Not Enough Money!";
        }

    }
    private void NoMoneyTextHandler(OnRefuelEvent eventDetails)
    {
        eventDetails.FuelOrMoney.text = "";
    }
    private void OnSpendFuelEventHandler(OnSpendFuelEvent eventDetails)
    {
        StartCoroutine(FuelSpending());
        IEnumerator FuelSpending()
        {
            if (eventDetails.PetrolValue.value < 10f)
            {
                petrol = eventDetails.Petrol = false;
                eventDetails.FuelOrMoney.text = "Go to the petrol station!";
                yield return new WaitForSeconds(1f);
            }
            else if (eventDetails.PetrolValue.value >= 10f)
            {
                petrol = eventDetails.Petrol = true;
                eventDetails.FuelOrMoney.text = "";
                yield return new WaitForSeconds(1f);
                eventDetails.PetrolValue.value -= (float)(speed.speed+1) / 150f;
            }
            StartCoroutine(FuelSpending());
        }
    }

    private void OnCustomersGetOrDeliverEventHandler(OnCustomersGetOrDeliverEvent eventDetails)
    {
        eventDetails.Customers.text = "" + customersCount;
        if (eventDetails.Coins.text != "" && coinsCount == 0)
        {
            coinsCount = int.Parse(eventDetails.Coins.text);
        }
        else
        {
            eventDetails.Coins.text = "" + coinsCount;
        }
    }
    private void OnLoginCoinSetEventHandler(OnLoginCoinSetEvent eventDetails)
    {
        coinsCount = eventDetails.coins;
    }

}
