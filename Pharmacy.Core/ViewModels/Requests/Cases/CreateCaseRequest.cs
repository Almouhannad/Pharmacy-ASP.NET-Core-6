﻿using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.SelectLists;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Requests.Cases
{
    public class CreateCaseRequest : CreateBase<Case>
    {
        [Required (ErrorMessage = "Patient is required")]
        [Display (Name = "Patient")]
        public int PatientId { get; set; }
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\s,-.!?;:\/]*$", ErrorMessage = "Name must contain English letters only.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }
        public SelectPatientForCase? PatientSelectList { get; set; }

        #region Model to view
        public override CreateCaseRequest GetRequest()
        {
            var request = new CreateCaseRequest
            {
                PatientSelectList = new SelectPatientForCase()
            };
            return request;
        }
        #endregion

        #region View to model
        public override Case GetModel()
        {
            var model = base.GetModel();
            model.PatientId = PatientId;
            model.Name = Name;
            return model;
        }
        #endregion



    }
}
