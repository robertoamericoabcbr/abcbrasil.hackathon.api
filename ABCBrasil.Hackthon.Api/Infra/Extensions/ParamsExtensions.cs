using ABCBrasil.Hackthon.Api.Infra.Commons.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ABCBrasil.Hackthon.Api.Infra.Extensions
{
    public static class ParamsExtensions
    {
        public static string GetParamType(this object obj)
        {
            object[] attrs = obj.GetType().GetCustomAttributes(typeof(ParamTypeAttribute), false);

            if (attrs.Length > 0)
            {
                var attr = (ParamTypeAttribute)attrs[0];
                return attr.ParamType;
            }

            return null;
        }

        public static string GetParamName<TSource, TProp>(this TSource obj, Expression<Func<TSource, TProp>> expression)
        {
            IReadOnlyList<MemberInfo> path = PropertyPath<TSource>.Get(expression);
            return string.Join(".", path.Select(p =>
            {
                ParamNameAttribute paramNameAttr = p.GetCustomAttribute<ParamNameAttribute>(false);

                if (paramNameAttr != null)
                {
                    return paramNameAttr.ParamName;
                }

                FromQueryAttribute fromQueryAttr = p.GetCustomAttribute<FromQueryAttribute>(false);

                return fromQueryAttr != null
                    ? fromQueryAttr.Name
                    : p.Name.ToCamelCase();
            }));
        }

        private static class PropertyPath<TSource>
        {
            public static IReadOnlyList<MemberInfo> Get<TResult>(Expression<Func<TSource, TResult>> expression)
            {
                var visitor = new PropertyVisitor();
                visitor.Visit(expression.Body);
                visitor.Path.Reverse();

                return visitor.Path;
            }

            private sealed class PropertyVisitor : ExpressionVisitor
            {
                internal readonly List<MemberInfo> Path = new();

                protected override Expression VisitMember(MemberExpression node)
                {
                    if (node.Member is not PropertyInfo)
                    {
                        throw new ArgumentException("The path can only contain properties", nameof(node));
                    }

                    Path.Add(node.Member);
                    return base.VisitMember(node);
                }
            }
        }
    }
}