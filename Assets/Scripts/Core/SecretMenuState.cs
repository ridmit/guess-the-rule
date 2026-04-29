public static class SecretMenuState
{
    public static bool ShouldEnterSecretMenu { get; private set; }

    public static void EnterSecretMenu()
    {
        ShouldEnterSecretMenu = true;
    }

    public static void ExitSecretMenu()
    {
        ShouldEnterSecretMenu = false;
    }
}