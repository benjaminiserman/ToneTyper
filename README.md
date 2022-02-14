# ToneTyper
![Windows](https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white)
![.Net](https://img.shields.io/badge/.NET6-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Downloads](https://img.shields.io/github/downloads/winggar/ToneTyper/total?style=for-the-badge)

ToneTyper is a lightweight background service that converts numbered pinyin tones into their proper symbols. For example, "Zho1ngguo2" => "Zhōngguó".


To escape a character, type an apostrophe before the u or number. For example, typing "a'3" will result in "a3".

To pause or resume ToneTyper, click its icon on the system tray, or use the hotkey "Alt + P".

To change the toggle hotkey, right-click the ToneTyper icon on the system tray and choose "Change Hotkey".

To change the Ü mode, right-click the ToneTyper icon on the system tray and choose .

To exit ToneTyper, right-click its icon on the system tray and choose "Exit".

## Prerequisites

Before you begin, ensure you have met the following requirements:
- [You use a machine supported by .NET 6](https://github.com/dotnet/core/blob/main/release-notes/6.0/supported-os.md)
- You have .NET 6 installed
- You have downloaded the file "build.zip" from the latest release

OR

- [You use a **Windows** machine supported by .NET 6](https://github.com/dotnet/core/blob/main/release-notes/6.0/supported-os.md)
- You **do not** need to have .NET 6 installed
- You have downloaded the file "standalone.zip" from the latest release

Due to low demand, standalone builds for Mac OSX and Linux are not provided. If you'd like a standalone build for Mac OSX or Linux, [contact me](mailto:winggar1228@gmail.com).

## Usage

1. Download either "build.zip" or "standalone.zip" from the latest release, depending on your prerequisites.
2. Unzip the file.
3. Find the file "ToneTyper.exe" within the unzipped folder and run it.
4. To add a tone mark to a vowel, type the number of the tone you'd like after typing the vowel.
5. By default, the character 'ü' can be accessed by typing "uu". This behavior can be changed instead so that 'v' is converted into 'ü' by right-clicking ToneTyper on the system tray and choosing "Toggle Ü Mode".
6. To pause or resume ToneTyper, click its icon on the system tray or use the hotkey (default is Alt + P).
7. To exit ToneTyper, right-click its icon on the system tray and choose "Exit".


## Contribution
To contribute to ToneTyper, follow these steps:

1. Fork this repository.
2. Create a branch: `git checkout -b <branch_name>`.
3. Make your changes and commit them: `git commit -m '<commit_message>'`
4. Push to the original branch: `git push origin <project_name>/<location>`
5. Create the pull request.

Alternatively see the GitHub documentation on [creating a pull request](https://help.github.com/en/github/collaborating-with-issues-and-pull-requests/creating-a-pull-request).

## License

![License](https://img.shields.io/github/license/winggar/ToneTyper?style=for-the-badge)
