using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string LevelsPath = "StaticData/Levels";
        private const string MonstersPath = "StaticData/Monsters";
        private const string WindowsPath = "StaticData/UI/WindowStaticData";

        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;

        public void LoadMonsters()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>(MonstersPath)
                .ToDictionary(x => x.MonsterTypeId, x => x);

            _levels = Resources
                .LoadAll<LevelStaticData>(LevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);

            _windowConfigs = Resources
                .Load<WindowsStaticData>(WindowsPath)
                .Configs
                .ToDictionary(x => x.WindowId, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId) =>
            _monsters.TryGetValue(typeId, out MonsterStaticData staticData)
                ? staticData
                : null;

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
                ? staticData
                : null;

        public WindowConfig ForWindow(WindowId windowId)=>
            _windowConfigs.TryGetValue(windowId, out WindowConfig staticData)
                ? staticData
                : null;
    }
}