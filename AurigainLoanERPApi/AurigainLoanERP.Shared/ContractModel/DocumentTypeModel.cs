﻿using System;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class DocumentTypeModel
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool IsNumeric { get; set; }
        public bool IsKyc { get; set; }
        public int RequiredFileCount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? DocumentNumberLength { get; set; }

    }
    /// <summary>
    /// Document Type Dropdown Model
    /// </summary>
    public class DDLDocumentTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsNumeric { get; set; }
        public int? DocumentNumberLength { get; set; }
        public bool IsKyc { get; set; }
        public int RequiredFileCount { get; set; }
    }


    public class UserDocumentFilesViewModel
    {
        public long Id { get; set; }
        public long DocumentId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Path { get; set; }



    }



    //public class FilePostModel
    //{
    //    public long Id { get; set; }
    //    public long DocumentId { get; set; }
    //    public string FileName { get; set; }
    //    public string File { get; set; }
    //    public string FileType { get; set; }
    //    /// <summary>
    //    /// If Is Edit =true then Update file in database Document not blank, if ie edit = true and Document is blank then remove file
    //    /// </summary>
    //    public bool IsEditMode { get; set; }

    //}
}
