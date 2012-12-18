using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core
{
    public class FluentConditions : IFluentConditions
    {

        private readonly IList<IFluentCondition> _conditions;

        public FluentConditions()
        {
            _conditions = new List<IFluentCondition>();
        }

        public IFluentConditions And(bool condition)
        {
            var fluentCondition = new FluentCondition(condition, ConditionType.And);
            _conditions.Add(fluentCondition);
            return this;
        }

        public bool Is(bool condition)
        {
            return _conditions.All(c => c.Condition == condition && c.Type == ConditionType.And) || 
                _conditions.Any(c => c.Condition == condition && c.Type == ConditionType.Or);
        }

        public IFluentConditions Or(bool condition)
        {
            var fluentCondition = new FluentCondition(condition, ConditionType.Or);
            _conditions.Add(fluentCondition);
            return this;
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
