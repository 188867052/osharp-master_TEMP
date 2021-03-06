﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OSharp.Caching
{
    /// <summary>
    /// Enables the partial evaluation of queries.
    /// </summary>
    /// <remarks>
    /// From http://msdn.microsoft.com/en-us/library/bb546158.aspx
    /// Copyright notice http://msdn.microsoft.com/en-gb/cc300389.aspx#O
    /// </remarks>
    internal static class Evaluator
    {
        /// <summary>
        /// Performs evaluation replacement of independent sub-trees
        /// </summary>
        /// <param name="expression">The root of the expression tree.</param>
        /// <param name="fnCanBeEvaluated">A function that decides whether a given expression node can be part of the local function.</param>
        /// <returns>A new tree with sub-trees evaluated and replaced.</returns>
        public static Expression PartialEval(Expression expression, Func<Expression, bool> fnCanBeEvaluated)
        {
            return new SubtreeEvaluator(new Nominator(fnCanBeEvaluated).Nominate(expression)).Eval(expression);
        }

        /// <summary>
        /// Evaluates replaces sub-trees when first candidate is reached (top-down)
        /// </summary>
        private class SubtreeEvaluator : ExpressionVisitor
        {
            private readonly HashSet<Expression> _candidates;

            internal SubtreeEvaluator(HashSet<Expression> candidates)
            {
                this._candidates = candidates;
            }

            internal Expression Eval(Expression exp)
            {
                return this.Visit(exp);
            }

            // Solves the projection problem, https://github.com/loresoft/EntityFramework.Extended/issues/19
            // and https://github.com/osjoberg/LinqCache/issues/3, thank you @agnauck and @geriadejes.
            protected override Expression VisitMemberInit(MemberInitExpression node)
            {
                if (node.NewExpression.NodeType == ExpressionType.New)
                {
                    return node;
                }

                return base.VisitMemberInit(node);
            }

            public override Expression Visit(Expression exp)
            {
                if (exp == null)
                {
                    return null;
                }

                if (this._candidates.Contains(exp))
                {
                    return Evaluate(exp);
                }

                return base.Visit(exp);
            }

            private static Expression Evaluate(Expression e)
            {
                if (e.NodeType == ExpressionType.Constant)
                {
                    return e;
                }

                var lambda = Expression.Lambda(e);
                var fn = lambda.Compile();
                return Expression.Constant(fn.DynamicInvoke(null), e.Type);
            }
        }

        /// <summary>
        /// Performs bottom-up analysis to determine which nodes can possibly
        /// be part of an evaluated sub-tree.
        /// </summary>
        private class Nominator : ExpressionVisitor
        {
            private readonly Func<Expression, bool> _fnCanBeEvaluated;
            private HashSet<Expression> _candidates;
            private bool _cannotBeEvaluated;

            internal Nominator(Func<Expression, bool> fnCanBeEvaluated)
            {
                this._fnCanBeEvaluated = fnCanBeEvaluated;
            }

            internal HashSet<Expression> Nominate(Expression expression)
            {
                this._candidates = new HashSet<Expression>();
                this.Visit(expression);
                return this._candidates;
            }

            public override Expression Visit(Expression expression)
            {
                if (expression != null)
                {
                    var saveCannotBeEvaluated = this._cannotBeEvaluated;
                    this._cannotBeEvaluated = false;
                    base.Visit(expression);
                    if (!this._cannotBeEvaluated)
                    {
                        if (this._fnCanBeEvaluated(expression))
                        {
                            this._candidates.Add(expression);
                        }
                        else
                        {
                            this._cannotBeEvaluated = true;
                        }
                    }

                    this._cannotBeEvaluated |= saveCannotBeEvaluated;
                }

                return expression;
            }
        }
    }
}



