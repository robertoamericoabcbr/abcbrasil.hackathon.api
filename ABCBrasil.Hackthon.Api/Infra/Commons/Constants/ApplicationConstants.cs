namespace ABCBrasil.Hackthon.Api.Infra.Commons.Constants
{
    public static class ApplicationConstants
    {
        #region "NOTIFICATIONS"

        public const string ERROR = "Error";
        public const string SERVICE_UNAVAILABLE = "IntegrationError";
        public const string CONTRACT_VALIDATION = "Validation";
        public const string DOMAIN_VALIDATION = "BusinessValidation";
        public const string INFO = "Information";
        public const string INTERNAL_SERVER_ERROR = "Critical";
        public const string NOT_FOUND = "NotFound";

        #endregion "NOTIFICATIONS"

        #region MESSAGES

        public static class Messages
        {
            public struct Validations
            {
                public const string ERROR_INTEGRATION = "Ocorreu um erro inesperado, tente novamente";
                public const string SERVICE_UNAVAILABLE_MESSAGE = "Serviço não está disponível no momento. Serviço solicitado pode estar em manutenção ou fora da janela de funcionamento.";
                public const string NOT_INFORMED = "Não informado.";
                public const string INVALID_FIELD_VALUE = "O valor informado é inválido";
                public const string INVALID_FIELD_FORMAT = "O formato do campo é inválido";
                public const string INVALID_REQUEST_BODY = "Um ou mais dados contidos no corpo da requisiçao são inválidos.";
                public const string MISSING_FIELD = "O campo é obrigatório";
                public const string RESOURCE_NOT_FOUND = "Recurso não encontrado.";
            }

            public struct FilterValidations
            {
                public const string INVALID_PAGE = "O numero da página informado é inválido. Por favor, fornecer um numero inteiro e maior que zero.";
                public const string INVALID_PAGE_SIZE = "O numero de itens por página é inválido. Por favor, fornecer um número inteiro e maior que zero.";
                public const string INVALID_PERIOD_RANGE = "Consulta não realizada. Período consultado é superior ao range máximo permitido de 30 dias.";
                public const string INVALID_PAGE_SIZE_LENGTH = "A quantidade de itens por página é inválida. O número de itens por página não pode ser maior que {0}.";
                public const string INVALID_START_DATE = "A data inicio é inválida.";
                public const string INVALID_END_DATE = "A data fim é inválida.";
                public const string INVALID_STATUS = "O status informado na consulta é inválido.";
                public const string PAGE_NOT_FOUND = "Página não encontrada.";
            }
        }

        #endregion MESSAGES

        #region APIConfig

        public const string API_VERSION_1 = "1";
        public const string API_VERSION_2 = "2";
        public const string ROUTE_DEFAULT_ID = "{id}";
        public const string ROUTE_DEFAULT_SEARCH = "{pageNumber}/{rowsPerPage}";
        public const string ROUTE_DEFAULT_CONTROLLER = "api/v{version:apiVersion}/[controller]";
        public const string ROUTE_CREDIT_ANALYSIS_CONTROLLER = "api/v{version:apiVersion}/credit-analyzes/";
        public const string ROUTE_CREDIT_LIMITS_CONTROLLER = "api/v{version:apiVersion}/credit-limits/";
        public const string BINDING_PAGE_NUMBER = "pageNumber";
        public const string BINDING_ROWS_PER_PAGE = "rowsPerPage";
        public const string PAGE_FIELD = "page";
        public const string PAGE_SIZE_FIELD = "page_size";

        public struct TypeValidations
        {
            public const string BODY = "body";
            public const string HEADER = "header";
            public const string QUERY = "query";
            public const string ROUTER = "router";
        }

        #endregion APIConfig
    }
}