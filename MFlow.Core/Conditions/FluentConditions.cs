using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Conditions
{
    public class FluentConditions : IFluentConditions
    {

        private readonly IList<IFluentCondition> _conditions;

        public FluentConditions()
        {
            _conditions = new List<IFluentCondition>();
        }

        public IFluentConditions If(bool condition)
        {
            And(condition);
            return this;
        }

        public IFluentConditions And(bool condition)
        {
            var fluentCondition = new FluentCondition(condition, ConditionType.And);
            _conditions.Add(fluentCondition);
            return this;
        }

        public IFluentConditions Or(bool condition)
        {
            var fluentCondition = new FluentCondition(condition, ConditionType.Or);
            _conditions.Add(fluentCondition);
            return this;
        }

        public IFluentConditions Clear()
        {
            _conditions.Clear();
            return this;
        }

        public IFluentConditions Then(Action execute, ExecuteThread options = ExecuteThread.Current)
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

        public IFluentConditions Else(Action execute, ExecuteThread options = ExecuteThread.Current)
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
            return _conditions.All(c => c.Condition == true && c.Type == ConditionType.And) ||
                _conditions.Any(c => c.Condition == true && c.Type == ConditionType.Or);
        }
    }

    internal class FluentCondition : IFluentCondition
    {
        public FluentCondition(bool condition, ConditionType type)
        {
            Condition = condition;
            Type = type;
        }

        public bool Condition { get; private set; }
        public ConditionType Type { get; private set; }
    }
}
