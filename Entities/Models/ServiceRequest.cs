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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }
        public string buildingCode { get; set; }
        public string description { get; set; }
        public CurrentStatus currentStatus { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string lastModifiedBy { get; set; }
        public DateTime lastUpdatedBy { get; set; }
    }
}
