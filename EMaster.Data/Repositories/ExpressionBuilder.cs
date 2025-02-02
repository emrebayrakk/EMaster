using EMaster.Domain.Requests;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

namespace EMaster.Data.Repositories
{
    public static class ExpressionBuilder
    {
        public static Expression<Func<T, bool>> ConstructAndExpressionTree<T>(List<ExpressionFilter> filters)
        {
            if (filters.Count == 0)
                return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            if (filters.Count == 1)
            {
                exp = GetExpression<T>(param, filters[0]);
            }
            else
            {
                exp = GetExpression<T>(param, filters[0]);
                for (int i = 1; i < filters.Count; i++)
                {
                    exp = Expression.And(exp, GetExpression<T>(param, filters[i]));
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        public static Expression GetExpression<T>(ParameterExpression param, ExpressionFilter filter)
        {
            MethodInfo containsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
            MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
            MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });

            MemberExpression member = Expression.Property(param, filter.PropertyName);

            // `Value` JsonElement ise, uygun türe dönüştür
            object filterValue;
            if (filter.Value is JsonElement jsonElement)
            {
                if (member.Type == typeof(int))
                    filterValue = jsonElement.GetInt32();
                else if (member.Type == typeof(decimal))
                    filterValue = jsonElement.GetDecimal();
                else if (member.Type == typeof(DateTime))
                    filterValue = jsonElement.GetDateTime();
                else
                    filterValue = jsonElement.ToString();
            }
            else
            {
                filterValue = filter.Value;
            }

            ConstantExpression constant = Expression.Constant(filterValue, member.Type);

            switch (filter.Comparison)
            {
                case Comparison.Equal:
                    return Expression.Equal(member, constant);
                case Comparison.GreaterThan:
                    return Expression.GreaterThan(member, constant);
                case Comparison.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);
                case Comparison.LessThan:
                    return Expression.LessThan(member, constant);
                case Comparison.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, constant);
                case Comparison.NotEqual:
                    return Expression.NotEqual(member, constant);
                case Comparison.Contains:
                    if (member.Type == typeof(string))
                        return Expression.Call(member, containsMethod, constant);
                    else
                        throw new ArgumentException("Contains sadece string alanlar için kullanılabilir.");
                case Comparison.StartsWith:
                    if (member.Type == typeof(string))
                        return Expression.Call(member, startsWithMethod, constant);
                    else
                        throw new ArgumentException("StartsWith sadece string alanlar için kullanılabilir.");
                case Comparison.EndsWith:
                    if (member.Type == typeof(string))
                        return Expression.Call(member, endsWithMethod, constant);
                    else
                        throw new ArgumentException("EndsWith sadece string alanlar için kullanılabilir.");
                default:
                    return null;
            }
        }
    }
}
