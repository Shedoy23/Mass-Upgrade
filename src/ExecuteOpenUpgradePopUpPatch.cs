using System;
using System.Collections.Generic;
using HarmonyLib;
using MCM.Abstractions.Base.Global;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Party;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace MassUpgrade
{
    /// <summary>
    /// Ctrl + клик по кнопке апгрейда в экране отряда = повысить ВСЕХ, кого можно,
    /// разом. Без Ctrl вызывается ванильное окно — мы не мешаем обычному клику.
    ///
    /// Когда у рекрута несколько веток повышения, выбирается та, у которой выше
    /// приоритет в настройках MCM (см. Settings).
    /// </summary>
    [HarmonyPatch(typeof(PartyVM))]
    public class ExecuteOpenUpgradePopUpPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("ExecuteOpenUpgradePopUp")]
        public static bool Prefix(PartyVM __instance)
        {
            try
            {
                if (!IsCtrlDown())
                    return true;   // обычный клик → ванильное поведение

                int upgraded = UpgradeAll(__instance);
                var msg = new TextObject("{=MassUpgrade_Success}Mass-upgraded {COUNT} troops.");
                msg.SetTextVariable("COUNT", upgraded);
                InformationManager.DisplayMessage(
                    new InformationMessage(msg.ToString(), Color.ConvertStringToColor("#80ff80FF")));
                return false;
            }
            catch (Exception ex)
            {
                var msg = new TextObject("{=MassUpgrade_Error}MassUpgrade error: {MESSAGE}");
                msg.SetTextVariable("MESSAGE", ex.Message);
                InformationManager.DisplayMessage(
                    new InformationMessage(msg.ToString(), Color.ConvertStringToColor("#ff8080FF")));
                return true;   // не смогли — отдаём ход ванили, чтобы игрок не остался без окна
            }
        }

        /// <summary>Ctrl зажат? Пробуем три разных API ввода: в разных версиях
        /// движка работает не всё, поэтому идём по очереди до первого успеха.</summary>
        private static bool IsCtrlDown()
        {
            try
            {
                if (Input.IsDown(InputKey.LeftControl) || Input.IsDown(InputKey.RightControl))
                    return true;
            }
            catch { }
            try
            {
                if (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl))
                    return true;
            }
            catch { }
            try
            {
                if (Input.IsKeyDownImmediate(InputKey.LeftControl)
                    || Input.IsKeyDownImmediate(InputKey.RightControl))
                    return true;
            }
            catch { }
            return false;
        }

        private static int UpgradeAll(PartyVM partyVM)
        {
            if (partyVM?.MainPartyTroops == null)
                return 0;

            // Копируем список: ExecuteUpgrade меняет коллекцию по ходу дела.
            var troops = new List<PartyCharacterVM>();
            foreach (var t in partyVM.MainPartyTroops)
                if (t != null) troops.Add(t);

            // Модификатор «весь стек» — чтобы повышать пачкой, а не по одному.
            bool prevStackModifier = partyVM.IsEntireStackModifierActive;
            int total = 0;
            try
            {
                partyVM.IsEntireStackModifierActive = true;
                foreach (var t in troops)
                    total += UpgradeOne(partyVM, t);
                return total;
            }
            finally
            {
                partyVM.IsEntireStackModifierActive = prevStackModifier;
            }
        }

        private static int UpgradeOne(PartyVM partyVM, PartyCharacterVM character)
        {
            if (character?.Character == null) return 0;
            if (character.Upgrades == null || character.Upgrades.Count == 0) return 0;

            try { character.InitializeUpgrades(); }
            catch { return 0; }

            int bestIndex = -1;
            int bestCount = 0;
            int bestScore = int.MinValue;

            for (int i = 0; i < character.Upgrades.Count; i++)
            {
                var upgrade = character.Upgrades[i];
                if (upgrade == null || !upgrade.IsAvailable
                    || upgrade.IsInsufficient || upgrade.AvailableUpgrades <= 0)
                    continue;

                int score = ScoreUpgradeTarget(character, i);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestIndex = i;
                    bestCount = upgrade.AvailableUpgrades;
                }
            }

            if (bestIndex < 0 || bestCount <= 0) return 0;

            try
            {
                partyVM.ExecuteUpgrade(character, bestIndex, bestCount);
                return bestCount;
            }
            catch { return 0; }
        }

        /// <summary>Приоритет ветки повышения из настроек MCM. Если настройки почему-то
        /// недоступны — 0 у всех, тогда берётся первая подходящая ветка.</summary>
        private static int ScoreUpgradeTarget(PartyCharacterVM character, int upgradeIndex)
        {
            try
            {
                var troop = character.Character;
                if (troop == null) return 0;

                var targets = troop.UpgradeTargets;
                if (targets == null || upgradeIndex >= targets.Length) return 0;

                var target = targets[upgradeIndex];
                if (target == null) return 0;

                var settings = GlobalSettings<Settings>.Instance;
                return settings?.ScoreOf(target.DefaultFormationClass) ?? 0;
            }
            catch { return 0; }
        }
    }
}
