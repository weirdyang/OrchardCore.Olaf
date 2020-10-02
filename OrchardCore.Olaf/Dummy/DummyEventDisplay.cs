using OrchardCore.Workflows.Display;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrchardCore.Olaf.Dummy
{
    public class DummyEventDisplay : ActivityDisplayDriver<DummyEvent, DummyEventViewModel>
    {
        protected override void EditActivity(DummyEvent source, DummyEventViewModel model)
        {
            
        }

        protected override void UpdateActivity(DummyEventViewModel model, DummyEvent target)
        {

        }
    }
}
