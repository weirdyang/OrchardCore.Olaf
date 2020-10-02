using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using NCrontab;
using OrchardCore.Modules;
using OrchardCore.Olaf.Common;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using OrchardCore.Workflows.Services;

namespace OrchardCore.Olaf.Delay
{
    public class DelayEvent : EventActivity
    {
        public static string EventName => nameof(DelayEvent);
        private readonly IClock _clock;
        private readonly IWorkflowExpressionEvaluator _expressionEvaluator;
        
        public DelayEvent(IClock clock, IStringLocalizer<DelayEvent> localizer, IWorkflowExpressionEvaluator expressionEvaluator)
        {
            _clock = clock;
            T = localizer;
            _expressionEvaluator = expressionEvaluator;
        }

        private IStringLocalizer T { get; }

        public override string Name => EventName;
        public override LocalizedString DisplayText => T["Delay Event"];

        public override LocalizedString Category => T["Background"];

        public WorkflowExpression<string> DateTimeExpression
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }
        private DateTimeOffset? StartedUtc
        {
            get => GetProperty<DateTimeOffset?>();
            set => SetProperty(value);
        }

        /// <summary>
        /// this gets called first
        /// </summary>
        /// <param name="workflowContext"></param>
        /// <param name="activityContext"></param>
        /// <returns></returns>
        public override async Task<bool> CanExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {

            return await Task.FromResult(true);
        }

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(T["Done"]);
        }

        /// <summary>
        /// this is called if can execute returns true
        /// exceptions thrown here will put the workflow in a faulted state
        /// </summary>
        /// <param name="workflowContext"></param>
        /// <param name="activityContext"></param>
        /// <returns></returns>
        public async override Task<ActivityExecutionResult> ResumeAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            ExecutionResult check = await IsExpiredAsync(workflowContext);
            switch (check)
            {
                case ExecutionResult.CanExecute:
                    return Outcomes("Done");
                case ExecutionResult.Halted:
                    return Halt();

            }
            return Halt();
        }
        private async Task<ExecutionResult> IsExpiredAsync(WorkflowExecutionContext workflowContext)
        {
            string dateExpression = await _expressionEvaluator.EvaluateAsync(DateTimeExpression, workflowContext, null);
            DateTimeOffset target;

            target = DateTimeUtils.ParseDateString(dateExpression);

            DateTimeOffset timeEnd = DateTimeOffset.UtcNow;

            if (timeEnd >= target)
            {
                workflowContext.LastResult = $"{nameof(DelayEvent)} executed at: {timeEnd}";
                return ExecutionResult.CanExecute;
            }
            return ExecutionResult.Halted;

        }

    }
    public enum ExecutionResult
    {
        CanExecute,
        Faulted,
        Halted

    }
}
