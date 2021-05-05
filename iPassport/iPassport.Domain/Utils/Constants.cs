namespace iPassport.Domain.Utils
{
    public static class Constants
    {
        public const int DISEASE_TEST_VALIDATE_IN_HOURS = 72;
        public const string DISEASE_TEST_NAME = "PCR";
        public const int IMPORT_USER_MAX_DEGREE_OF_PARALLELISM = 10;
        public const string COLUMN_NAME_IMPORT_FILE_TO_RESOURCE = "ColumnNameImportFile";

        public const string CONST_POSITIVO_VALUE = "POSITIVO";
        public const string CONST_NEGATIVO_VALUE = "NEGATIVO";
        public const string CONST_SIM_VALUE = "SIM";
        public const string CONST_NAO_VALUE = "NÃO";
        public const string CONST_NENHUM_VALUE = "NENHUM";

        public const int MAX_LENGHT_IMPORT_USERS_FILE = 82160; // 80KB

        public const string ADMIN_PROFILE_KEY = "admin";

        public const string TOKEN_CLAIM_USER_ID = "UserId";
        public const string TOKEN_CLAIM_PROFILE = "Profile";
        public const string TOKEN_CLAIM_HAS_PHOTO = "HasPhoto";
        public const string TOKEN_CLAIM_HAS_PLAN = "HasPlan";
        public const string TOKEN_CLAIM_FIRST_LOGIN = "FirstLogin";
        public const string TOKEN_CLAIM_FULL_NAME = "FullName";
        public const string TOKEN_CLAIM_CITY_ID = "CityId";
        public const string TOKEN_CLAIM_STATE_ID = "StateId";
        public const string TOKEN_CLAIM_COUNTRY_ID = "CountryId";
        public const string TOKEN_CLAIM_COMPANY_ID = "CompanyId";
        public const string TOKEN_CLAIM_HEALTH_UNITY_ID = "HealthUnityId";

        public const string S3_USER_IMAGES_PATH = "images/users";
    }
}
