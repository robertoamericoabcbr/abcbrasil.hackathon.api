namespace ABCBrasil.Hackthon.Api.Infra.Commons
{
    public static class SwaggerExamplesConstants
    {
        public const string ISSUER = "0001";
        public const string ACCOUNT_NUMBER = "22283277";
        public const string CURRENCY = "BRL";
    }

    public static class SwaggerPatternsConstants
    {
        public const string DATE = "^(\\d{4})-(1[0-2]|0?[1-9])-(3[01]|[12][0-9]|0?[1-9])$";
        public const string DATE_TIME_ZONE = "^(19|2[0-9])[0-9]{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01]) (0[0-9]|1[0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])((\\+|-)[0-1][0-9]{3})?$";
        public const string DECIMAL = "^((\\d{1,16}\\.\\d{2}))$";
        public const string CURRENCY = "^([A-Z]{3})$";
        public const string ISSUER = "(^\\d{4}$)";
        public const string ACCOUNT_NUMBER = "^\\d{1,12}$";
        public const string REMITTANCE_INFORMATION = "[\\w\\W\\s]*";
        public const string EMAIL = "^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$";
        public const string ISPB = "^\\d{3,10}$";
    }
}