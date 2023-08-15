using Newtonsoft.Json;
using PeterHan.PLib.Options;

namespace ActiveHandWashing
{
    [RestartRequired]
    [JsonObject(MemberSerialization.OptIn)]
    [ConfigFile("ActiveHandWashing.json" , SharedConfigLocation: true)]
    public class Options
    {
        public const int CURRENT_CONFIG_VERSION = 1;
        private static Options _instance;
        public static Options Instance
        {
            get
            {
                var opts = _instance;
                if (opts == null)
                {
                    opts = POptions.ReadSettings<Options>();
                    if (opts == null || opts.ConfigVersion < CURRENT_CONFIG_VERSION)
                    {
                        opts = new Options();
                        POptions.WriteSettings(opts);
                    }
                    _instance = opts;
                }
                return opts;
            }
        }
        public Options()
        {
            ConfigVersion = CURRENT_CONFIG_VERSION;
        }
        [JsonProperty]
        public int ConfigVersion
        {
            get; set;
        }
        [Option("STRINGS.UI.FRONTEND.ACTIVEHANDWASHING.GERM_COUNT" , "" , null)]
        [JsonProperty]
        [Limit(1 , 99999)]
        public int germCount { get; set; } = 1;

        [Option("STRINGS.UI.FRONTEND.ACTIVEHANDWASHING.PRIORIT_VALUE" , "" , null)]
        [JsonProperty]
        [Limit(1 , 9)]
        public int prioritValue { get; set; } = 6;

        [Option("STRINGS.UI.FRONTEND.ACTIVEHANDWASHING.PRIORITY_CLASS" , "" , null)]
        [JsonProperty]
        public PriorityScreen.PriorityClass priorityClass { get; set; } = PriorityScreen.PriorityClass.personalNeeds;

        [Option("STRINGS.UI.FRONTEND.ACTIVEHANDWASHING.RECREATION_TIME" , "" , null)]
        [JsonProperty]
        public bool RecreationTime { get; set; } = true;

        [Option("STRINGS.UI.FRONTEND.ACTIVEHANDWASHING.WORK_TIME" , "" , null)]
        [JsonProperty]
        public bool WorkTime { get; set; } = false;

        [Option("STRINGS.UI.FRONTEND.ACTIVEHANDWASHING.SLEEP_TIME" , "" , null)]
        [JsonProperty]
        public bool SleepTime { get; set; } = false;

        [Option("STRINGS.UI.FRONTEND.ACTIVEHANDWASHING.HYGIENE_TIME" , "" , null)]
        [JsonProperty]
        public bool HygieneTime { get; set; } = true;
    }
}
