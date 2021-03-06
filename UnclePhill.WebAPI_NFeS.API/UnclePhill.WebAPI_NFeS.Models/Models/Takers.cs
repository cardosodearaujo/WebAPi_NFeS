﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnclePhill.WebAPI_NFeS.Models.Models
{
    public class Takers : DefaultModels.Master
    {
        public Takers()
        {

        }

        public Takers(long TakerId, long CompanyId, string IM, 
            string CPF_CNPJ, string RG_IE, 
            string Name, string NameFantasy, 
            string TypePerson, string CEP, 
            string Street, string Neighborhood, 
            string City, string State, string Telephone,
            string Email, bool Active, 
            string DateInsert, string DateUpdate)
        {
            this.TakerId = TakerId;
            this.CompanyId = CompanyId;
            this.IM = IM;
            this.CPF_CNPJ = CPF_CNPJ;
            this.RG_IE = RG_IE;
            this.Name = Name;
            this.NameFantasy = NameFantasy;
            this.TypePerson = TypePerson;
            this.CEP = CEP;
            this.Street = Street;
            this.Number = Number;
            this.Neighborhood = Neighborhood;
            this.City = City;
            this.State = State;
            this.Telephone = Telephone;
            this.Email = Email;
            this.Active = Active;
            this.DateInsert = DateInsert;
            this.DateUpdate = DateUpdate;
        }
                
        public long TakerId { get; set; }
        public long CompanyId { get; set; }
        public string IM { get; set; }
        public string CPF_CNPJ { get; set; }
        public string RG_IE { get; set; }
        public string Name { get; set; }
        public string NameFantasy { get; set; }
        public string TypePerson { get; set; }
        public string CEP { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string DateInsert { get; set; }
        public string DateUpdate { get; set; }
    }
}