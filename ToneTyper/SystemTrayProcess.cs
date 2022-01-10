namespace ToneTyper;
using System;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

internal class SystemTrayProcess : ApplicationContext
{
	private static readonly Icon _normal = new("tonetyper.ico"), _suspended = new("tonetyper_suspend.ico");

	private readonly NotifyIcon _trayIcon;
	private readonly ToolStripMenuItem _toggleButton, _changeHotkeyButton, _exitButton, _vModeButton;

	private readonly Converter _converter;
	private readonly Timer _timer;

	private const string PAUSE_MESSAGE = "Pause", RESUME_MESSAGE = "Resume", CHANGE_HOTKEY_MESSAGE = "Change Hotkey", EXIT_MESSAGE = "Exit";

	private const string TOGGLE_MODE_V = "Toggle Ü Mode (Current: V)", TOGGLE_MODE_UU = "Toggle Ü Mode (Current: UU)";

	internal SystemTrayProcess()
	{
		ContextMenuStrip menuStrip = new();

		_toggleButton = new()
		{
			Name = PAUSE_MESSAGE,
			Text = PAUSE_MESSAGE,
		};

		_changeHotkeyButton = new()
		{
			Name = CHANGE_HOTKEY_MESSAGE,
			Text = CHANGE_HOTKEY_MESSAGE,
		};

		_vModeButton = new()
		{
			Name = "VModeButton",
			Text = GetVModeString,
		};

		_exitButton = new()
		{
			Name = EXIT_MESSAGE,
			Text = EXIT_MESSAGE,
		};

		_toggleButton.Click += new EventHandler(ToggleLoop);
		_changeHotkeyButton.Click += new EventHandler(ChangeHotkey);

		_exitButton.Click += new EventHandler(Exit);

		menuStrip.Items.Add(_toggleButton);
		menuStrip.Items.Add(_changeHotkeyButton);
		menuStrip.Items.Add(_exitButton);

		_trayIcon = new()
		{
			Visible = true,
			Icon = _normal,
			Text = "ToneTyper",
			ContextMenuStrip = menuStrip,
		};

		_trayIcon.MouseClick += TrayRightClick;

		_converter = new();

		HotkeyManager.RegisterHotkey(ToggleLoop);

		_timer = new() { Interval = 10 };
		_timer.Elapsed += (object _, ElapsedEventArgs _) => _converter.Convert();
		_timer.Start();
	}

	private void TrayRightClick(object sender, MouseEventArgs e)
	{
		switch (e.Button)
		{
			case MouseButtons.Left:
			{
				ToggleLoop(null, null);
				break;
			}
			case MouseButtons.Right:
			{
				_trayIcon.ContextMenuStrip.Show(Cursor.Position);
				break;
			}
		}
	}

	private void ToggleLoop(object sender, EventArgs e)
	{
		bool wasPaused = _toggleButton.Name == RESUME_MESSAGE;

		_toggleButton.Name = wasPaused ? PAUSE_MESSAGE : RESUME_MESSAGE;
		_toggleButton.Text = _toggleButton.Name;

		_trayIcon.Icon = wasPaused ? _normal : _suspended;

		_converter.Paused = !wasPaused;
	}

	private void ChangeHotkey(object sender, EventArgs e)
	{
		EnterHotkeyForm form = new();

		HotkeyManager.EnableSelector(form.GetHotkeyTextbox());

		form.Show();
	}

	private void ToggleVMode(object sender, EventArgs e)
	{
		Program.Config.VMode = Program.Config.VMode switch
		{
			VMode.SingleV => VMode.DoubleU,
			VMode.DoubleU => VMode.SingleV,
			_ => throw new NotImplementedException()
		};

		_vModeButton.Text = GetVModeString; 

		Program.SaveConfig();
	}

	internal void Exit(object sender, EventArgs e)
	{
		_trayIcon.Visible = false;
		_timer.Stop();

		Application.Exit();
	}

	private static string GetVModeString => Program.Config.VMode switch
	{
		VMode.SingleV => TOGGLE_MODE_V,
		VMode.DoubleU => TOGGLE_MODE_UU,
		_ => throw new NotImplementedException()
	};
}