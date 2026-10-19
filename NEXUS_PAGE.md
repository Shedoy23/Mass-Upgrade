# Публикация на Nexus Mods — шпаргалка

Архив: `dist/MassUpgrade-v1.0.2.zip` (пересобрать после правок — см. «Выпуск
новой версии» в конце).

## Поля формы на nexusmods.com (Upload a mod)

| Поле | Значение |
|---|---|
| Game | Mount & Blade II: Bannerlord |
| Mod name | Mass Upgrade |
| Category | User Interface (если нет — Utilities) |
| Version | 1.0.2 |
| Language | English (переводы перечислены в описании) |
| Brief overview | Ctrl + Click the party-screen upgrade button to upgrade every troop at once, following the troop types you prefer. |
| Requirements (Nexus mods) | Harmony (2006), ButterLib (2018), Mod Configuration Menu v5 (612) |
| Permissions | Open permissions — MIT license (у мода MIT, см. LICENSE.txt) |
| File category | Main file |
| Images | `preview.png` / `preview.jpg` + скриншот экрана отряда с зелёным сообщением |

## Description (вставить как есть — это разметка BBCode для Nexus)

```
[size=5][b]Mass Upgrade[/b][/size]

A tiny, focused mod: hold [b]Ctrl[/b] and click the [b]Upgrade Troops[/b] button on the party screen — every troop that can be upgraded gets upgraded in one click.

When a recruit has more than one upgrade path, the mod picks the one you prefer, based on a priority you set per troop type in the mod settings.

[size=4][b]How to use[/b][/size]
[list=1]
[*]Open the party screen ([b]P[/b] on the world map).
[*]Hold [b]Ctrl[/b] and click the upgrade-troops button (the round badge at the top right of your party panel).
[*]A green message shows how many troops were upgraded. Click [b]Done[/b] to apply.
[/list]
Without Ctrl the normal upgrade window opens as usual — the mod doesn't change default behaviour.

[size=4][b]Settings[/b][/size]
[i]Options → Mod Settings → Mass Upgrade[/i]. Higher number = preferred path.
[list]
[*]Ranged — 80
[*]Horse Archer — 70
[*]Heavy Cavalry — 60
[*]Cavalry — 50
[*]Light Cavalry — 40
[*]Skirmisher — 30
[*]Heavy Infantry — 20
[*]Infantry — 10
[/list]

[size=4][b]Requirements[/b][/size]
[list]
[*]Bannerlord [b]v1.4.8[/b]
[*][url=https://www.nexusmods.com/mountandblade2bannerlord/mods/2006]Harmony[/url]
[*][url=https://www.nexusmods.com/mountandblade2bannerlord/mods/2018]ButterLib[/url]
[*][url=https://www.nexusmods.com/mountandblade2bannerlord/mods/612]Mod Configuration Menu v5[/url]
[/list]
Load order: Harmony → ButterLib → MCM → native modules → Mass Upgrade.

[size=4][b]Installation[/b][/size]
Vortex: click [i]Mod Manager Download[/i].
Manual: extract the archive into [i]Mount & Blade II Bannerlord\Modules\[/i] so you get [i]Modules\Shedoy23.MassUpgrade\SubModule.xml[/i], then enable it in the launcher.

[size=4][b]Safe to add or remove[/b][/size]
The mod writes nothing to your save. You can add or remove it mid-campaign.

[size=4][b]Languages[/b][/size]
English, Russian, Korean, Simplified Chinese. Troop-type names use the game's official translations. Corrections are welcome in the comments.

[size=4][b]Also on Steam Workshop[/b][/size]
[url=https://steamcommunity.com/sharedfiles/filedetails/?id=3724776428]Mass Upgrade on Steam Workshop[/url] — same mod, same version.

[size=4][b]License[/b][/size]
MIT.
```

## Выпуск новой версии — ДВА места, не одно

Nexus сам у игроков не обновляется, и про него легко забыть: тогда там
остаётся старая версия, а жалобы приходят на уже исправленное.

1. Поднять `Version` в `SubModule.xml`, записать изменения в `CHANGELOG.md`
   и `ChangeNotes` в `workshop_update.xml`.
2. `dotnet build src/MassUpgrade.csproj -c Release`, скопировать в папку
   игры, проверить в игре.
3. Workshop: публикация из папки игры через `workshop_update.xml`.
4. Nexus: пересобрать архив (тот же состав, что в `dist/`), на странице мода
   **Manage → Files → Add file**, старый файл перевести в «Old versions».
