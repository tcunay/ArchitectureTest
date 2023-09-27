using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public class UnityRandomService : IRandomService
    {
        public int Next(int lootMin, int lootMax)
        {
            return Random.Range(lootMin, lootMax);
        }
        
        public float Next(float lootMin, float lootMax)
        {
            return Random.Range(lootMin, lootMax);
        }
    }
}