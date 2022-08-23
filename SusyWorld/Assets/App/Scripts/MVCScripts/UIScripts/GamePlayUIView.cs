using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;
using DynamicBox.EventManagement.Customers;
using DynamicBox.EventManagement.Speed;
using UnityEngine.UI;
using System;
using DynamicBox.EventManagement.Refuel;
using DynamicBox.FuelSpend;

public class GamePlayUIView : MonoBehaviour
{
    [SerializeField] private Text speed;
    [SerializeField] private Text customers;
    [SerializeField] private Text coins;
    [SerializeField] private Text fuelAndMoney;
    [SerializeField] private Slider petrolValue;
    [SerializeField] private bool petrol;

    private void Start()
    {
        petrolValue.value = petrolValue.maxValue;
        SpendFuel();
    }
    private void Update()
    {
        ShowSpeed();
        ShowCoinsAndCustomers();
        Refueling();
    }
    private void ShowCoinsAndCustomers()
    {
        EventManager.Instance.Raise(new OnCustomersGetOrDeliverEvent(customers,coins));
    }

    private void ShowSpeed()
    {
        EventManager.Instance.Raise(new OnPlayerMoveEvent(speed));
    }
    public void Refueling()
    {
        EventManager.Instance.Raise(new OnRefuelEvent(petrolValue,fuelAndMoney));
    }
    public void SpendFuel()
    {
        EventManager.Instance.Raise(new OnSpendFuelEvent(petrolValue, petrol,fuelAndMoney));
    }
}
