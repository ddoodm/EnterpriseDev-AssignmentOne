﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ENETCare.IMS.Users;

namespace ENETCare.IMS.Interventions
{
    public class Interventions
    {
        private static List<Intervention> interventions = new List<Intervention>();

        public Interventions()
        {
            interventions = new List<Intervention>();

            SetUpData();
        }

        public void SetUpData()
        {
            // Test engineer
            SiteEngineer testEngineerDistrict0 = new SiteEngineer
                ("Bill Williams", "williams.bill", "abc1234",
                Clients.GetClientByID(0).District, 12, 1000);

            SiteEngineer testEngineerDistrict1 = new SiteEngineer
                ("Ted Edwardson", "edwardson.ted", "abc1234",
                Clients.GetClientByID(1).District, 12, 1000);

            SiteEngineer testEngineerDistrict2 = new SiteEngineer
                ("Richard Dickson", "dickson.richard", "abc1234",
                Clients.GetClientByID(2).District, 12, 1000);

            SiteEngineer testEngineerDistrict3 = new SiteEngineer
                ("James \"Jimmy\" Jameson", "jameson.jim", "abc1234",
                Clients.GetClientByID(3).District, 12, 1000);

            SiteEngineer testEngineerDistrict4 = new SiteEngineer
                ("Rupert von Ochtag Gon", "octhaggon.rupert", "abc1234",
                Clients.GetClientByID(4).District, 12, 1000);

            SiteEngineer testEngineerDistrict5  = new SiteEngineer
                ("Dr. Byron Orpheus", "orpheus.byron", "abc1234",
                Clients.GetClientByID(5).District, 12, 1000);

            // Populate interventions with fake data
            InterventionTypes types = new InterventionTypes();

            interventions.Add(Intervention.Factory.CreateIntervention(
                0, types[0], Clients.GetClientByID(0), testEngineerDistrict0)); //0
            interventions.Add(Intervention.Factory.CreateIntervention(
                1, types[1], Clients.GetClientByID(4), testEngineerDistrict4)); //1
            interventions.Add(Intervention.Factory.CreateIntervention(
                2, types[0], Clients.GetClientByID(3), testEngineerDistrict3)); //2
            interventions.Add(Intervention.Factory.CreateIntervention(
                2, types[2], Clients.GetClientByID(2), testEngineerDistrict2)); //3
            interventions.Add(Intervention.Factory.CreateIntervention(
                2, types[1], Clients.GetClientByID(0), testEngineerDistrict0)); //4
            interventions.Add(Intervention.Factory.CreateIntervention(
                2, types[0], Clients.GetClientByID(1), testEngineerDistrict1)); //5

            interventions.Add(Intervention.Factory.CreateIntervention(
                0, types[0], Clients.GetClientByID(5), testEngineerDistrict5)); //6
            interventions.Add(Intervention.Factory.CreateIntervention(
                1, types[1], Clients.GetClientByID(1), testEngineerDistrict1)); //7
            interventions.Add(Intervention.Factory.CreateIntervention(
                2, types[0], Clients.GetClientByID(3), testEngineerDistrict3)); //8
            interventions.Add(Intervention.Factory.CreateIntervention(
                2, types[2], Clients.GetClientByID(2), testEngineerDistrict2)); //9
            interventions.Add(Intervention.Factory.CreateIntervention(
                2, types[1], Clients.GetClientByID(0), testEngineerDistrict0)); //10
            interventions.Add(Intervention.Factory.CreateIntervention(
                2, types[0], Clients.GetClientByID(5), testEngineerDistrict5)); //11

            //Approve a few interventions
            interventions[4].Approve(testEngineerDistrict0);
            interventions[5].Approve(testEngineerDistrict1);
            interventions[7].Approve(testEngineerDistrict1);
            interventions[10].Approve(testEngineerDistrict0);
        }

        public void Add(Intervention intervention)
        {
            interventions.Add(intervention);
        }

        public int Count
        {
            get { return interventions.Count; }
        }

        public Intervention this[int i]
        {
            get { return interventions[i]; }
        }

        public List<Intervention> GetInterventionsWithClient(int clientID)
        {
            return interventions.Where(x => x.Client.ID == clientID).ToList<Intervention>();
        }
    }
}
