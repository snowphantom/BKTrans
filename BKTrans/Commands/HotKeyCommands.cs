using System.Windows.Input;

namespace BKTrans.Commands
{
    public static class HotKeyCommands
    {
        public static readonly RoutedUICommand TakeScreenCapture = new RoutedUICommand
            (
                nameof(TakeScreenCapture),
                nameof(TakeScreenCapture),
                typeof(HotKeyCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.T, ModifierKeys.Control | ModifierKeys.Alt)
                }
            );
    }
}
