
public static class StringExtensions
{
    public static string Pluralize(this string word, int value)
    {
        if (string.IsNullOrWhiteSpace(word))
            return word;

        bool isLower = char.IsLower(word[word.Length-1]); //check letter before pluralization

        var plural = value == 1 ? word : word + "S";

        // If original word starts lowercase, make plural lowercase too
        return isLower ? plural.ToLower() : plural.ToUpper();
    }

}
