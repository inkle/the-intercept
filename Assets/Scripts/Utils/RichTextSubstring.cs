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

	public string Substring(int length) {
		return string.Empty;
	}
}
