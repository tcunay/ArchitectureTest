using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class ShopWindow : WindowBase
    {
        [SerializeField] private TMP_Text _skullCounter;
        private LootData LootData => Progress.WorldData.LootData;

        protected override void Initialize()
        {
            base.Initialize();
            UpdateLootCounter();
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();

            LootData.Changed += UpdateLootCounter;
        }

        protected override void CleanUp()
        {
            base.CleanUp();

            LootData.Changed -= UpdateLootCounter;
        }

        private void UpdateLootCounter()
        {
            _skullCounter.text = LootData.Collected.ToString();
        }
    }
}