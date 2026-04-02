using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BambooEnterprise.Models
{
    public enum GrowthHabit { Monopodial, Sympodial }

    // Required for the ExchangeRate-API integration
    public enum Currency { SEK, EUR, USD, GBP }

    [JsonDerivedType(typeof(Monopodial), typeDiscriminator: "monopodial")]
    [JsonDerivedType(typeof(Sympodial), typeDiscriminator: "sympodial")]
    public abstract class BambooSpecies
    {
        public string PlantUID { get; set; }
        public string Genus { get; set; }
        public string Species { get; set; }
        public string Cultivar { get; set; }
        public GrowthHabit Habit { get; set; }
        public double HeightLow { get; set; }
        public double HeightHigh { get; set; }
        public double DiameterLow { get; set; }
        public double DiameterHigh { get; set; }
        public double HardinessLow { get; set; }
        public int TypicalShootingMonth { get; set; }
        public string SverigeSpecifikt { get; set; }
        public int ShootEdibilityRank { get; set; }
        public int TimberQualityRank { get; set; }
    }

    public class Monopodial : BambooSpecies { public double RhizomeDepth { get; set; } }
    public class Sympodial : BambooSpecies { public double ClumpSpacing { get; set; } }

    public class InventoryBatch
    {
        public string BatchID { get; set; }
        public string PlantUID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Nursery { get; set; }
        public double PotVolume { get; set; }
        public int Quantity { get; set; }
        public double UnitCost { get; set; }
        public Currency Curr { get; set; }
        public double ExchangeRateToSek { get; set; }

        // Calculated Property
        public double TotalCostSek => (UnitCost * Quantity) * ExchangeRateToSek;

        // Audit Trail Fields
        public string UpdateReason { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public bool IsPeakCulmAge(int currentYear) => (currentYear - PurchaseDate.Year) >= 4;
    }

    public class PerformanceLog
    {
        public string LogID { get; set; }
        public string BatchID { get; set; }
        public DateTime Date { get; set; }
        public int NewShootCount { get; set; }
        public double MaxHeightCurrent { get; set; }
        public double CulmRevenueSek { get; set; }
        public double ShootRevenueSek { get; set; }
    }

    public class OperationalExpense
    {
        public string ExpenseID { get; set; }
        public string BatchID { get; set; }
        public DateTime Date { get; set; }
        public double MaintenanceCost { get; set; }
        public double EquipmentCost { get; set; }
        public double LaborCost { get; set; }
        public double TransportCost { get; set; }
    }
}
