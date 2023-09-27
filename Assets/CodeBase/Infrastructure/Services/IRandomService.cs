namespace CodeBase.Infrastructure.Services
{
    public interface IRandomService : IService
    {
        int Next(int lootMin, int lootMax);
        float Next(float lootMin, float lootMax);
    }
}