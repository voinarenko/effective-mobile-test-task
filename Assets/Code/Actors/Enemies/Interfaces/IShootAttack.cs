using Code.Infrastructure.Factory;

namespace Code.Actors.Enemies.Interfaces
{
  public interface IShootAttack : ISpecificAttack
  {
    void Construct(IGameFactory factory, float damage, float bulletSpeed, float shotDistance);
  }
}