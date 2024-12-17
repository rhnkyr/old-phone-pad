using System.Text;
using System.Text.RegularExpressions;

namespace OldPhonePad;

public class Program
{
    static void Main(string[] args)
    {
        Console.Write("Please enter digits and sign to be converted:");
        var input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine(OldPhonePad(input));
        }
    }
    /// <summary>
    /// Converts the input to the old phone pad
    /// </summary>
    /// <param name="input">Digits and signs input to be converted</param>
    /// <returns>Encoded text</returns>
    public static string OldPhonePad(string input)
    {
        // We expect to have # at the end of the input.
        if (!input.Trim().EndsWith('#'))
        {
            return "The provided input is not valid. Please add # at the end of the input.";
        }

        return PopulateOutput(PrepareStrokeList(input.Trim()));
    }

    /// <summary>
    /// Converts digits to readable string
    /// </summary>
    /// <param name="strokeList">List of stroke groups</param>
    /// <returns>Populated string</returns>
    private static string PopulateOutput(List<string> strokeList)
    {
        // This method easy to read instead of creating a range
        string[] letterGroups = ["ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"];
        var result = new StringBuilder();

        foreach (var stroke in strokeList)
        {
            // Determine the digit
            var digit = int.Parse(stroke.Substring(0, 1));// For "333" it is 3
            // Determine the letter group by digit
            var letterGroup = letterGroups[digit - 2];// Since our digits start from 2 so for 3, it will be DEF.
            // Char count to find letter index
            var charCount = stroke.Length;// For "333" it is 3
            // Find the letter
            var letter = letterGroup[charCount - 1];// For "333" it is F

            result.Append(letter);
        }

        return result.ToString();
    }

    /// <summary>
    /// The stroke list for conversion helper
    /// </summary>
    /// <param name="input">Digits and signs input to be converted</param>
    /// <returns></returns>
    private static List<string> PrepareStrokeList(string input)
    {
        // Cleanup the input. We don't need 1 and 0 digits.
        input = input.Replace("1", string.Empty);
        input = input.Replace("0", string.Empty);
        input = input.Replace("#", string.Empty);

        // Let's group digits to calculate char position later
        var matchList = Regex.Matches(input, @"(.)\1*", RegexOptions.IgnoreCase);
        // List non-empty matches because of spaces (pauses)
        var strokeList = matchList.Select(match => match.Value).ToList().Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
        // Cleanup strokeList for deletions
        for (var i = 0; i < strokeList.Count; i++)
        {
            if (strokeList[i] != "*") continue;
            strokeList.RemoveAt(i);// Remove deletion stroke
            strokeList.RemoveAt(i - 1); // Remove the previous stroke since it is unrelated
            i -= 2;// Adjust the index
        }

        return strokeList;
    }
}
