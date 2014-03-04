using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace AzureRaceDataWebAPI.Models
{
    /// <summary>
    /// This is a meeting resource
    /// </summary>
    [Table("Meeting")]
    public class Meeting
    {
        public Meeting()
        {
            this.RaceStarts = new HashSet<RaceStartItem>();
            this.Coverages = new HashSet<CoverageItem>();
            //this.Races = new List<Race>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string VenueName { get; set; }
        [Required]
        public char RGB { get; set; }
        [Required]
        public int NumberOfRaces { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(10)]
        public string TabcorpReqCode { get; set; }
        [Required]
        [StringLength(10)]
        public string TabcorpDispCode { get; set; }
        [Required]
        [StringLength(10)]
        public string TabLtdReqCode { get; set; }
        [Required]
        [StringLength(10)]
        public string TabLtdReqDispCode { get; set; }
        [Required]
        [StringLength(10)]
        public string UniTABReqCode { get; set; }
        [Required]
        [StringLength(10)]
        public string UniTABReqDispCode { get; set; }
        public ICollection<RaceStartItem> RaceStarts { get; set; }
        public ICollection<CoverageItem> Coverages { get; set; }
        //public List<Race> Races { get; set; }
    }

    /// <summary>
    /// This is a string record resource 
    /// </summary>
    [Table("CoverageItem")]
    public class CoverageItem
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public String String { get; set; }

        // Foreign key 
        public int MeetingID { get; set; }
        // Navigation properties 
        public virtual Meeting Meeting { get; set; }
    }

    /// <summary>
    /// This is a JumptimeUpdate resource 
    /// </summary>
    [Table("RaceStartItem")]
    public class RaceStartItem
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        // Foreign key 
        public int MeetingID { get; set; }
        // Navigation properties 
        public virtual Meeting Meeting { get; set; }
    }

    /// <summary>
    /// This is a reace resource
    /// </summary>
    [Table("Race")]
    public class Race
    {
        public Race()
        {
            this.Runners = new List<Runner>();
            this.Pools = new List<Pool>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string RaceCode { get; set; }
        [Required]
        public string VenueName { get; set; }
        [Required]
        public int RaceNumber { get; set; }
        [Required]
        public string RaceTitle { get; set; }
        [Required]
        public DateTime RaceDate { get; set; }
        [Required]
        public string RaceDistance { get; set; }
        [Required]
        public string TrackCondition { get; set; }
        [Required]
        public string WatherCondition { get; set; }
        [Required]
        public string RaceStatus { get; set; }
        [Required]
        public DateTime ActualJumpTime { get; set; }
        public JumpTimeHistory HistoricJumpTime { get; set; }
        public List<Runner> Runners { get; set; }
        public List<Pool> Pools { get; set; }

        // Foreign key 
        public int MeetingID { get; set; }
        // Navigation properties 
        public virtual Meeting Meeting { get; set; }
    }

    /// <summary>
    /// This is a resource representing the jump time history
    /// </summary>
    [Table("JumpTimeHistory")]
    public class JumpTimeHistory
    {
        public JumpTimeHistory()
        {
            this.JumptimeUpdates = new HashSet<DateTimeRecord>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NrOfUpdates { get; set; }
        public int NrOfRunners { get; set; }
        public ICollection<DateTimeRecord> JumptimeUpdates { get; set; }
    }

    /// <summary>
    /// This is a JumptimeUpdate resource 
    /// </summary>
    [Table("DateTimeRecord")]
    public class DateTimeRecord
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        // Foreign key 
        public int JumpTimeHistoryID { get; set; }
        // Navigation properties 
        public virtual JumpTimeHistory JumpTimeHistory { get; set; }
    }

    /// <summary>
    /// This is a runner resource
    /// </summary>
    [Table("Runner")]
    public class Runner
    {
        public Runner()
        {
            this.ApproxDividends = new HashSet<ApproxDividend>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RunnerNo { get; set; }
        public bool Scratched { get; set; }
        public string RunnerName { get; set; }
        public string Jockey { get; set; }
        public string Trainer { get; set; }
        public string Owner { get; set; }
        public int Barrier { get; set; }
        public float Weight { get; set; }
        public ICollection<ApproxDividend> ApproxDividends { get; set; }

        // Foreign key 
        public int RaceID { get; set; }
        // Navigation properties 
        public virtual Race Race { get; set; }
    }

    /// <summary>
    /// This is a ApproxDividend resource
    /// </summary>
    [Table("ApproxDividend")]
    public class ApproxDividend
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public float Dividend { get; set; }
    }

    /// <summary>
    /// This is a pool resource
    /// </summary>
    [Table("Pool")]
    public class Pool
    {
        public Pool()
        {
            this.Amounts = new HashSet<PoolAmount>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public ICollection<PoolAmount> Amounts { get; set; }

        // Foreign key 
        public int RaceID { get; set; }
        // Navigation properties 
        public virtual Race Race { get; set; }
    }

    /// <summary>
    /// This is a pool amount resource
    /// </summary>
    [Table("PoolAmount")]
    public class PoolAmount
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double Amount { get; set; }
    }
}