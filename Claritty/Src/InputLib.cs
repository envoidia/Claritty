using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Claritty.Src;

public static class InputLib
{
    // public static KeyboardState PreviousKeyboardState { get; private set; }
    // public static KeyboardState KeyboardState { get; private set; }

    public static bool CheckInput = true;

    // public static void Update(GameTime gt)
    // {
    //     PreviousKeyboardState = KeyboardState;
    //     KeyboardState = Keyboard.GetState();

    //     if (KeyboardState.IsKeyDown(Keys.C) && KeyboardState.IsKeyDown(Keys.LeftControl))
    //     {
    //         // todo
    //     }
    // }

    public static void TextInput(object? sender, TextInputEventArgs args)
    {
        if (!CheckInput)
        {
            return;
        }

        Debug.Log($"TextInput: {args.Key}");

        if (args.Key == Keys.Enter)
        {
            Terminal.Shell.ExecuteCurrentLine(Terminal.CurLine.ToString());
            Terminal.CurLine.Clear();

            return;
        }

        Terminal.CurLine.Append(args.Character);
    }
}
