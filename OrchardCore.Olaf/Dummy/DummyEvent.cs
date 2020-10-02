using System;
using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using NCrontab;
using OrchardCore.Modules;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;

namespace OrchardCore.Olaf.Dummy
{
    public class DummyEvent : EventActivity
    {
        public static string EventName => nameof(DummyEvent);

        public DummyEvent(IStringLocalizer<DummyEvent> localizer)
        {
            T = localizer;
        }

        private IStringLocalizer T { get; }

        public override string Name => EventName;
        public override LocalizedString DisplayText => T["Dummy Event"];

        public override LocalizedString Category => T["Background"];


        public override bool CanExecute(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return true;
        }

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(T["Done"]);
        }

        public override ActivityExecutionResult Resume(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            workflowContext.LastResult = "DummyEvent";
            return Outcomes("Done");
        }

    }
}