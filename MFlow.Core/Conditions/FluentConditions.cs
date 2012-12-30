using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Conditions
{
    /// <summary>
    ///     A fluent conditions implementation
    /// </summary>
    public class FluentConditions<T> : IFluentConditions<T>
    {
        /// <summary>
        ///     The internal list of conditions
        /// </summary>
        protected readonly IList<IFluentCondition<T>> _conditions;

        /// <summary>
        ///     The target of this validation instance
        /// </summary>
        protected T _target;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="Target"></param>       
        public FluentConditions(T Target)
        {
            _conditions = new List<IFluentCondition<T>>();
            _target = Target;
        }

        /// <summary>
        ///     Takes a boolean AND condition and evaluates it
        /// </summary>
        public IFluentConditions<T> And(bool condition, string key = "", string message = "")
        {
            var fluentCondition = new FluentCondition<T>(c => condition, ConditionType.And, key, message);
            _conditions.Add(fluentCondition);
            return this;
        }

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean AND condition, then evaluates it
        /// </summary>
        public IFluentConditions<T> And(Expression<Func<T, bool>> expression, string key = "", string message = "")
        {
            var fluentCondition = new FluentCondition<T>(expression, ConditionType.And, key, message);
            _conditions.Add(fluentCondition);
            return this;
        }

        /// <summary>
        ///     Takes a boolean OR condition and evaluates it
        /// </summary>
        public IFluentConditions<T> Or(bool condition, string key = "", string message = "")
        {
            var fluentCondition = new FluentCondition<T>(c => condition, ConditionType.Or, key, message);
            _conditions.Add(fluentCondition);
            return this;
        }

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean OR condition, then evaluates it
        /// </summary>
        public IFluentConditions<T> Or(Expression<Func<T, bool>> expression, string key = "", string message = "")
        {
            var fluentCondition = new FluentCondition<T>(expression, ConditionType.Or, key, message);
            _conditions.Add(fluentCondition);
            return this;
        }

        /// <summary>
        ///     Clear the conditions for the validation instance
        /// </summary>
        public IFluentConditions<T> Clear()
        {
            _conditions.Clear();
            return this;
        }

        /// <summary>
        ///     Takes an action to execute if the validator is satisfied
        /// </summary>
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

        /// <summary>
        ///     Takes an action to execute if the validator is not satisfied
        /// </summary>
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

        /// <summary>
        ///     Returns a boolean indicating if this validator is satisfied
        /// </summary>
        public bool Satisfied()
        {
            return _conditions.All(c => c.Condition.Compile().Invoke(_target) == true && c.Type == ConditionType.And) ||
                _conditions.Any(c => c.Condition.Compile().Invoke(_target) && c.Type == ConditionType.Or);
        }
    }
}
