
    public class ActiveCondition 
    {
        public int Duration { get; set; }
        public Condition Condition { get; set; }
        
        
        public ActiveCondition(Condition _condition, int _duration)
        {
            Condition = _condition;
            Duration = _duration;
        }
    }
