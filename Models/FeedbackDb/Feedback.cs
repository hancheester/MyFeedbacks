using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFeedbacks.Models.FeedbackDb
{
    [Table("Feedbacks", Schema = "dbo")]
    public partial class Feedback : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("FeedbackID")]
        public int FeedbackId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string FeedbackMessage { get; set; }

        public DateTime DateSubmitted { get; set; }
    }
}