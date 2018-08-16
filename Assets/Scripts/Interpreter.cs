
using System;
using System.Collections.Generic;

public static class Interpreter
{
    private static double a;
    private static double b;
    private static double result;
    private static Stack<double> stack = new Stack<double>();

    public static double Interpretation()
    {
        for (int i = 0; i < Compiler.objectCode.Count; i++) 
        {
            if (Compiler.operatorsIndex.Contains(i))
            {
                a = stack.Pop();
                b = stack.Pop();
                //  0, 1, 2, 3, 4, 5  => (,  ),  +,  -,  *, /
                switch (Convert.ToString(Compiler.objectCode[i])) 
                {
                    case "2": result = b + a; break;
                    case "3": result = b - a; break;
                    case "4": result = b * a; break;
                    case "5": result = b / a; break;
                }
                stack.Push(result);
            }
            else 
            {
                stack.Push(Compiler.objectCode[i]);
            }
        }
        result = stack.Peek();
        return result; 
    }
}



