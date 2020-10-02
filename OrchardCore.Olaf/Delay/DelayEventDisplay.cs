using OrchardCore.Workflows.Display;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrchardCore.Olaf.Delay
{
    public class DelayEventDisplay : ActivityDisplayDriver<DelayEvent, DelayEventViewModel>
    {
        protected override void EditActivity(DelayEvent source, DelayEventViewModel model)
        {
            model.DateTimeExpression = source.DateTimeExpression.Expression;
        }

        protected override void UpdateActivity(DelayEventViewModel model, DelayEvent target)
        {
            target.DateTimeExpression = new Workflows.Models.WorkflowExpression<string>(model.DateTimeExpression);
        }
    }
}
