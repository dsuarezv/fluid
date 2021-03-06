﻿using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Fluid.Ast;

namespace Fluid.MvcViewEngine.Statements
{
    public class RenderBodyStatement : Statement
    {
        public override async Task<Completion> WriteToAsync(TextWriter writer, TextEncoder encoder, TemplateContext context)
        {
            if (context.AmbientValues.TryGetValue("Body", out var body))
            {
                await writer.WriteAsync((string)body);
            }
            else
            {
                throw new ParseException("Could not render body, Layouts can't be evaluated directly.");
            }

            return Completion.Normal;
        }
    }
}
