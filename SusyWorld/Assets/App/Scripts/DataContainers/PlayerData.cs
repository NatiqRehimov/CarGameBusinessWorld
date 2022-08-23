namespace DynamicBox.DataContainer
{
    public class PlayerData
    {
        public string Name;
        public string Coins = "0";

        public PlayerData(string _name,string _coins)
        {
            Name = _name;
            Coins = _coins;
        }
    }
}
