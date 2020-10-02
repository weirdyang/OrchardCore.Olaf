using OrchardCore.Workflows.Display;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrchardCore.Olaf.ExpressionValidator
{
    public class ExpressionValidatorTaskDisplay : ActivityDisplayDriver<ExpressionValidator.ExpressionValidatorTask, ExpressionValidator.ExpressionValidatorTaskViewModel>
    {
        protected override void EditActivity(
            ExpressionValidator.ExpressionValidatorTask source,
            ExpressionValidator.ExpressionValidatorTaskViewModel model)
        {
            model.Expression = source.ValidationExpression.Expression;
            model.PropertyType = source.PropertyType;
            model.PropertyValue = source.PropertyValue.Expression;
        }

        protected override void UpdateActivity(
            ExpressionValidator.ExpressionValidatorTaskViewModel model,
            ExpressionValidator.ExpressionValidatorTask target)
        {
            target.ValidationExpression = new Workflows.Models.WorkflowExpression<string>(model.Expression);
            target.PropertyValue = new Workflows.Models.WorkflowExpression<string>(model.PropertyValue);
            target.PropertyType = model.PropertyType;
        }
    }
}
