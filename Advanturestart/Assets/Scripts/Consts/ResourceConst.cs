using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceConst 
{
    public class UI
    {
        public const string logView= "LogView";
        public const string MessageItemPoolVIew = "MessageItemPoolView";
        public const string LevelOneView = "LevelOneView";

        public const string loginView = "LoginView";
        public const string registerView = "RegisterView";
        public const string resetPasView = "ResetPasView";
        public const string personInfoView = "PersonInfoView";
        public const string journeyView = "JourneyView";
        public const string trackView = "TrackView";
        public const string mainView = "MainView";
        public const string settingView = "SettingView";
    }
    public class JSONCONFIG
    {
        public const string JSON_ROOT = "Config/Table/";
        public const string UIConfig = JSON_ROOT + "UIConfig.json.txt";
        public const string TempCfg = JSON_ROOT + "111.json.txt";
        public const string ServerConfig = JSON_ROOT + "ServerConfig.json.txt";
    }
}
