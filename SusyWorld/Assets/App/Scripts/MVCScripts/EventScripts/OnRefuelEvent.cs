using DynamicBox.EventManagement;
using UnityEngine.UI;

namespace DynamicBox.EventManagement.Refuel
{
    public class OnRefuelEvent : GameEvent
    {
        public Slider PetrolValue;
        public Text FuelOrMoney;

        public OnRefuelEvent(Slider petrolValue, Text fuelOrMoney)
        {
            PetrolValue = petrolValue;
            FuelOrMoney = fuelOrMoney;

        }
    }
}
