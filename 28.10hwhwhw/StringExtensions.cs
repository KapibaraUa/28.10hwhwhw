using System.Collections.Generic;

namespace HomeworkEventsAndExtensions;

internal static class StringExtensions
{
    public static bool IsValidBrackets(this string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return true;
        }

        Stack<char> stack = new Stack<char>();

        foreach (char ch in text)
        {
            if (ch == '(' || ch == '{' || ch == '[')
            {
                stack.Push(ch);
            }
            else if (ch == ')' || ch == '}' || ch == ']')
            {
                if (stack.Count == 0)
                {
                    return false;
                }

                char lastOpen = stack.Pop();

                if ((ch == ')' && lastOpen != '(') ||
                    (ch == '}' && lastOpen != '{') ||
                    (ch == ']' && lastOpen != '['))
                {
                    return false;
                }
            }
        }

        return stack.Count == 0;
    }
}