namespace ABCBrasil.Hackthon.Api.Infra.Configurations
{
    public class ApiConfig
    {
        public PageListConfig PagedList { get; set; }

        public class PageListConfig
        {
            public PageSizeConfig PageSize { get; set; }
        }

        public class PageSizeConfig
        {
            public int Default { get; set; }
            public int Maximum { get; set; }
        }
    }
}