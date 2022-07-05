using System.ComponentModel.DataAnnotations;

namespace Bank_Apis.Model
{
    public class Branch

    {
        [Key]
        public string BranchId { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string BranchName { get; set; } = null!;

        [MaxLength(100)]
        public string Address { get; set; } = null;

        public string Description { get; set; } = null;

        [Required]
        public string UserId { get; set; } = null!;

        [MaxLength(30)]
        public string AccountId { get; set; } = null;


    }
}
