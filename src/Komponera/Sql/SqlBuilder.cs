using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Komponera
{
    public class SqlBuilder<T>
    {
        private string _sqlQuery = string.Empty;

        private List<string> selectedColumns = new List<string>();

        public SqlBuilder(){

        }

        public SqlBuilder<T> Select(string columnList)
        {
            return this;
        }

        public SqlBuilder<T> Select(List<string> columns)
        {
            return this;
        }

        public SqlBuilder<T> Select<TField>(Expression<Func<T, TField>> include)
        {
            Type type = typeof(T);

            MemberExpression member = include.Body as MemberExpression;

            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    include.ToString()));


            selectedColumns.Add(member.Member.Name);
            return this;
        }

        public string Query()
        {
            return string.Join(",", selectedColumns);
        }
    }
}
