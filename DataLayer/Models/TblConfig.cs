﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataLayer.Models
{
    [Table("TblConfig", Schema = "dbo")]
    public partial class TblConfig
    {
        [Key]
        [StringLength(100)]
        public string Keyword { get; set; }
        public string Value { get; set; }
    }
}
