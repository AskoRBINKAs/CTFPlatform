namespace CTFPlatform;

public static class ContestSettings
{
    public static class CTF
    {
        public static bool Started { get; set; } = true;
        public static bool DynamicScoreEnabled { get; set; } = false;
        public static bool TeamsEnabled { get; set; } = true;
        public static bool FlagAcceptingEnabled { get; set; } = false;
        public static int TeamSize { get; set; } = 5;
    }

    public static class UserConfiguration
    {
        public static bool RegistrationAllowed { get; set; } = true;
        public static bool AccessAllowedDefault { get; set; } = true;
    }
    public static List<Dictionary<string, Dictionary<string,string>>> GetAllSettings()
    {
        List<Dictionary<string, Dictionary<string,string>>> response = new List<Dictionary<string, Dictionary<string,string>>>();
        response.Add(new Dictionary<string, Dictionary<string, string>>() {
            {"CTF", new Dictionary<string, string>(){
                    {"Started",CTF.Started.ToString() },
                    {"DynamicScoreEnabled",CTF.DynamicScoreEnabled.ToString() },
                    {"TeamsEnabled",CTF.TeamsEnabled.ToString() },
                    {"FlagAcceptingEnabled",CTF.FlagAcceptingEnabled.ToString() }
                } 
            }
        });

        response.Add(new Dictionary<string, Dictionary<string, string>>() {
            {"UsersConfiguration", new Dictionary<string, string>(){
                    {"RegistrationAllowed",UserConfiguration.RegistrationAllowed.ToString() },
                    {"AccessAllowedDefault",UserConfiguration.AccessAllowedDefault.ToString() },
                }
            }
        });
        return response;
    }
}