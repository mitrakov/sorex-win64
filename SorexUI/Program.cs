namespace SorexUI;

// build:
// 1) raise version in SorexUI.csproj
// 2) clean the "bin" folder manually
// 3) run: dotnet publish --self-contained
// 4) compile installer.iss with InnoSetup
internal static class Program {
    [STAThread]
    internal static void Main() {
        ApplicationConfiguration.Initialize();
        Application.Run(new view.MainForm(new()));
    }
}
