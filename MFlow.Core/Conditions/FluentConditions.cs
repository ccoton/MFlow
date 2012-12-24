using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Conditions
{
    public class FluentConditions<T> : IFluentConditions<T>
    {

        private readonly IList<IFluentCondition<T>> _conditions;
        protected T _target;
        public FluentConditions(T Target)
        {
            _conditions = new List<IFluentCondition<T>>();
            _target = Target;
        }

        public IFluentConditions<T> If(bool condition, string key = "", string message = "")
        {
            And(condition, key, message);
            return this;
        }

        public IFluentConditions<T> If(Expression<Func<T, bool>> expression, string key = "", string message = "")
        {
            And(expression, key, message);
            return this;
        }

        public IFluentConditions<T> And(bool condition, string key = "", string message = "")
        {
            var fluentCondition = new FluentCondition<T>(c => condition, ConditionType.And, key, message);
            _conditions.Add(fluentCondition);
            return this;
        }

        public IFluentConditions<T> And(Expression<Func<T, bool>> expression, string key = "", string message = "")
        {
            var fluentCondition = new FluentCondition<T>(expression, ConditionType.And, key, message);
            _conditions.Add(fluentCondition);
            return this;
        }

        public IFluentConditions<T> Or(bool condition, string key = "", string message = "")
        {
            var fluentCondition = new FluentCondition<T>(c => condition, ConditionType.Or, key, message);
            _conditions.Add(fluentCondition);
            return this;
        }

        public IFluentConditions<T> Or(Expression<Func<T, bool>> expression, string key = "", string message = "")
        {
            var fluentCondition = new FluentCondition<T>(expression, ConditionType.Or, key, message);
            _conditions.Add(fluentCondition);
            return this;
        }

        public IFluentConditions<T> Clear()
        {
            _conditions.Clear();
            return this;
        }

        public IFluentConditions<T> Then(Action execute, ExecuteThread options = ExecuteThread.Current)
        {
            if (Satisfied())
            {
                if (options == ExecuteThread.Current)
                {
                    execute();
                }
                else
                {
                    System.Threading.ThreadPool.QueueUserWorkItem(delegate
                    {
                        execute();
                    });
                }
            }
            return this;
        }

        public IFluentConditions<T> Else(Action execute, ExecuteThread options = ExecuteThread.Current)
        {
            if (!Satisfied())
            {
                if (options == ExecuteThread.Current)
                {
                    execute();
                }
                else
                {
                    System.Threading.ThreadPool.QueueUserWorkItem(delegate
                    {
                        execute();
                    });
                }
            }
            return this;
        }

        public bool Satisfied()
        {
            return _conditions.All(c => c.Condition.Compile().Invoke(_target) == true && c.Type == ConditionType.And) ||
                _conditions.Any(c => c.Condition.Compile().Invoke(_target) && c.Type == ConditionType.Or);
        }

        public IEnumerable<IFluentCondition<T>> Conditions
        {
            get
            {
                return _conditions;
            }
        }
    }

    internal class FluentCondition<T> : IFluentCondition<T>
    {
        public FluentCondition(Expression<Func<T, bool>> condition, ConditionType type, string key, string message)
        {
            Condition = condition;
            Type = type;
            Key = key;
            Message = message;
        }

        public Expression<Func<T, bool>> Condition { get; private set; }
        public ConditionType Type { get; private set; }
        public string Key { get; private set; }
        public string Message { get; private set; }
    }
}
