using System.Runtime.Serialization;

namespace IdlegharDotnetDomain.Constants
{
    public static class ErrorMessages
    {
        public static readonly string MORE_THAN_ONE_CHARACTER = "Cannot create more than one character";
        public static readonly string CHARACTER_NOT_CREATED = "Current player doesn't have a character yet";
        public static readonly string INVALID_QUEST = "Requested quest is invalid";
        public static readonly string CHARACTER_NOT_QUESTING = "Character is not questing";
        public static readonly string CHARACTER_ALREADY_QUESTING = "Character is already questing";
        public static readonly string INVALID_ENCOUNTER_STATE = "Character is in an invalid encounter state";
        public static readonly string INVALID_PLAYER = "Invalid player";
        public static readonly string EMAIL_ALREADY_VALIDATED = "Email already validated";
        public static readonly string INVALID_VALIDATION_CODE = "Invalid code";
        public static readonly string INVALID_CREDENTIALS = "Wrong Credentials";
        public static readonly string CHANCES_DON_T_REACH_100 = "Chances are less than 100%";
        public static readonly string EMAIL_IN_USE = "Email already in use";
        public static readonly string INVALID_EMAIL = "Invalid Email";
        public static readonly string ITEM_NOT_OWNED = "Item not owned by player, cannot equip";
        public static readonly string INVALID_ITEM = "Invalid item";
        public static readonly string INVALID_ITEM_TYPE = "Invalid item type";
        public static readonly string INVALID_REWARD = "Invalid Reward";
        public static readonly string CHARACTER_IS_NOT_LEVELING_UP = "Character is not leveling up";
        public static readonly string INVALID_STAT_POINTS_AMOUNT = "Invalid amount of character stat points";
        public static readonly string INVALID_STAT = "Invalid character stat";
    }
}