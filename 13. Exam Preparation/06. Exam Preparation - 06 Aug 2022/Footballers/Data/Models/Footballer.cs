﻿using System.ComponentModel.DataAnnotations;
using Footballers.Data.Models.Enums;
using static Footballers.DataConstraints;

namespace Footballers.Data.Models
{
    public class Footballer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(FootballerNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime ContractStartDate { get; set; }

        [Required]
        public DateTime ContractEndDate { get; set; }

        [Required]
        public PositionType PositionType { get; set; }

        [Required]
        public BestSkillType BestSkillType { get; set; }

        [Required]
        public int CoachId { get; set; }

        [Required]
        public virtual Coach Coach { get; set; } = null!;

        public virtual ICollection<TeamFootballer> TeamsFootballers { get; set; } = new HashSet<TeamFootballer>();
    }
}
