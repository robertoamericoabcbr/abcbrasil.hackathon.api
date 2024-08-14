using System;

namespace ABCBrasil.Hackthon.Api.Infra.Commons.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class ParamNameAttribute : Attribute
    {
        public string ParamName { get; set; }

        public ParamNameAttribute(string paramName)
        {
            ParamName = paramName;
        }
    }
}