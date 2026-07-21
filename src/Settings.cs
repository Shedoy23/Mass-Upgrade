using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace MassUpgrade
{
    /// <summary>
    /// Настройки мода в меню MCM: приоритет линеек апгрейда.
    /// Чем больше число — тем охотнее рекрут пойдёт по этой ветке, когда у него
    /// несколько вариантов повышения.
    /// </summary>
    public sealed class Settings : AttributeGlobalSettings<Settings>
    {
        // 2026-07-21 — ВАЖНО: Id БЕЗ ТОЧЕК. Он становится именем файла настроек;
        // точка внутри читается как расширение и путь получается непредсказуемым.
        // Старый вариант был "MassUpgrade.Settings" — менять безопасно, потому что
        // сохранений всё равно не существовало (см. FormatType ниже).
        public override string Id => "MassUpgrade_MCM";

        public override string DisplayName =>
            new TextObject("{=MassUpgrade_DisplayName}Mass Upgrade").ToString();

        public override string FolderName => "MassUpgrade";

        // 2026-07-21 — ПОЧИНКА БАГА (жалоба Trickers555 в Workshop, 18 июля):
        // «меняю настройку → просит перезапуск → после перезапуска снова дефолт».
        //
        // Причина: этой строки не было. FormatType говорит MCM, ЧЕМ сериализовать
        // настройки на диск. Без него формат не выбран, писать нечем — значения
        // живут только в памяти запущенной игры и исчезают при выходе.
        // Проверено сравнением с модами, где сохранение работает (BanditBlackHole,
        // BetterTime — у обоих FormatType задан), и по факту: папка
        // Configs\ModSettings\Global\MassUpgrade не создавалась НИ РАЗУ.
        public override string FormatType => "json2";

        [SettingPropertyGroup("{=MassUpgrade_GroupPriority}Priority", GroupOrder = 0)]
        [SettingPropertyInteger("{=MassUpgrade_PriorityRanged}Ranged", 0, 100, "0",
            HintText = "{=MassUpgrade_HintRanged}Foot archers and crossbows. Higher number = preferred when a recruit has multiple upgrade paths.")]
        public int PriorityRanged { get; set; } = 80;

        [SettingPropertyGroup("{=MassUpgrade_GroupPriority}Priority", GroupOrder = 0)]
        [SettingPropertyInteger("{=MassUpgrade_PriorityHorseArcher}Horse Archer", 0, 100, "0",
            HintText = "{=MassUpgrade_HintHorseArcher}Mounted ranged units.")]
        public int PriorityHorseArcher { get; set; } = 70;

        [SettingPropertyGroup("{=MassUpgrade_GroupPriority}Priority", GroupOrder = 0)]
        [SettingPropertyInteger("{=MassUpgrade_PriorityHeavyCavalry}Heavy Cavalry", 0, 100, "0",
            HintText = "{=MassUpgrade_HintHeavyCavalry}Armoured mounted melee.")]
        public int PriorityHeavyCavalry { get; set; } = 60;

        [SettingPropertyGroup("{=MassUpgrade_GroupPriority}Priority", GroupOrder = 0)]
        [SettingPropertyInteger("{=MassUpgrade_PriorityCavalry}Cavalry", 0, 100, "0",
            HintText = "{=MassUpgrade_HintCavalry}Generic mounted melee.")]
        public int PriorityCavalry { get; set; } = 50;

        [SettingPropertyGroup("{=MassUpgrade_GroupPriority}Priority", GroupOrder = 0)]
        [SettingPropertyInteger("{=MassUpgrade_PriorityLightCavalry}Light Cavalry", 0, 100, "0")]
        public int PriorityLightCavalry { get; set; } = 40;

        [SettingPropertyGroup("{=MassUpgrade_GroupPriority}Priority", GroupOrder = 0)]
        [SettingPropertyInteger("{=MassUpgrade_PrioritySkirmisher}Skirmisher", 0, 100, "0",
            HintText = "{=MassUpgrade_HintSkirmisher}Foot units that throw javelins.")]
        public int PrioritySkirmisher { get; set; } = 30;

        [SettingPropertyGroup("{=MassUpgrade_GroupPriority}Priority", GroupOrder = 0)]
        [SettingPropertyInteger("{=MassUpgrade_PriorityHeavyInfantry}Heavy Infantry", 0, 100, "0")]
        public int PriorityHeavyInfantry { get; set; } = 20;

        [SettingPropertyGroup("{=MassUpgrade_GroupPriority}Priority", GroupOrder = 0)]
        [SettingPropertyInteger("{=MassUpgrade_PriorityInfantry}Infantry", 0, 100, "0",
            HintText = "{=MassUpgrade_HintInfantry}Generic foot melee.")]
        public int PriorityInfantry { get; set; } = 10;

        public int ScoreOf(FormationClass cls)
        {
            switch (cls)
            {
                case FormationClass.Ranged:        return PriorityRanged;
                case FormationClass.HorseArcher:   return PriorityHorseArcher;
                case FormationClass.HeavyCavalry:  return PriorityHeavyCavalry;
                case FormationClass.Cavalry:       return PriorityCavalry;
                case FormationClass.LightCavalry:  return PriorityLightCavalry;
                case FormationClass.Skirmisher:    return PrioritySkirmisher;
                case FormationClass.HeavyInfantry: return PriorityHeavyInfantry;
                case FormationClass.Infantry:      return PriorityInfantry;
                default:                           return 0;
            }
        }
    }
}
