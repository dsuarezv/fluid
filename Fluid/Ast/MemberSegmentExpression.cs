﻿using System.Threading.Tasks;
using Fluid.Values;

namespace Fluid.Ast
{
    public abstract class MemberSegment
    {
        public abstract Task<FluidValue> ResolveAsync(FluidValue value, TemplateContext context);
        public abstract Task<FluidValue> ResolveAsync(Scope value, TemplateContext context);
    }

    public class IdentifierSegment : MemberSegment
    {
        public IdentifierSegment(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; }

        public override Task<FluidValue> ResolveAsync(FluidValue value, TemplateContext context)
        {
            return Task.FromResult(value.GetValue(Identifier, context));
        }

        public override Task<FluidValue> ResolveAsync(Scope value, TemplateContext context)
        {
            return Task.FromResult(value.GetValue(Identifier));
        }
    }

    public class IndexerSegment : MemberSegment
    {
        public IndexerSegment(Expression expression)
        {
            Expression = expression;
        }

        public Expression Expression { get; }

        public override async Task<FluidValue> ResolveAsync(FluidValue value, TemplateContext context)
        {
            var index = await Expression.EvaluateAsync(context);
            return value.GetIndex(index, context);
        }

        public override async Task<FluidValue> ResolveAsync(Scope value, TemplateContext context)
        {
            var index = await Expression.EvaluateAsync(context);
            return value.GetIndex(index);
        }
    }
}
