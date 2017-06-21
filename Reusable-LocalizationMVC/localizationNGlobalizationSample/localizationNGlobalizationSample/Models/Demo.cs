using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace localizationNGlobalizationSample.Models
{
    public class Demo
    {
        [Display(Name ="Name", ResourceType = typeof(MyResources.Resources))]
        [Required(ErrorMessageResourceName = "NameRequiredError", ErrorMessageResourceType = typeof(MyResources.Resources))]
        public string Name { get; set; }

        [Display(Name = "Age", ResourceType = typeof(MyResources.Resources))]
        [Required(ErrorMessageResourceName = "AgeRequiredError", ErrorMessageResourceType = typeof(MyResources.Resources))]
        public int Age { get; set; }
    }
}