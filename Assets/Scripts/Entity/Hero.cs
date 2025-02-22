public class Hero : EntityAttack
{
    public static Hero instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
