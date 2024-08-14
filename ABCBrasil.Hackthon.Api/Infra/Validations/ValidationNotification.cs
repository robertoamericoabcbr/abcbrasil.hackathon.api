using ABCBrasil.Providers.BasicContractProvider.Lib;

namespace ABCBrasil.Hackthon.Api.Infra.Validations
{
    public class ValidationNotification : NotificationBase
    {
        public string Code { get; set; }
        public string Param { get; set; }
        public string ParamType { get; set; }
        public bool? IsIdempotenceFail { get; set; }
    }
}