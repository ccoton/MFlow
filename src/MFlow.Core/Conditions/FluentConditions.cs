﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MFlow.Core.Conditions.Enums;
using MFlow.Core.Internal;
using MFlow.Core.ExpressionBuilder;

namespace MFlow.Core.Conditions
{
    /// <summary>
    ///     A fluent conditions implementation
    /// </summary>
    public class FluentConditions<T> : IFluentConditions<T>
    {
        /// <summary>
        ///     The list of conditions
        /// </summary>
        public IList<IFluentCondition<T>> Conditions {get; private set;}

        /// <summary>
        ///     The target of this validation instance
        /// </summary>
        protected T _target;
        IBuildExpressions _expressionBuilder; 

        /// <summary>
        ///     Constructor
        /// </summary>      
        public FluentConditions(T target, IBuildExpressions expressionBuilder)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            if (expressionBuilder == null)
                throw new ArgumentNullException("expressionBuilder");

            _target = target;
            Conditions = new List<IFluentCondition<T>>();
            _expressionBuilder = expressionBuilder;
        }

        /// <summary>
        ///     Takes a boolean IF condition and evaluates it
        /// </summary>
        public IFluentConditions<T> If(bool condition, string key = "", string message = "", string hint = "", ConditionOutput output = ConditionOutput.Error)
        {
            And(condition, key, message, hint, output);
            return this;
        }

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean IF condition, then evaluates it
        /// </summary>
        public IFluentConditions<T> If(Expression<Func<T, bool>> expression, string key = "", string message = "", string hint = "", ConditionOutput output = ConditionOutput.Error)
        {
            And(expression, key, message, hint, output);
            return this;
        }

        /// <summary>
        ///     Takes a boolean AND condition and evaluates it
        /// </summary>
        public IFluentConditions<T> And(bool condition, string key = "", string message = "", string hint = "", ConditionOutput output = ConditionOutput.Error)
        {
            var fluentCondition = new FluentCondition<T>(c => condition, ConditionType.And, key, message, hint, output);
            Conditions.Add(fluentCondition);
            return this;
        }

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean AND condition, then evaluates it
        /// </summary>
        public IFluentConditions<T> And(Expression<Func<T, bool>> expression, string key = "", string message = "", string hint = "", ConditionOutput output = ConditionOutput.Error)
        {
            var fluentCondition = new FluentCondition<T>(expression, ConditionType.And, key, message, hint, output);
            Conditions.Add(fluentCondition);
            return this;
        }

        /// <summary>
        ///     Takes a boolean OR condition and evaluates it
        /// </summary>
        public IFluentConditions<T> Or(bool condition, string key = "", string message = "", string hint = "", ConditionOutput output = ConditionOutput.Error)
        {
            var fluentCondition = new FluentCondition<T>(c => condition, ConditionType.Or, key, message, hint, output);
            Conditions.Add(fluentCondition);
            return this;
        }

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean OR condition, then evaluates it
        /// </summary>
        public IFluentConditions<T> Or(Expression<Func<T, bool>> expression, string key = "", string message = "", string hint = "", ConditionOutput output = ConditionOutput.Error)
        {
            var fluentCondition = new FluentCondition<T>(expression, ConditionType.Or, key, message, hint, output);
            Conditions.Add(fluentCondition);
            return this;
        }

        /// <summary>
        ///     Clear the conditions for the validation instance
        /// </summary>
        public IFluentConditions<T> Clear()
        {
            Conditions.Clear();
            return this;
        }

        /// <summary>
        ///     Groups any conditions created that havent already been grouped
        /// </summary>
        public void Group(string name)
        {
            Conditions.Where(c => String.IsNullOrEmpty(c.GroupName)).ToList().ForEach(c =>
            {
                c.SetGroup(name);
            });
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
        public bool Satisfied(string group = "", bool suppressWarnings = true)
        {
            return Conditions
                .Where(c => c.GroupName.ToLower() == group.ToLower() || string.IsNullOrEmpty(group))
                .Where(c => (c.Output == ConditionOutput.Error) || (c.Output == ConditionOutput.Warning && !suppressWarnings))
                .All(c => _expressionBuilder.Invoke(_expressionBuilder.Compile(c.Condition), _target) && c.Type == ConditionType.And) ||
                Conditions
                .Where(c => (c.Output == ConditionOutput.Error) || (c.Output == ConditionOutput.Warning && !suppressWarnings))
                .Any(c => _expressionBuilder.Invoke(_expressionBuilder.Compile(c.Condition), _target) && c.Type == ConditionType.Or);
        }
    }
}
