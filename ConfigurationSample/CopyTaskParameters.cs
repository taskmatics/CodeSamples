using System.ComponentModel.DataAnnotations;

namespace ConfigurationSample
{
    public class CopyTaskParameters
    {
         [Required]
         [Display(Name = "Source Copy Path", Description = "The folder that files will be copied from.")] 
         public string SourceFolderPath { get; set; }
     
         [Required]
         [Display(Name = "Destination Copy Path", Description = "The folder that files will be copied to.")]
         [RegularExpression(@"\w{1,100}")]
         public string DestinationFolderPath { get; set; }
    }
}
