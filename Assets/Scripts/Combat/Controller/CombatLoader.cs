namespace Combat.Controller
{
    public class CombatLoader
    {
        public static CombatLoader Instance => instance ??= new CombatLoader();
        private static CombatLoader instance;
    }
}