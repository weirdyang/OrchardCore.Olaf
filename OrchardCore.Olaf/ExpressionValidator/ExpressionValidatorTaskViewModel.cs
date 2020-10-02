using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OrchardCore.Olaf.ExpressionValidator
{
    public class ExpressionValidatorTaskViewModel
    {
        [Required]
        public string PropertyValue { get; set; }
        [Required]
        public string PropertyType { get; set; }
        [Required]
        public string Expression { get; set; }

    }
}

