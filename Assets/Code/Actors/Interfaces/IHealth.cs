namespace Code.Actors.Interfaces
{
  public interface IHealth
  {
    float Current { get; set; }
    float Max { get; set; }
    void TakeDamage(float damage);
  }
}