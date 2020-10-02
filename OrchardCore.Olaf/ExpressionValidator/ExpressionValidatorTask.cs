using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.Olaf.ExpressionValidator.Services;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using OrchardCore.Workflows.Services;

namespace OrchardCore.Olaf.ExpressionValidator
{
    public class ExpressionValidatorTask : TaskActivity
    {
        public static string TaskName => nameof(ExpressionValidatorTask);

        public ExpressionValidatorTask(IStringLocalizer<ExpressionValidatorTask> localizer,ValidationService validation,  IWorkflowExpressionEvaluator expressionEvaluator)
        {
            T = localizer;
            _expressionEvaluator = expressionEvaluator;
            _validation = validation;
        }

        private IStringLocalizer T { get; }
        private readonly IWorkflowExpressionEvaluator _expressionEvaluator;
        private readonly ValidationService _validation;
        public override string Name => TaskName;
        public override LocalizedString DisplayText => T["Expression Validator"];

        public override LocalizedString Category => T["Validation"];
        public WorkflowExpression<string> PropertyValue
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }
        public string PropertyType
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public WorkflowExpression<string> ValidationExpression
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }
        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(T["Done"], T["Invalid"], T["Valid"]);
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            var propertyValue = await _expressionEvaluator.EvaluateAsync(PropertyValue, workflowContext, null);
            var expressionValue = await _expressionEvaluator.EvaluateAsync(ValidationExpression, workflowContext, null);
            bool isValid = await  _validation.ValidateAsync(this.PropertyType, propertyValue, expressionValue);

            var outcome = isValid ? "Valid" : "Invalid";
            workflowContext.LastResult = $"{nameof(ExpressionValidatorTask)}: {outcome} for {propertyValue} - {expressionValue}";
            return Outcomes("Done", outcome);
        }
    }
}
