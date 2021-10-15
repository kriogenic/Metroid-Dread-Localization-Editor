# Metroid Dread Text\Localization Editor

### Preperation
You will need to do some setup to extract the localization files from Metroid Dread
- Download the Yuzu Switch Emulator from [https://yuzu-emu.org/](https://yuzu-emu.org/)
- Obtain a copy of Metroid Dread in either NSP or XCI - you can google how to dump your own copy
- Open Yuzu and add Metroid Dread as a game, Right click it and select "Dump RomFS"
You now have the required file dumped for editing

### Using the editor
- Download the latest release and extract it
- Open DreadLocaleEditor.exe
- Select File -> Open and then navigate to **%appdata%\yuzu\dump\010093801237C000\romfs\system\localization**
- Select which file you would like to edit
- Make your changes
- Select File -> Save and save it somewhere\overwrite it.

### Loading your changes in Yuzu or any OS\CFW that has LayeredFS Support.
I will be using Yuzu for this but the steps are very similar for others
- Create a directory called **load** in **%appdata%\yuzu** directory
- Then in the **load** directory create another with Metroid Dreads title ID **010093801237C000**
- Inside that directory create a folder and give it a name (this is how Yuzu can enable \ disable different mods so give it a basic but descriptive name like *TextEdits*)
- Then recreate the directory\file structure including the romfs directory
- You should end up with your edited file in this directory **%appdata%\yuzu\load\010093801237C000\TextEdits\romfs\system\localization\us_engligh.txt**

Now when you load Yuzu, right click Metroid Dread and select *Properties* you should see an Add-Ons tab which shows TextEdits (or what ever you named it) show up in that list, if it does you can load Metroid Dread and see if your edits have taken effect.
