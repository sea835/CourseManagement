using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Enums
{
    public enum CourseLevel
    {
        [Display(Name = "Beginner")]
        Beginner = 0,

        [Display(Name = "Intermediate")]
        Intermediate = 1,

        [Display(Name = "Advanced")]
        Advanced = 2
    }
}