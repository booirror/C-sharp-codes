class Palindrome
{
    static public void PalindromeMain()
    {
        System.Console.Write("please enter a palindrome: ");
        string palindrome = System.Console.ReadLine();
        Palindrome instance = new Palindrome();
        if (instance.IsPalindrome(palindrome))
        {
            System.Console.WriteLine("\"{0}\" is a palindrome.", palindrome);
        }
        else
        {
            System.Console.WriteLine("\"{0}\" is NOT a palindrome.", palindrome);
        }
    }

    public bool IsPalindrome(string text)
    {
        char[] temp;
        string reverse;
        reverse = text.Replace(" ", "");
        reverse = reverse.ToLower();
        temp = reverse.ToCharArray();
        System.Array.Reverse(temp);
        return reverse == new string(temp);
    }
}