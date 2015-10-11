# Rhoa
Path Of Exile item searcher utilizing poe.trade

It is recommended to run POE in Windowed Fullscreen mode.

# SETUP:

To use Rhoa, please install AutoHotKey. There is a sample script provided called "AutoHotkeyRhoa.ahk".

Open the script with any text editor and edit the part where it says

"WRITE_THE_PATH_TO_YOUR_RHOA.EXE_HERE"

Instead of that, write the path to the location of Rhoa.exe on your system.

You can change the hotkey that runs Rhoa by changing the top line in the script. The current line specifies that the key combination SHIFT+ALT+C will run rhoa.
Load the script with AutoHotKey.

Once in POE, hover over an item and press the key combination specified in the autohotkey script. A form will appear with the item mods loaded. Once you modified them to how you want to search, click the search button.

# SETTINGS:
Under League type the name of the league as written in poe.xyz.

Use addition or use multiplication determines whether the + and - buttons will change the respective value by multiplying it with the selected coefficient or adding/subtracting.

Initially all selected selects all the mods to be searched by on startup.

# Currently not supported item types:
Non unique Flasks
Non unique Maps
Scrolls
Currency

# POSSIBLE ISSUES
There have been reports of localization issues with the config file. To clarify, depending on your localization settings the parser may get confused with the decimal separator. In which case, an error will inform you of this. Try using whole numbers for now, or replacing the decimal point with ','.

Please report all issues either directly to me, or to the issues section of this site. If you find this tool useful and want to contribute to the developer, any donation is much appreciated.


