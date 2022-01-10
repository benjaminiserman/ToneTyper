namespace ToneTyper;
using WK.Libraries.HotkeyListenerNS;

internal record Config
{
	public Hotkey ToggleHotkey { get; set; } = new(Keys.Alt, Keys.P);

	public VMode VMode { get; set; } = VMode.DoubleU;
}