namespace Code.Actors.Enemies.Interfaces
{
  public interface IMeleeAttack : ISpecificAttack
  {
    void Construct(float cleavage, float damage);
  }
}