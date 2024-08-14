using System;

namespace ABCBrasil.Hackthon.Api.Infra.Commons.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class ParamTypeAttribute : Attribute
    {
        public string ParamType { get; set; }

        public ParamTypeAttribute(string paramType)
        {
            ParamType = paramType;
        }
    }
}