using Code.Actors.Hero;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Services.Progress;
using Code.Services.StaticData;
using Code.UI.Elements.Buttons;
using UnityEngine;

namespace Code.UI.Windows
{
  public class PauseWindow : BaseWindow
  {
    private const string LevelSceneName = "Menu";

    private ISceneLoader _sceneLoader;
    private GameObject _hero;
    private HeroMove _heroMove;
    private HeroLook _heroLook;
    private HeroShoot _heroShoot;
    [SerializeField] private ConfirmButton _confirmButton;
    [SerializeField] private RestartButton _restartButton;
    [SerializeField] private MenuReturnButton _returnButton;

    public void Construct(IProgressService progress, IGameStateMachine stateMachine, IStaticDataService staticData, Transform heroTransform)
    {
      _hero = heroTransform.gameObject;
    }
    
    protected override void Start()
    {
      base.Start();
      Time.timeScale = 0;
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      if (_heroMove) _heroMove.enabled = true;
      if (_heroLook) _heroLook.enabled = true;
      if (_heroShoot) _heroShoot.enabled = true;
      _restartButton.Clicked -= ProcessRestartClick;
      _returnButton.Clicked -= ProcessReturnClick;
      Cursor.visible = false;
      Time.timeScale = 1;
    }

    public override void Init()
    {
      _heroMove = _hero.GetComponent<HeroMove>();
      _heroLook = _hero.GetComponent<HeroLook>();
      _heroShoot = _hero.GetComponent<HeroShoot>();
      _heroMove.enabled = false;
      _heroLook.enabled = false;
      _heroShoot.enabled = false;
    }

    protected override void Initialize()
    {
      _restartButton.Clicked += ProcessRestartClick;
      _returnButton.Clicked += ProcessReturnClick;
    }

    private void ProcessReturnClick()
    {
      Time.timeScale = 1;
      _returnButton.Clicked -= ProcessReturnClick;
      // StateMachine.Enter<MenuLoadState>();
      Destroy(gameObject);
    }

    private void ProcessRestartClick()
    {
      Time.timeScale = 1;
      _restartButton.Clicked -= ProcessRestartClick;
      // StateMachine.Enter<LevelLoadState, string>(LevelSceneName);
    }
  }
}