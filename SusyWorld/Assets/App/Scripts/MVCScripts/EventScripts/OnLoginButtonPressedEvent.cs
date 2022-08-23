using DynamicBox.EventManagement;

namespace DynamicBox.UIEvents 
{
    public class OnLoginButtonPressedEvent : GameEvent
    {
        public string Name;
        public string Coins;

        public OnLoginButtonPressedEvent(string _name,string _coins)
        {
            Name = _name;
            Coins = _coins;
        }
    }
}
