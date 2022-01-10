namespace ToneTyper;

internal readonly struct Key
{
	private readonly PossibleKey _keyReceived;
	private readonly bool _shift;

	private Key(PossibleKey key, bool shift)
	{
		_keyReceived = key;
		_shift = shift;
	}

	public Key(Keys key, bool shift)
	{
		_keyReceived = key switch
		{
			Keys.A => PossibleKey.A,
			Keys.E => PossibleKey.E,
			Keys.I => PossibleKey.I,
			Keys.O => PossibleKey.O,
			Keys.U => PossibleKey.U,
			Keys.V => PossibleKey.V,

			Keys.D1 => PossibleKey.One,
			Keys.D2 => PossibleKey.Two,
			Keys.D3 => PossibleKey.Three,
			Keys.D4 => PossibleKey.Four,
			Keys.NumPad1 => PossibleKey.One,
			Keys.NumPad2 => PossibleKey.Two,
			Keys.NumPad3 => PossibleKey.Three,
			Keys.NumPad4 => PossibleKey.Four,

			Keys.Oem7 => PossibleKey.Apostrophe,
			_ => PossibleKey.Other,
		};

		_shift = shift;
	}

	public bool IsStart => !IsNumber && _keyReceived is not PossibleKey.Other;

	public bool IsApostrophe => _keyReceived is PossibleKey.Apostrophe && !_shift;

	public bool IsNumber => !_shift && _keyReceived is PossibleKey.One or PossibleKey.Two or PossibleKey.Three or PossibleKey.Four;

	public bool IsU => _keyReceived is PossibleKey.U;

	public bool IsV => _keyReceived is PossibleKey.V;

	public bool Shift => _shift;

	private char ToChar()
	{
		char c = _keyReceived switch
		{
			PossibleKey.A => 'a',
			PossibleKey.E => 'e',
			PossibleKey.I => 'i',
			PossibleKey.O => 'o',
			PossibleKey.U => 'u',
			PossibleKey.V => 'v',
			PossibleKey.Umlaut => 'ü',
			_ => throw new InvalidOperationException("Cannot convert KeyReceived.X or KeyReceived.Other into a char.")
		};

		if (_shift) c = char.ToUpper(c);

		return c;
	}

	public char Tone => _keyReceived switch
	{
		PossibleKey.One => '̄',
		PossibleKey.Two => '́',
		PossibleKey.Three => '̌',
		PossibleKey.Four => '̀',
		_ => throw new Exception($"Key {_keyReceived} has no tone associated with it.")
	};

	private enum PossibleKey { A, E, I, O, U, V, Umlaut, One, Two, Three, Four, Apostrophe, Other }

	public static explicit operator char(Key key) => key.ToChar();

	public static string operator +(Key a, Key b) => $"{(char)a}{b.Tone}";

	public static Key Umlaut(bool shift) => new(PossibleKey.Umlaut, shift);
}