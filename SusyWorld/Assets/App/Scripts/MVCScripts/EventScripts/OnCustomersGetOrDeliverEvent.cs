using DynamicBox.EventManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DynamicBox.EventManagement.Customers
{
    public class OnCustomersGetOrDeliverEvent : GameEvent
    {
        public Text Customers;
        public Text Coins;

        public OnCustomersGetOrDeliverEvent(Text customers, Text coins)
        {
            Customers = customers;
            Coins = coins;
        }
    }
}
