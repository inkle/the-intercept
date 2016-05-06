public class RichTextSubstring
{
	public readonly string richText;
	public readonly string plainText;

	private bool[] plainTextItalicState;

	private const string OPEN_ITALIC_TAG = "<i>";
	private const string CLOSE_ITALIC_TAG = "</i>";

	public RichTextSubstring (string richText) {
		this.richText = richText;
		plainTextItalicState = new bool[richText.Length];
		plainText = string.Empty;

		bool isItalic = false;

		for (int i = 0; i < richText.Length;) {
			if (!isItalic && richText.Substring(i).StartsWith(OPEN_ITALIC_TAG)) {
				isItalic = true;
				i += OPEN_ITALIC_TAG.Length;
			} else if (isItalic && richText.Substring(i).StartsWith(CLOSE_ITALIC_TAG)) {
				isItalic = false;
				i += CLOSE_ITALIC_TAG.Length;
			} else {
				plainTextItalicState [plainText.Length] = isItalic;
				plainText += richText[i];
				i++;
			}
		}
	}

	public string Substring(int startIndex, int length) {
		string result = string.Empty;
		bool isItalic = false;

		for (int i = startIndex; i < startIndex + length; i++) {
			if (isItalic && !plainTextItalicState [i]) {
				isItalic = false;
				result += CLOSE_ITALIC_TAG;
			} else if (!isItalic && plainTextItalicState [i]) {
				isItalic = true;
				result += OPEN_ITALIC_TAG;
			}
			result += plainText [i];
		}

		if (isItalic) {
			result += CLOSE_ITALIC_TAG;
		}

		return result;
	}
}
