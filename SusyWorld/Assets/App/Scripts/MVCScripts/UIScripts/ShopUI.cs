using DynamicBox.EventManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject carModel;
    [SerializeField] private GetCustomers coins;
    [SerializeField] private AudioSource cashRegisterSource;
    [SerializeField] private AudioClip cashRegisterSound;
    public void OnBuyButtonClicked(Button shopButton)
    {
        int i = 0;
        if (int.TryParse(shopButton.GetComponentInChildren<Text>().text, out i) && coins.coinsCount >= i)
        {
            coins.coinsCount -= int.Parse(shopButton.GetComponentInChildren<Text>().text);
            shopButton.GetComponentInChildren<Text>().text = "";
            shopButton.image.color = new Color(1,1,1,1);
            cashRegisterSource.PlayOneShot(cashRegisterSound);
        }
        else if (shopButton.image.color == new Color(1, 1, 1, 1))
        {
            carModel.GetComponent<MeshRenderer>().material = shopButton.GetComponentInChildren<MeshRenderer>().material;
        }
    }
}
