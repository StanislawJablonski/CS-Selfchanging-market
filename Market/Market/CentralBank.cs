using Market.Publisher.CentralBank;

namespace Market
{
    public class CentralBank
    {
        private float _inflation;
        private float _previousIncome;
        private float _income;
        public InflationProvider Provider { get; set; }

        public CentralBank(float inflation, float previousIncome) {
            _inflation = inflation;
            _previousIncome = previousIncome;
            _income = 0;
            Provider = new InflationProvider();
            Provider.SetMeasurements(inflation);
        }
        private void CalculateInflation() {
            if (_previousIncome > 30)
                _inflation = 1 + 0.20f * (1- _income / _previousIncome);
            else
                _inflation -= 0.05f;
            Provider.SetMeasurements(_inflation);
        }

        public void NextPeriod() {
            CalculateInflation();
            _previousIncome = _income;
            _income = 0;
        }

        public void PayTax(float tax) {
            _income += tax;
        }

        public float CheckPublicIncome() {
            return _income;
        }

        public override string ToString()
        {
            return $"Inflation: {_inflation}, Income: {_income}";
        }

        public float GetInflation()
        {
            return _inflation;
        }
        
    }
}