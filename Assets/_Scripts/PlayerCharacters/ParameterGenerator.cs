
    public class ParameterGenerator
    {
        public int CurrentValue { get; set; }
        public int BaseValue { get; set; }
        private float Multiplier { get; set; } = 1;
        private int Bonus { get; set; } = 0;

        public ParameterGenerator(int baseValue)
        {
            BaseValue = baseValue;
            CurrentValue = baseValue;
            
        }
        public void ModifyValueAdd(int bonus)
        {
            this.Bonus += bonus;
            ParameterUpdate();
        }
        
        public void ModifyValueMultiply(float multiplier)
        {
            this.Multiplier = 1 + (multiplier * 0.01f);
            ParameterUpdate();
        }

        public void ParameterUpdate()
        {
            
            CurrentValue = (int) (BaseValue  * Multiplier  + Bonus);
        }
    }
