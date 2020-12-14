using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    public partial class Machines
    {
        public int Id { get; set; }
        public string MachineName { get; set; }
        public string MachineCode { get; set; }
        public int? Factoryfid { get; set; }
        public int? Typefid { get; set; }
        public int? Terminalfid { get; set; }
        public int? Andonfid { get; set; }
        public bool? Ispassive { get; set; }
        public DateTime? Createddate { get; set; }
        public int? Createdby { get; set; }
        public int? Modifiedby { get; set; }
        public DateTime? Modifieddate { get; set; }
        public int? Panono { get; set; }
        public string Panoip { get; set; }
        public int? Inputno { get; set; }
        public int? SignalType { get; set; }
        public int? Groupfid { get; set; }
        public int? Enginnerfid { get; set; }
        public int? Erpfid { get; set; }
        public bool? UseSerial { get; set; }
        public bool? UsePalet { get; set; }
        public byte? ProductScreen { get; set; }
        public bool? IsReadFromOpc { get; set; }
        public string TerminalCode { get; set; }
        public string MachineIp { get; set; }
        public int? OperationModeId { get; set; }
        public int? WebPageId { get; set; }
        public bool? IsStockBeNegative { get; set; }
        public bool? UsePalletBarcode { get; set; }
        public bool? IsUseMaterialDialogBox { get; set; }
        public bool? IsUseDepartmentForDowntime { get; set; }
        public bool? UseProductionMultiplier { get; set; }
        public int? MudurlukId { get; set; }
        public int? TakimLiderligiId { get; set; }
        public int? MasrafYeriId { get; set; }
        public int? MachineSignalTypeId { get; set; }
        public int? MachineProductivityCalculationMethodId { get; set; }
        public bool? IsActiveForProductivityCalculations { get; set; }
        public string MachineType { get; set; }
        public bool? RuloFlag { get; set; }
    }
}
