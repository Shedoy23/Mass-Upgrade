# Changelog

## v1.0.2 (2026-10-19)

**Updated for Bannerlord v1.4.8** and **translated** into Russian, Korean and
Simplified Chinese.

- Rebuilt against the v1.4.8 game libraries (no compile errors; the patched
  method `PartyVM.ExecuteOpenUpgradePopUp` and every API the mod calls still
  exist). The launcher no longer flags the mod as made for v1.3.15.
- Translations: troop-type names are taken from the game's own official
  translations, so they read the same as the rest of the UI.
- No gameplay changes.

## v1.0.1 (2026-07-21)

**Fixed: settings were never saved.**

Reported by Trickers555 (Workshop, 18 July): changing a setting showed the
"restart to take effect" prompt, but after a restart every value was back to
default.

Cause: the settings class did not declare `FormatType`. That property tells MCM
which serializer to use when writing the file to disk — without it no format is
selected, nothing is ever written, and the values only live in memory for the
current session. Confirmed by comparing against mods whose settings do persist
(BanditBlackHole, BetterTime — both declare it), and by the fact that
`Configs\ModSettings\Global\MassUpgrade\` was never created on any machine.

Also changed the settings `Id` from `MassUpgrade.Settings` to `MassUpgrade_MCM`:
the id becomes the settings file name, and a dot in it reads as a file
extension. Safe to change because no saved settings existed anywhere.

No gameplay changes. Upgrade logic, priority table and defaults are byte-for-byte
identical to v1.0.0 (verified by decompiling both builds and diffing).

## v1.0.0

Initial release. Ctrl + click the upgrade button in the party screen to upgrade
every eligible troop at once, choosing the branch with the highest configured
priority.
