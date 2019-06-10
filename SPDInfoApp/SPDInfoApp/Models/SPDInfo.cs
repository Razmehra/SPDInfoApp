using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SPDInfoApp.Models
{
    public class SPDInfo
    {
        [JsonProperty("Appid")]
        public double Appid { get; set; }
        [JsonProperty("AppearingClass")]
        public string AppearingClass { get; set; }
        [JsonProperty("IsPG")]
        public bool IsPG { get; set; }
        [JsonProperty("YearSemester")]
        public int YearSemester { get; set; }
        [JsonProperty("AddmissionDate1")]
        public DateTime AddmissionDate1 { get; set; }
        [JsonProperty("AddmissionDate2")]
        public DateTime AddmissionDate2 { get; set; }
        [JsonProperty("AddmissionDate3")]
        public DateTime AddmissionDate3 { get; set; }
        [JsonProperty("ApplicationID")]
        public Nullable<double> ApplicationID { get; set; }
        [JsonProperty("StudentFName")]
        public string StudentFName { get; set; }
        [JsonProperty("StudentMName")]
        public string StudentMName { get; set; }
        [JsonProperty("StudentLName")]
        public string StudentLName { get; set; }
        [JsonProperty("RollNo")]
        public string RollNo { get; set; }
        [JsonProperty("EnrolmentNo")]
        public string EnrolmentNo { get; set; }
        [JsonProperty("DOB")]
        public DateTime DOB { get; set; }
        [JsonProperty("Medium")]
        public string Medium { get; set; }
        [JsonProperty("Gender")]
        public string Gender { get; set; }
        [JsonProperty("Category")]
        public string Category { get; set; }
        [JsonProperty("RegCastCertificate")]
        public string RegCastCertificate { get; set; }
        [JsonProperty("IsMinority")]
        public bool IsMinority { get; set; }
        [JsonProperty("Minority")]
        public string Minority { get; set; }
        [JsonProperty("IsHandicapped")]
        public bool IsHandicapped { get; set; }
        [JsonProperty("HandicapType")]
        public string HandicapType { get; set; }
        [JsonProperty("HandicappedOtherDetail")]
        public string HandicappedOtherDetail { get; set; }
        [JsonProperty("HandicappPercent")]
        public string HandicappPercent { get; set; }
        [JsonProperty("HandicappDetail")]
        public string HandicappDetail { get; set; }
        [JsonProperty("HandicappUDID")]
        public string HandicappUDID { get; set; }
        [JsonProperty("BloodGroup")]
        public string BloodGroup { get; set; }
        [JsonProperty("PhoneMobile")]
        public string PhoneMobile { get; set; }
        [JsonProperty("AadharNo")]
        public string AadharNo { get; set; }
        [JsonProperty("EMail")]
        public string EMail { get; set; }
        [JsonProperty("AddressPermanent")]
        public string AddressPermanent { get; set; }
        [JsonProperty("AddressCurrent")]
        public string AddressCurrent { get; set; }
        [JsonProperty("IsUrban")]
        public bool IsUrban { get; set; }
        [JsonProperty("Domicile")]
        public string Domicile { get; set; }
        [JsonProperty("SSSMID")]
        public string SSSMID { get; set; }
        [JsonProperty("RegDomicileCertificateNo")]
        public string RegDomicileCertificateNo { get; set; }
        [JsonProperty("FHName")]
        public string FHName { get; set; }
        [JsonProperty("MotherName")]
        public string MotherName { get; set; }
        [JsonProperty("PhoneMobile_Gaurdian")]
        public string PhoneMobile_Gaurdian { get; set; }
        [JsonProperty("IncomeFather")]
        public Nullable<double> IncomeFather { get; set; }
        [JsonProperty("OccupationFather")]
        public string OccupationFather { get; set; }
        [JsonProperty("BankAcNo")]
        public string BankAcNo { get; set; }
        [JsonProperty("BankIFSC")]
        public string BankIFSC { get; set; }
        [JsonProperty("BankName")]
        public string BankName { get; set; }
        [JsonProperty("BankBranch")]
        public string BankBranch { get; set; }
        [JsonProperty("VoterID")]
        public string VoterID { get; set; }
        [JsonProperty("PANNo")]
        public string PANNo { get; set; }
        [JsonProperty("DrivingLicNo")]
        public string DrivingLicNo { get; set; }
        [JsonProperty("ScholershipName")]
        public string ScholershipName { get; set; }
        [JsonProperty("FamilySSSMID")]
        public string FamilySSSMID { get; set; }
        [JsonProperty("IsNCC")]
        public bool IsNCC { get; set; }
        [JsonProperty("CertNCC")]
        public string CertNCC { get; set; }
        [JsonProperty("CampNCC")]
        public string CampNCC { get; set; }
        [JsonProperty("NCCCampOtherDetail")]
        public string NCCCampOtherDetail { get; set; }
        [JsonProperty("IsNSS")]
        public bool IsNSS { get; set; }
        [JsonProperty("CertNSS")]
        public string CertNSS { get; set; }
        [JsonProperty("IsScoutGuide")]
        public bool IsScoutGuide { get; set; }
        [JsonProperty("IsSports")]
        public bool IsSports { get; set; }
        [JsonProperty("CertSports")]
        public string CertSports { get; set; }
        [JsonProperty("SportsOtherDetail")]
        public string SportsOtherDetail { get; set; }
        [JsonProperty("PhotoPath")]
        public string PhotoPath { get; set; }
        [JsonProperty("PhotoName")]
        public string PhotoName { get; set; }
        [JsonProperty("EntryDate")]
        public DateTime EntryDate { get; set; }
        [JsonProperty("UpdateDate")]
        [Description("Last Update on")]
        public DateTime UpdateDate { get; set; }


    }
}
