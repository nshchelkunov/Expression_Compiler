
/*
 *  This step-by-step  compiler implements the Dijkstra's algorithm "sorting station" 
 *  (character processing with one stack).
 *  Shared data: list for source code, list for object code, string for error.
*/

using System;
using System.Collections.Generic;

public static class Compiler
{
    public static List<double> objectCode = new List<double>();
    public static List<int> operatorsIndex = new List<int>();
    public static string error;

    private static int parenthesisСount; 
    private static bool unaryMinus;

    private static Stack<char> stack = new Stack<char>();
    private static string position;
    private static string number;
    private static char token;
    private static int index;
    private static bool wrong;

    public static void Сompilation(string sourceCode)
    {
        objectCode.Clear();
        operatorsIndex.Clear();
        stack.Clear();
        error = String.Empty;

        parenthesisСount = 0;
        unaryMinus = false;

        sourceCode = sourceCode.Replace(" ", string.Empty);

        for (int i = 0; i < sourceCode.Length; i++)
        {
            // token == digit, left token != ')' and right token != '(' 
            if (Char.IsDigit(sourceCode[i]) &&
                (!(i != 0 && sourceCode[i - 1] == ')') || (sourceCode.Length > i + 1 && sourceCode[i + 1] == '(')))
            {
                number = String.Empty;
                while ((Char.IsDigit(sourceCode[i])) ||
                      ((sourceCode[i] == ',') &&
                      ((Char.IsDigit(sourceCode[i - 1]))) && ((Char.IsDigit(sourceCode[i - 1])))))
                {
                    number += sourceCode[i];
                    i++;
                    if (i == sourceCode.Length)
                    {
                        break;
                    }
                }
                i--;
                if (unaryMinus == false)
                {
                    objectCode.Add(double.Parse(number));
                }
                else
                {
                    objectCode.Add((double.Parse(number)) * -1);
                    unaryMinus = false;
                }
                continue;
            }

            // unary minus 
            if (sourceCode[i] == '-' && 
               (i == 0 || sourceCode[i - 1] == '(' && 
               (sourceCode.Length > i + 1 && Char.IsDigit(sourceCode[i + 1]))))
            {
                unaryMinus = true;
                continue;
            }

            // SyntaxValidator(int index, int inputLength, char token, char leftToken, char rightToken)
            // If valid cases, return false, else true
            if (i == 0 && sourceCode.Length > 1)
            {
                wrong = Syntax_Validator.SyntaxValidator("first", sourceCode[i], ' ', sourceCode[i + 1]);
            } 
            else if (i == sourceCode.Length - 1)
            {
                wrong = Syntax_Validator.SyntaxValidator("last", sourceCode[i], sourceCode[i - 1], ' ');
            }
            else 
            {
                wrong = Syntax_Validator.SyntaxValidator("between", sourceCode[i], sourceCode[i - 1], sourceCode[i + 1]);
            }

            if (wrong)
            {
                error = "Not a valid " + sourceCode[i] + " at position " + (i + 1) + " or adjacent token";
                return;
            }

            // brackets
            if (sourceCode[i] == '(')
            {
                parenthesisСount++;
                stack.Push(sourceCode[i]);
                continue;
            }
            else if (sourceCode[i] == ')')
            {
                parenthesisСount--;
                if (parenthesisСount < 0)
                {
                    error = "Not a valid ')' at position " + (i + 1);
                    return;
                }
                token = stack.Pop();
                while (token != '(')
                {
                    AddOperator(token);
                    token = stack.Pop();
                }
                continue;
            }

            // operator
            if (stack.Count > 0)
            {
                index = IsOperator(sourceCode[i]);
                if (index == 5)
                {
                    index = 4;
                }
                if (index <= IsOperator(stack.Peek()))
                {
                    token = stack.Pop();
                    AddOperator(token);
                }
            }
            stack.Push(sourceCode[i]);
        }
        if (parenthesisСount > 0)
        {
            error = "Not a valid '(' at position " + FindParenthesis(sourceCode);
            return;
        }
        while (stack.Count > 0)
        {
            AddOperator(stack.Pop());
        }
    }

    private static void AddOperator(char token)
    {
        objectCode.Add(IsOperator(token));
        operatorsIndex.Add(objectCode.Count - 1);
    }

    private static int IsOperator(char token)
    {
        // '(', ')', '+', '-', '*' => 0, 1, 2, 3, 4, 5  if not: return -1
        index = "()+-*/".IndexOf(token); 
        return index;
    }

    private static Stack<int> parenthesisStack = new Stack<int>();

    private static int FindParenthesis(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '(')
            {
                parenthesisStack.Push(i + 1);
            }
            if (input[i] == ')')
            {
                parenthesisStack.Pop();
            }
        }
        return parenthesisStack.Pop();
    }
}



