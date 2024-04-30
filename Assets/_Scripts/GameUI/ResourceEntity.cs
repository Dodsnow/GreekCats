using UnityEngine;

namespace GameUI
{
    public class ResourceEntity
    {
        public int CurrentValue = 0;
        
        public ResourceEntity(int currentValue)
        {
            this.CurrentValue = currentValue;
        }

        void AddResource(int amount)
        {
            CurrentValue += amount;
        }

        void RemoveResource(int amount)
        {
                CurrentValue -= amount;
                if (CurrentValue < 0)
                {
                    CurrentValue = 0;
                }
        }
    }
}