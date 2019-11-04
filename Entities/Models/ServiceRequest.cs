using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public enum CurrentStatus
    {
        NotApplicable,
        Created,
        InProgress,
        Complete,
        Canceled
    }

    public class ServiceRequest
    {
        public Guid id { get; set; }
        [Required]
        public string buildingCode { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public CurrentStatus currentStatus { get; set; }
        [Required]
        public string createdBy { get; set; }
        [Required]
        public DateTime createdDate { get; set; }
        [Required]
        public string lastModifiedBy { get; set; }
        [Required]
        public DateTime lastUpdatedBy { get; set; }
    }
}
