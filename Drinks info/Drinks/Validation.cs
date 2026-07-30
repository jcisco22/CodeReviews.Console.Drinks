public class Validation
{


    internal static bool IsStringValid(string stringInput)
    {
        if (String.IsNullOrEmpty(stringInput))
        {
            return false;
        }


        foreach (char letter in stringInput)
        {
            if (!char.IsLetter(letter) && letter != '/' && letter != ' ')
                return false;
        }

        return true;
    }



    public static bool IsIdValid(string stringInput)
    {
        if (string.IsNullOrEmpty(stringInput))
        {
            return false;

        }

        foreach (char c in stringInput)
        {
            if (!Char.IsDigit(c))
            {
                return false;
            }// if not digit return false
        }
        //otherwise
        return true;

    }
}
