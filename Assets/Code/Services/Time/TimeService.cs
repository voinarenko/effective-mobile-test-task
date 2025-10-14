namespace Code.Services.Time
{
  public class TimeService : ITimeService
  {
    public float DeltaTime() => 
      UnityEngine.Time.deltaTime;
  }
}