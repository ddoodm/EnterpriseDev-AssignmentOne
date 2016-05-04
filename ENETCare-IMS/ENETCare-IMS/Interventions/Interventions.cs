using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ENETCare.IMS.Users;

namespace ENETCare.IMS.Interventions
{
    public class Interventions : IReadOnlyList<Intervention>
    {
        private ENETCareDAO application;

        private List<Intervention> interventions = new List<Intervention>();

        //IF ANYTHING BREAKS: Delete the below property, and replace all instances of 'InterventionList' with 'interventions'
        private List<Intervention> InterventionList { get { return interventions.Where(i => i.ApprovalState != InterventionApprovalState.Cancelled).Select(i => i).ToList(); } }

        public Interventions(ENETCareDAO application)
        {
            this.application = application;

            interventions = new List<Intervention>();
        }

        public Interventions(ENETCareDAO application, List<Intervention> list)
        {
            this.application = application;
            this.interventions = list;
        }

        /// <summary>
        /// Computes the next available ID number
        /// </summary>
        private int NextID
        {
            get
            {
                if (interventions.Count < 1)
                    return 0;

                var highestIntervention
                    = interventions.OrderByDescending(i => i.ID)
                    .FirstOrDefault();
                return highestIntervention.ID + 1;
            }
        }

        public Intervention CreateIntervention(
            InterventionType type,
            Client client,
            SiteEngineer siteEngineer,
            DateTime date,
            decimal? cost,
            decimal? labour,
            string notes)
        {
            int id = NextID;

            Intervention newIntervention = Intervention.Factory.CreateIntervention(
                id, type, client, siteEngineer, labour, cost, date);
            newIntervention.UpdateNotes(siteEngineer, notes);

            application.Save(newIntervention);
            Add(newIntervention);
            return newIntervention;
        }

        public void Add(Intervention intervention)
        {
            interventions.Add(intervention);
        }

        public int Count
        {
            get { return interventions.Count; }
        }

        /// <summary>
        /// Retrieves the Intervention with the given ID
        /// </summary>
        /// <param name="ID">The ID of the Intervention to retrieve</param>
        /// <returns>The Intervention with the given ID</returns>
        public Intervention this[int ID]
        {
            get
            {
                if (ID == 0)
                    throw new IndexOutOfRangeException("ENETCare data is 1-indexed, but an index of 0 was requested.");
                return interventions.First<Intervention>(
                    intervention => intervention.ID == ID);
            }
        }

        public List<Intervention> GetInterventionsWithClient(Client client)
        {
            return InterventionList
                .Where(x => x.Client.ID == client.ID)
                .ToList<Intervention>();
        }

        public List<Intervention> FilterByDistrict(District district)
        {
            return InterventionList
                .Where(x => x.District == district)
                .ToList<Intervention>();
        }

        public List<Intervention> FilterByState(InterventionApprovalState state)
        {
            return InterventionList
                .Where(x => x.ApprovalState == state)
                .ToList<Intervention>();
        }

        public List<Intervention> FilterForUserDisplay(IInterventionApprover user)
        {
            return InterventionList
                .Where(x =>
                    (x.District == user.District ||
                    x.SiteEngineer == user ||
                    x.ApprovingUser == user))
                .ToList<Intervention>();
        }

        public IEnumerator<Intervention> GetEnumerator()
        {
            return InterventionList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<Intervention> GetInterventions()
        {
            return InterventionList;
        }
    }
}
