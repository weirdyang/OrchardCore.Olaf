using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OrchardCore.Olaf.Delay
{
    public class DelayEventViewModel
    {
        [Required]
        public string DateTimeExpression { get; set; }
    }
}
