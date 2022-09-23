using UnityEngine;
using DynamicBox.EventManagement;
namespace DynamicBox.CheckCar
{
    public class CarInTunningPlaceEvent : GameEvent
    {
        public bool inShop;

        public CarInTunningPlaceEvent(bool inShop)
        {
            this.inShop = inShop;
        }
    }
}