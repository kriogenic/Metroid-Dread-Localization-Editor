# Metroid Dread Text\Localization Editor

### Using the editor
- Download the latest release and extract it
- Open DreadLocaleEditor.exe
- Select File -> Open and then navigate to your extracted localization .txt file
- Select which file you would like to edit
- Make your changes
- Select File -> Save and save it somewhere\overwrite it.

### Loading your changes in OS\CFW that has LayeredFS Support.
I will be using Ryujinx for this but the steps are very similar for others
- In Ryujinx right click on dread and view mod folder
- Then in the **contents** directory create another with Metroid Dreads title ID **010093801237C000**
- Inside that directory create a folder and give it a name like **TextEdits**
- Then recreate the directory\file structure including the romfs directory
- You should end up with your edited file in this directory **mods\contents\010093801237C000\TextEdits\romfs\system\localization\us_engligh.txt**
