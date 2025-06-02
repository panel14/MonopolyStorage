namespace MonopolyStorage.Presentation.Interactive.Utils
{
    public static class SafetyWrapper
    {
        public static void SafetyExecuteCommand(Action executeAction)
        {
            try
            {
                executeAction();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка во время испольнения команды: {ex.Message}");
            }

        }
    }
}
