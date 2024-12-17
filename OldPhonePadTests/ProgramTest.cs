using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace OldPhonePadTests;

[TestClass]
public sealed class ProgramTest
{
    [TestMethod]
    public void OldPhonePad_ValidInput_ReturnsCorrectString()
    {
        const string input = "2 22 222#";
        var result = OldPhonePad.Program.OldPhonePad(input);
        Assert.AreEqual("ABC", result);
    }

    [TestMethod]
    public void OldPhonePad_MissingHash_ReturnsErrorMessage()
    {
        const string input = "2 22 222";
        var result = OldPhonePad.Program.OldPhonePad(input);
        Assert.AreEqual("The provided input is not valid. Please add # at the end of the input.", result);
    }

    [TestMethod]
    public void OldPhonePad_InputWithSpaces_ReturnsCorrectString()
    {
        const string input = "2 22 222#"; // Spaces (pauses); should ignore them
        var result = OldPhonePad.Program.OldPhonePad(input);
        Assert.AreEqual("ABC", result);
    }
    
    [TestMethod]
    public void OldPhonePad_Case_One()
    {
        const string input = "33#";
        var result = OldPhonePad.Program.OldPhonePad(input);
        Assert.AreEqual("E", result);
    }
    
    [TestMethod]
    public void OldPhonePad_Case_Two()
    {
        const string input = "227*#";
        var result = OldPhonePad.Program.OldPhonePad(input);
        Assert.AreEqual("B", result);
    }
    
    [TestMethod]
    public void OldPhonePad_Case_Three()
    {
        const string input = "4433555 555666#";
        var result = OldPhonePad.Program.OldPhonePad(input);
        Assert.AreEqual("HELLO", result);
    }
    
    [TestMethod]
    public void OldPhonePad_Case_Question()
    {
        const string input = "8 88777444666*664#";
        var result = OldPhonePad.Program.OldPhonePad(input);
        Assert.AreEqual("TURING", result);
    }
    
    [TestMethod]
    public void OldPhonePad_Case_Four()
    {
        const string input = "844266557777#";
        var result = OldPhonePad.Program.OldPhonePad(input);
        Assert.AreEqual("THANKS", result);
    }
}