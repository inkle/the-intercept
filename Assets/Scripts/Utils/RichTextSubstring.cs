public class RichTextSubstring
{
	public readonly string richText;
	public readonly string plainText;

	public RichTextSubstring (string richText) {
		this.richText = richText;
		this.plainText = string.Empty;

		bool isInTag = false;

		for (int i = 0; i < richText.Length; i++) {
			char c = richText [i];
			if (isInTag) {
				if (c == '>') {
					isInTag = false;
				}
			} else {
				if (c == '<') {
					isInTag = true;
				} else {
					this.plainText += c;
				}
			}
		}
	}

	public string Substring(int startIndex, int length) {
		if (startIndex != 0) {
			// We might add support for this case later, but for now, throw an error.
			throw new System.InvalidOperationException ("startIndex must be 0 for now, sorry!");
		}
		return string.Empty;
	}
}
