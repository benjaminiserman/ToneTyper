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

	private Key? _cached = null;

	public bool Paused { get; set; }

	public ManualResetEvent Handle { get; private set; } = new(false);

	public void Convert()
	{
		if (Paused) return;

		Key? received = GetKey();

		if (received is Key receivedKey)
		{
			if ((receivedKey.IsStart || receivedKey.IsApostrophe) && (_cached is not Key possibleU || !possibleU.IsU || !receivedKey.IsU || Program.Config.VMode != VMode.DoubleU))
			{
				if (receivedKey.IsV)
				{
					if (Program.Config.VMode == VMode.SingleV)
					{
						_cached = Key.Umlaut(receivedKey.Shift);

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
					if (receivedKey.IsNumber || (receivedKey.IsU && Program.Config.VMode == VMode.DoubleU))
					{
						_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
						_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BACK);

						if (cachedKey.IsApostrophe) // a'3 => a3
						{
							_inputSimulator.Keyboard.TextEntry((char)cachedKey);
						}
						else if (cachedKey.IsStart) // a3 => ǎ
						{
							if (receivedKey.IsU && cachedKey.IsU && Program.Config.VMode == VMode.DoubleU)
							{
								_cached = Key.Umlaut(receivedKey.Shift);

								_inputSimulator.Keyboard.TextEntry((char)_cached);
								return;
							}

							_inputSimulator.Keyboard.TextEntry(cachedKey + receivedKey);
						}
					}
				}

				_cached = null;
			}
		}
	}

	private static Key? GetKey()
	{
		bool shift = GetAsyncKeyState(Keys.Shift) != 0;

		if (shift)
		{
			throw new Exception("shift detected");
		}

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