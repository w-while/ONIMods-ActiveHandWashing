namespace ActiveHandWashing
{
    public static class ActiveHandWashingStrings
    {
        public static class DUPLICANTS
        {
            public static class CHORES
            {
                public static class PRECONDITIONS
                {
                    public static LocString DOES_DUPE_NEED_WASH_HANDS = "Duplicant no need to wash hands";
                    public static LocString DOES_DUPE_AT_WASH_TIME = "It's not time to wash  hands";

                }
                public static class ACTIVEHANDWASHING
                {
                    public static LocString NAME = "Active Washing Hands";

                    public static LocString STATUS = "Going to washing Hands";

                    public static LocString TOOLTIP = "When duplicants exceed a certain number of germs, actively go to wash hands";
                }
            }
        }
        public static class UI
        {
            public static class FRONTEND
            {
                public static class TELEPORTSUITMOD
                {
                    public static LocString PRIORIT_VALUE = "Priority value of wash task";
                    public static LocString PRIORITY_CLASS = "Priority class of wash task";
                    public static LocString RECREATION_TIME = "Should Duplicants active wash hand at recreation time";
                    public static LocString WORK_TIME = "Should Duplicants active wash hand at work time";
                    public static LocString SLEEP_TIME = "Should Duplicants active wash hand at sleep time";
                    public static LocString HYGIENE_TIME = "Should Duplicants active wash hand at hygiene time";
                }
            }
        }
        internal static void DoReplacement()
        {
            LocString.CreateLocStringKeys(typeof(UI));

        }
    }
}
