using DynamicBox.EventManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DynamicBox.CoinsInScripts
{
    public class OnLoginCoinSetEvent : GameEvent
    {
        public int coins;

        public OnLoginCoinSetEvent(int coins)
        {
            this.coins = coins;
        }
    }
}
