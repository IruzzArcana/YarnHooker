using MelonLoader;

namespace YarnHooker
{
    internal class Config
    {
        private static MelonPreferences_Category category;
        private static MelonPreferences_Entry<string> filepath;
        private const string m_cfg = "UserData/yarn.cfg";
        private const string m_deffilepath = "yarnproj.yarnc";

        public static string FilePath => filepath.Value;

        public static void Init()
        {
            category = MelonPreferences.CreateCategory("YarnHooker");
            category.SetFilePath(m_cfg, false);

            category.LoadFromFile();

            if (category.HasEntry("filepath"))
            {
                filepath = category.GetEntry<string>("filepath");
            }
            else
            {
                filepath = category.CreateEntry("filepath", m_deffilepath);
                category.SaveToFile();
            }
        }

    }
}
