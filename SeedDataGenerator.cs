using BambooEnterprise.Models;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2013.Word;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using iText.StyledXmlParser.Jsoup.Nodes;
using iText.StyledXmlParser.Jsoup.Select;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;
namespace BambooEnterprise.Testing
{
    public static class SeedDataGenerator
    {
        public static List<InventoryBatch> GetSampleInventoryBatches()
        {
            return new List<InventoryBatch>
            {
                new InventoryBatch {
                    BatchID = "20220611-KIM-001",
                    PlantUID = "PHY-VIV-AUR",
                    PurchaseDate = new DateTime(2022, 06, 11),
                    Nursery = "Kimmei",
                    PotVolume = 10,
                    Quantity = 2,
                    UnitCost = 45.00,
                    Curr = Currency.EUR,
                    ExchangeRateToSek = 11.20,
                    UpdateReason = "Initial Seed Data",
                    LastUpdatedDate = DateTime.Now
                },
                new InventoryBatch {
                    BatchID = "20240315-BAM-002",
                    PlantUID = "FAR-NIT-OBE",
                    PurchaseDate = new DateTime(2024, 03, 15),
                    Nursery = "BambooGardens",
                    PotVolume = 5,
                    Quantity = 5,
                    UnitCost = 250.00,
                    Curr = Currency.SEK,
                    ExchangeRateToSek = 1.0,
                    UpdateReason = "Initial Seed Data",
                    LastUpdatedDate = DateTime.Now
                }
            };
        }
        public static List<BambooSpecies> GetResearchedSpecies()
        {
            var list = new List<BambooSpecies>();
            // Runners
            list.Add(CreateMono("PHY-VIV-HUA", "Phyllostachys", "vivax", "Huanwenzhu", 12, 10, -21, 6, "Green culm, yellow sulcus.Snöbrott risk."));
            list.Add(CreateMono("PHY-VIV-MCC", "Phyllostachys", "vivax", "McClure", 14, 12, -21, 6, "Classic green timber. Large leaves."));
            list.Add(CreateMono("PHY-VIV-AUR", "Phyllostachys", "vivax", "Aureocaulis", 12, 11, -21, 6, "Yellow culm, green stripes. High aesthetics."));
            list.Add(CreateMono("PHY-PAR-NOM", "Phyllostachys", "parvifolia", "", 10, 9, -25, 5, "Extremely hardy timber. Edible shoots."));
            list.Add(CreateMono("PHY-ATR-INC", "Phyllostachys","atrovaginata", "", 8, 7, -23, 5, "Air canals for wet soil. Scented."));
            list.Add(CreateMono("PHY-AUR-NOM", "Phyllostachys", "aureosulcata", "", 7, 4, -25, 5, "Green culm, yellow sulcus.Zig - zag bases."));
            list.Add(CreateMono("PHY-AUR-AUR", "Phyllostachys", "aureosulcata", "Aureocaulis", 7, 4, -25, 5, "Solid yellow. Very hardy."));
            list.Add(CreateMono("PHY-AUR-SPE", "Phyllostachys", "aureosulcata", "Spectabilis", 7, 4, -25, 5, "Yellow culm, green sulcus.Red shoots."));
            list.Add(CreateMono("PHY-AUR-ARG", "Phyllostachys", "aureosulcata", "Argus", 7, 4, -25, 5, "Green stripes on yellow."));
            list.Add(CreateMono("PHY-NIG-NOM", "Phyllostachys", "nigra", "", 5, 3, -18, 5, "Jet black culms. Needs shelter."));
            list.Add(CreateMono("PHY-NIG-HEN", "Phyllostachys", "nigra", "Henonis", 10, 8, -23, 6, "Giant grey timber. Very hardy."));
            list.Add(CreateMono("PHY-NIG-PUN", "Phyllostachys", "nigra", "Punctata", 7, 4, -20, 6, "Speckled black culms."));
            list.Add(CreateMono("PHY-BIS-NOM", "Phyllostachys", "bissetii", "", 7, 4, -26, 4, "Best Swedish windbreak."));
            list.Add(CreateMono("PHY-DEC-NOM", "Phyllostachys", "decora", "", 6, 4, -22, 5, "Drought tolerant runner."));
            list.Add(CreateMono("PHY-NUD-NOM", "Phyllostachys", "nuda", "", 6, 4, -26, 4, "Early purple / black nodes."));
            list.Add(CreateMono("SAS-KUR-NOM", "Sasa", "kurilensis", "", 2, 1.5, -30, 6, "Northernmost species. Large leaves."));
            list.Add(CreateMono("BAS-QIN-NOM", "Bashania","qingchengshanensis", "", 4, 2, -20, 4, "Strong, stiff culms."));
            list.Add(CreateMono("SEM-FAS-NOM", "Semiarundinaria", "fastuosa", "", 8, 4, -20, 6, "Upright columnar habit."));
            // Clumpers
            list.Add(CreateSym("FAR-NIT-OBE", "Fargesia", "nitida x murieliae", "Obelisk", 4, -25, 5, "Tallest upright columnar hedge."));
            list.Add(CreateSym("FAR-MUR-RZB", "Fargesia", "murieliae", "Red Zebra", 3, -23, 5, "Dark red / green contrast stems."));
            list.Add(CreateSym("FAR-NIT-SCH", "Fargesia", "nitida x murieliae", "Schensbossen", 4, -26, 5, "Exceptional hardiness."));
            list.Add(CreateSym("FAR-SPP-RUF", "Fargesia", "sp.", "Rufa", 2.5, -23, 4, "Non-curling leaves. Early shoots."));
            list.Add(CreateSym("FAR-ROB-CAM", "Fargesia", "robusta","Campbell", 5, -20, 4, "Earliest shoots. White pattern."));
            list.Add(CreateSym("FAR-NIT-JUR", "Fargesia", "nitida", "Jürgen", 3.5, -26, 5, "Upright, dark culms."));
            list.Add(CreateSym("FAR-JIU-ONE", "Fargesia", "sp. Jiuzhaigou", "1", 3, -25, 5, "Red Panda.Cherry stems in sun."));
            list.Add(CreateSym("FAR-JIU-DPU", "Fargesia", "sp. Jiuzhaigou", "Deep Purple", 3, -25, 5, "Deepest purple culm color."));
            list.Add(CreateSym("FAR-DEM-GER", "Fargesia", "demissa", "Gerry", 3, -25, 5, "Almost black culms."));
            list.Add(CreateSym("FAR-DEN-XI2", "Fargesia", "denudata", "Xian 2", 4, -22, 5, "Cloud - like elegant habit."));
            list.Add(CreateSym("FAR-SPP-KR5", "Fargesia", "sp.", "KR5287", 4, -20, 6, "Rare collector blueish culms."));
return list;
        }
        private static Monopodial CreateMono(string uid, string g, string
        s, string c, double h, double d, double hard, int shoot, string
        note)
        => new Monopodial
        {
            PlantUID = uid,
            Genus = g,
            Species = s,
            Cultivar = c,
            Habit = GrowthHabit.Monopodial,
            HeightHigh =
        h,
            DiameterHigh = d,
            HardinessLow = hard,
            TypicalShootingMonth = shoot,
            SverigeSpecifikt = note,
            ShootEdibilityRank = 4,
            HeightLow = h * 0.7,
            DiameterLow = d * 0.6
        };
        private static Sympodial CreateSym(string uid, string g, string s,
        string c, double h, double hard, int shoot, string note)
        => new Sympodial
        {
            PlantUID = uid,
            Genus = g,
            Species = s,
            Cultivar = c,
            Habit = GrowthHabit.Sympodial,
            HeightHigh = h,
            HardinessLow = hard,
            TypicalShootingMonth = shoot,
            SverigeSpecifikt = note,
            ClumpSpacing = 1.0,
            HeightLow = h * 0.7
        };
    }
}