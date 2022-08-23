using DynamicBox.EventManagement;
using UnityEngine.UI;

namespace DynamicBox.FuelSpend
{
    public class OnSpendFuelEvent : GameEvent
    {
        public Slider PetrolValue;
        public bool Petrol;
        public Text FuelOrMoney;
        public OnSpendFuelEvent(Slider petrolValue, bool petrol,Text fuelOrMoney)
        {
            PetrolValue = petrolValue;
            Petrol = petrol;
            FuelOrMoney = fuelOrMoney;
        }
    }
}
