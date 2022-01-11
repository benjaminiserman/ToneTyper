namespace ToneTyper;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

internal class Converter
{
	[DllImport("User32.dll")]
	private static extern short GetAsyncKeyState(Keys ArrowKeys);

	private static readonly Keys[] _possibleKeys = Enum.GetValues<Keys>();

	private readonly InputSimulator _inputSimulator = new();

	private Key? _cached = null, _saved = null;

	public bool Paused { get; set; }

	public ManualResetEvent Handle { get; private set; } = new(false);

	public void Convert()
	{
		if (Paused) return;

		Key? received = GetKey();

		if (received is Key receivedKey)
		{
			if (receivedKey.IsBackspace && _saved is Key savedKey)
			{
				_cached = savedKey;
				_saved = null;
				return;
			}

			_saved = null;

			if ((receivedKey.IsStart || receivedKey.IsApostrophe) &&
				!(_cached is not null && receivedKey.IsU && (_cached.Value.IsApostrophe || _cached.Value.IsU) && Program.Config.VMode == VMode.DoubleU) &&
				!(_cached is not null && receivedKey.IsV && _cached.Value.IsApostrophe && Program.Config.VMode == VMode.SingleV))
			{
				if (receivedKey.IsV)
				{
					if (Program.Config.VMode == VMode.SingleV)
					{
						_cached = Key.Umlaut(receivedKey.Shift);

						_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_U);
						_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
						_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
						_inputSimulator.Keyboard.TextEntry((char)_cached);
					}

					return;
				}

				_cached = received;
			}
			else
			{
				if (_cached is Key cachedKey)
				{
					if (receivedKey.IsNumber || 
						(receivedKey.IsU && Program.Config.VMode == VMode.DoubleU) ||
						(receivedKey.IsV && Program.Config.VMode == VMode.SingleV))
					{
						_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
						_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BACK);

						if (cachedKey.IsApostrophe) // a'3 => a3
						{
							_inputSimulator.Keyboard.TextEntry((char)receivedKey);
						}
						else if (cachedKey.IsStart) // a3 => ǎ
						{
							if (receivedKey.IsU && cachedKey.IsU && Program.Config.VMode == VMode.DoubleU)
							{
								_cached = Key.Umlaut(cachedKey.Shift);

								_inputSimulator.Keyboard.TextEntry((char)_cached);
								_saved = cachedKey;
								return;
							}
							
							_inputSimulator.Keyboard.TextEntry(cachedKey + receivedKey);
							_saved = cachedKey;
						}
					}
				}

				_cached = null;
			}
		}
	}
	
	private static Key? GetKey()
	{
		bool shift = GetAsyncKeyState(Keys.ShiftKey) != 0;

		foreach (Keys key in _possibleKeys)
		{
			if (GetAsyncKeyState(key) == -32767) // -32767 == 0b10000000_00000001 => key is down and was pressed since last query
			{
				return new Key(key, shift);
			}
		}

		return null;
	}
}