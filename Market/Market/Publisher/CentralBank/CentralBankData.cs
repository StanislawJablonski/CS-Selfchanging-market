namespace Market.Publisher.CentralBank
{
    public class CentralBankData
    {
        public float InflationLevel { get; set; }

        public CentralBankData(float inflationLevel)
        {
            InflationLevel = inflationLevel;
        }
    }
}