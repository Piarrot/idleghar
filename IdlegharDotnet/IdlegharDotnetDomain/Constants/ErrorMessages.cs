using System.Runtime.Serialization;

namespace IdlegharDotnetDomain.Constants
{
    public static class ErrorMessages
    {
        public static readonly string MORE_THAN_ONE_CHARACTER = "Cannot create more than one character";
        public static readonly string CHARACTER_NOT_CREATED = "Current user doesn't have a character yet";
        public static readonly string INVALID_QUEST = "Requested quest is invalid";
        public static readonly string CHARACTER_NOT_QUESTING = "Character is not questing";
        internal static readonly string CHARACTER_ALREADY_QUESTING = "Character is already questing";
        internal static readonly string INVALID_ENCOUNTER_STATE = "Character is in an invalid encounter state";
        internal static readonly string INVALID_USER = "Invalid user";
        internal static readonly string EMAIL_ALREADY_VALIDATED = "Email already validated";
        internal static readonly string INVALID_VALIDATION_CODE = "Invalid code";
        internal static readonly string INVALID_CREDENTIALS = "Wrong Credentials";
        internal static readonly string CHANCES_DON_T_REACH_100 = "Chances are less than 100%";
        internal static readonly string EMAIL_IN_USE = "Email already in use";
        internal static readonly string INVALID_EMAIL = "Invalid Email";
    }
}