using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DynamicBox.EventManagement;
using DynamicBox.EventManagement.Customers;
using DynamicBox.EventManagement.Refuel;
using DynamicBox.DataContainer;
using DynamicBox.UIEvents;
using DynamicBox.FuelSpend;
using DynamicBox.CheckCar;

public class GetCustomers : MonoBehaviour
{
    [SerializeField] public GameObject Car;
    [SerializeField] private AudioSource carDoorSlamSource;
    [SerializeField] private AudioClip[] carDoorSlamClip;
    private int customersCount;
    public RandomGenerate notPickedCustomers;
    public int coinsCount;
    private bool petrol;
    private int[] numberOfClone = new int[3];
    [SerializeField] private CarController speed;
    private bool inShop;
    private void OnEnable()
    {
        EventManager.Instance.AddListener<OnSpendFuelEvent>(OnSpendFuelEventHandler);
        EventManager.Instance.AddListener<OnPlayerDataExistsEvent>(OnPlayerDataExistsEventHandler);
    }
    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<OnCustomersGetOrDeliverEvent>(OnCustomersGetOrDeliverEventHandler);
        EventManager.Instance.RemoveListener<OnSpendFuelEvent>(OnSpendFuelEventHandler);
        EventManager.Instance.RemoveListener<OnPlayerDataExistsEvent>(OnPlayerDataExistsEventHandler);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer")&&petrol)
        {
            for (int i = 0; i < notPickedCustomers.availableM.Length; i++)
            {
                if (other.name[0] - 49 == i)
                {
                    notPickedCustomers.availableM[i] = 3;
                    numberOfClone[i] = i;
                    Destroy(other.gameObject);
                    customersCount++;
                    carDoorSlamSource.PlayOneShot(carDoorSlamClip[0],1);
                } 
            }
            for (int i = 0; i < notPickedCustomers.stationsOrCustomers.Length; i++)
            {
                if (other.transform.position == notPickedCustomers.stationsOrCustomers[i].position)
                {
                    notPickedCustomers.stationPositionNumber = i;
                }
            }
            

        }
    
        else if (other.CompareTag("Station") && customersCount>0)
        {
            for (int i = 0; i < notPickedCustomers.stationsOrCustomers.Length; i++)
            {
                if (other.transform.position == notPickedCustomers.stationsOrCustomers[i].position && other.name[0]-49 == numberOfClone[other.name[0]-49])
                {
                    notPickedCustomers.availableT[i] = true;
                    notPickedCustomers.availableM[other.name[0] - 49] = 1;
                    coinsCount += Random.Range(10, 30);
                    customersCount --;
                    Destroy(other.gameObject);
                    carDoorSlamSource.PlayOneShot(carDoorSlamClip[1],1);
                    EventManager.Instance.AddListener<OnCustomersGetOrDeliverEvent>(OnCustomersGetOrDeliverEventHandler);
                }
            }
        }
        else if (other.CompareTag("PetrolStation"))
        {
            EventManager.Instance.AddListener<OnRefuelEvent>(OnRefuelEventHandler);
            EventManager.Instance.AddListener<OnCustomersGetOrDeliverEvent>(OnCustomersGetOrDeliverEventHandler);
        }
        else if (other.CompareTag("PaintShop"))
        {
            inShop = true;
            EventManager.Instance.Raise(new CarInTunningPlaceEvent(inShop));
        }
        else
        {
            EventManager.Instance.RemoveListener<OnRefuelEvent>(OnRefuelEventHandler);
            EventManager.Instance.AddListener<OnCustomersGetOrDeliverEvent>(OnCustomersGetOrDeliverEventHandler);
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
        if(eventDetails.PetrolValue.value<eventDetails.PetrolValue.maxValue && (eventDetails.PetrolValue.maxValue - eventDetails.PetrolValue.value) < coinsCount*10)
        {
            coinsCount -= ((int)(eventDetails.PetrolValue.maxValue - eventDetails.PetrolValue.value))/10;
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
    private void OnPlayerDataExistsEventHandler(OnPlayerDataExistsEvent eventDetails)
    {
            int.TryParse(eventDetails.PlayerData.Coins, out coinsCount);
    }

}
