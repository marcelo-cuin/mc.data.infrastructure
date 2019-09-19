using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using mc.data.infrastructure;

namespace mc.data.infrastructure
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
        [Column("createdon")]
        public DateTime CreatedOn { get; set; }

        public EntityBase()
        {
            this.RecId = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}
