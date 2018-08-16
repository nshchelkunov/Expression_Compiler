
public class Application
{
    public static string Processing (string input)
    {
        Compiler.Сompilation(input);

        if (Compiler.error == "")
        {
            return "Result: " + Interpreter.Interpretation();
        }
        return Compiler.error;
    }
}

