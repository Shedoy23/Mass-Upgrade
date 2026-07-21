# Mass Upgrade

A tiny, focused Mount & Blade II: Bannerlord mod that adds **Ctrl + Click** to
the party-screen "Upgrade Troops" button. One click = every upgradeable troop
in your main party gets upgraded along the path you prefer.

No save data is touched. No screen overlay is added. The mod patches a single
vanilla method.

## Requirements

- Mount & Blade II: Bannerlord **v1.3.15** (locked to this version).
- [Bannerlord.Harmony](https://www.nexusmods.com/mountandblade2bannerlord/mods/2006)
- [Bannerlord.ButterLib](https://www.nexusmods.com/mountandblade2bannerlord/mods/2018)
- [Bannerlord.MBOptionScreen](https://www.nexusmods.com/mountandblade2bannerlord/mods/612) (MCM)

## Usage

1. Enable **Mass Upgrade** in the launcher.
2. Open the party screen (P on the world map).
3. Hold **Ctrl** and click the upgrade-troops button (the round badge in the
   top-right of your party panel showing the number of upgradeable troops).
4. A green message reports how many troops were upgraded. Click **Done** to
   apply.

Without Ctrl, the vanilla upgrade popup opens as usual — Mass Upgrade does
not change default behaviour.

## Configuration

In-game: **Options → Mod Settings → Mass Upgrade**.

When a recruit can upgrade into multiple troop types (e.g. Imperial Recruit
→ Infantryman or Archer), Mass Upgrade picks the path whose target has the
highest priority you've assigned to its formation class.

| Setting        | Default | Notes                              |
|----------------|---------|------------------------------------|
| Ranged         | 80      | foot archers / crossbows           |
| Horse Archer   | 70      | mounted ranged                     |
| Heavy Cavalry  | 60      | knights                            |
| Cavalry        | 50      | generic mounted melee              |
| Light Cavalry  | 40      |                                    |
| Skirmisher     | 30      | foot javelin throwers              |
| Heavy Infantry | 20      |                                    |
| Infantry       | 10      | generic foot melee                 |

Higher number = stronger preference. Ties fall through to vanilla order.

## What it doesn't do

- No prisoner-recruit batch (vanilla popup is fine for that).
- No troop-sorting, no UI overlay, no equipment management.
- Doesn't write anything to your save file.

## License

MIT — do whatever you want.

## Issues / contributions

Drop a comment on the Steam Workshop page.
