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

		Assert.AreEqual ("<i>f</i>", rts.Substring (1));
		Assert.AreEqual ("<i>fo</i>", rts.Substring (2));
		Assert.AreEqual ("<i>foo</i>", rts.Substring (3));
		Assert.AreEqual ("<i>foo</i> ", rts.Substring (4));
		Assert.AreEqual ("<i>foo</i> b", rts.Substring (5));
		Assert.AreEqual ("<i>foo</i> ba", rts.Substring (6));
		Assert.AreEqual ("<i>foo</i> bar", rts.Substring (7));
	}
}
