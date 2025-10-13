using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Code.Services.Async
{
  public interface IAsyncService
  {
    UniTask NextFrame(CancellationTokenSource cts = null);
    UniTask WaitForSeconds(float duration, CancellationTokenSource cts = null);
    UniTask WaitWithPause(float durationSeconds, Func<bool> isPaused);
  }
}