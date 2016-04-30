using NUnit.Framework;

public class RichTextSubstringTests {
	[Test]
	public void ItSetsPlainText () {
		RichTextSubstring rts = new RichTextSubstring("<i>foo</i> bar");
		Assert.AreEqual ("foo bar", rts.plainText);
	}

	[Test]
	public void SubstringReturnsWellFormattedRichText() {
		RichTextSubstring rts = new RichTextSubstring("<i>foo</i> bar");

		Assert.AreEqual ("<i>f</i>", rts.Substring (0, 1));
		Assert.AreEqual ("<i>fo</i>", rts.Substring (0, 2));
		Assert.AreEqual ("<i>foo</i>", rts.Substring (0, 3));
		Assert.AreEqual ("<i>foo</i> ", rts.Substring (0, 4));
		Assert.AreEqual ("<i>foo</i> b", rts.Substring (0, 5));
		Assert.AreEqual ("<i>foo</i> ba", rts.Substring (0, 6));
		Assert.AreEqual ("<i>foo</i> bar", rts.Substring (0, 7));
	}
}
