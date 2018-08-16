
using System;

public static class Syntax_Validator
{
    // If valid cases, return false, else true
    public static bool SyntaxValidator(string option,
                                       char token,
                                       char leftToken,
                                       char rightToken)
    {
        // first token =='(', and right token == number or '-'
        if (option == "first" && 
           (token == '(' && (Char.IsDigit(rightToken) || "(-".IndexOf(rightToken) > -1)))
        {
            return false;
        } 
        // latest token == number, left token == operator 
        // or the last token == ')', left token ==  number or ')'
        if (option == "last" && 
           (Char.IsDigit(token) && "+-*/".IndexOf(leftToken) > -1) || 
           (token == ')' && (Char.IsDigit(leftToken) || leftToken == ')')))
        {  
            return false;
        }
        // tokens to the left and right exist
        if (option == "between")
        {
            // token == operator, left token == number or ')' 
            // and right token == number or '('
            if (("+-*/".IndexOf(token) > -1) &&
               (Char.IsDigit(leftToken) || leftToken == ')') &&
               (Char.IsDigit(rightToken) || rightToken == '('))
            {
                return false;
            }
            // token == '(', left token == operator or '(', 
            // right token == number or '(' or '-'
            if (token == '(' &&
               ("+-*/".IndexOf(leftToken) > -1 || leftToken == '(') &&
               (Char.IsDigit(rightToken) || "(-".IndexOf(rightToken) > -1))
            {
                return false;
            }
            // token == ')', left token == number or ')'  right token operator, or ')'
            if (token == ')' &&
               (Char.IsDigit(leftToken) || leftToken == ')') &&
               ("+-*/".IndexOf(rightToken) > -1 || rightToken == ')'))
            {
                return false;
            }
        }
        return true;
    }
}
