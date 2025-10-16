using Code.Actors.Hero;
using Code.Data;

namespace Code.Services.Progress
{
  public interface IProgressService : IService
  {
    PlayerProgress Progress { get; set; }
    void TrackHeroDeath(HeroDeath heroDeath);
  }
}