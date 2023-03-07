using mis.Core;
using UObject = UnityEngine.Object;

namespace mis.UI
{
    public sealed class UIService : IUIService, IStartableService
    {
        private readonly IConfigService _configService;
        private readonly IObjectService _objectService;
        private readonly IMessageService _messageService;
        private IUIServiceConfig _config;

        private IUICanvas _gameCanvas;
        private WindowController _windowController;
        private HUDController _hudController;

        public int StartPriority => 0;

        public UIService(
            IConfigService configService,
            IObjectService objectService,
            IMessageService messageService)
        {
            _configService = configService;
            _objectService = objectService;
            _messageService = messageService;
        }

        void IStartableService.OnServiceStart()
        {
            _messageService.Subscribe<SwitchLevelMessage>(OnLevelSwitch);

            _config = _configService.GetConfig<IUIServiceConfig>(GameIds.GLOBAL_UI_CFG);
            SpawnUIInputMode();
            SpawnCanvas();
            InitializeControllers();
        }

        private void SpawnUIInputMode()
        {
            var uiInputModule = UObject.Instantiate(_config.UIEventSystemPrefab);
            UObject.DontDestroyOnLoad(uiInputModule);
        }

        private void SpawnCanvas()
        {
            var canvasGO = UObject.Instantiate(_config.UICanvasPrefab);
            _gameCanvas = canvasGO.GetComponent<IUICanvas>();
            UObject.DontDestroyOnLoad(canvasGO);
        }

        private void InitializeControllers()
        {
            _windowController = new WindowController(_gameCanvas.WindowsContainer, _objectService, _messageService);
            _hudController = new HUDController(_gameCanvas.HudContainer, _objectService, _messageService);
        }

        public IHUD OpenHUD(GameId hudId)
        {
            return _hudController.Open(hudId);
        }

        public void CloseHUD(GameId hudId)
        {
            _hudController.Close(hudId);
        }

        public IWindow OpenWindow(GameId windowID)
        {
            return _windowController.Open(windowID);
        }

        public void CloseWindow(GameId windowID)
        {
            _windowController.Close(windowID);
        }

        private void OnLevelSwitch(SwitchLevelMessage message)
        {
            _windowController.CloseAll();
            _hudController.CloseAll();
        }
    }
}