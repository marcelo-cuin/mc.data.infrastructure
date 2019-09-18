using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using connector.infrastructure.Interfaces;

namespace connector.infrastructure
{
    public class EntityBase : IEntity
    {
        [Key]
        [Required]
        [DataType("System.Guid")]
        [Column("recid")]
        public Guid RecId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        [NotMapped]
        public DateTime TimestampUTC
        {
            get
            {
                return this.Timestamp.ToLocalTime().ToUniversalTime();
            }
        }

        public EntityBase()
        {
            this.RecId = Guid.NewGuid();
            this.Timestamp = DateTime.UtcNow;
        }
    }
}
